using System;
using System.IO;
using IMAPI2;
using IMAPI2FS;

namespace STO_WORKBENCH.MultiBurner
{
    // Сервис записи данных на диск через IMAPI2 (Windows Image Mastering API v2).
    // Работает с COM-объектами из библиотек IMAPI2 / IMAPI2FS.
    // Основные операции: открыть/закрыть лоток привода и записать
    // содержимое папки на чистый диск в виде файловой системы ISO9660+Joliet+UDF.
    internal class ImapiBurnService
    {
        // Открывает лоток привода (извлекает диск).
        // recorderId — уникальный идентификатор рекордера IMAPI2.
        // Выбрасывает исключение, если привод недоступен (например, занят).
        public void Eject(string recorderId)
        {
            MsftDiscRecorder2 recorder = new MsftDiscRecorder2();
            recorder.InitializeDiscRecorder(recorderId);
            recorder.EjectMedia();
        }

        // Закрывает лоток привода (втягивает диск обратно).
        // recorderId — тот же идентификатор рекордера IMAPI2.
        public void CloseTray(string recorderId)
        {
            MsftDiscRecorder2 recorder = new MsftDiscRecorder2();
            recorder.InitializeDiscRecorder(recorderId);
            recorder.CloseTray();
        }

        // Главный метод модуля: записывает содержимое папки folderPath на диск.
        //   recorderId — идентификатор привода IMAPI2;
        //   volumeName — желаемое имя тома диска (будет очищено от недопустимых символов);
        //   includeRootFolder — включать ли саму папку-источник в корень диска.
        // Метод синхронный: блокирует вызвавший поток на все время записи,
        // поэтому должен вызываться только из фонового потока BurnWorker.
        // При любой проблеме выбрасывает исключение с пояснением на русском.
        public void BurnFolderToDisc(
            string recorderId,
            string folderPath,
            string volumeName,
            bool includeRootFolder)
        {
            // Папка-источник обязана существовать на момент запуска записи.
            if (!Directory.Exists(folderPath))
                throw new DirectoryNotFoundException(folderPath);

            // COM-объекты IMAPI2. Объявляются заранее, чтобы поля
            // try-блока были доступны (управляемые RCW-обертки,
            // освобождаются сборщиком мусора .NET, явный
            // Marshal.ReleaseComObject здесь не используется).
            MsftDiscRecorder2 recorder = null;
            MsftDiscFormat2Data dataWriter = null;
            MsftFileSystemImage fileSystemImage = null;

            try
            {
                // 1. Создаем рекордер и привязываем его к физическому приводу.
                recorder = new MsftDiscRecorder2();
                recorder.InitializeDiscRecorder(recorderId);

                // 2. Создаем писатель данных (формат Disc-at-Once / track data).
                dataWriter = new MsftDiscFormat2Data();

                // Проверяем, что IMAPI2 умеет работать с этим приводом.
                if (!dataWriter.IsRecorderSupported(recorder))
                    throw new Exception("Привод не поддерживается IMAPI2.");

                // Привязываем привод и задаем имя клиента — IMAPI2 показывает
                // его в системе, когда привод "занят записью".
                dataWriter.Recorder = recorder;
                dataWriter.ClientName = "STO_WORKBENCH MultiDriveBurner";

                // Диск в приводе должен существовать и поддерживаться для записи.
                if (!dataWriter.IsCurrentMediaSupported(recorder))
                    throw new Exception("Диск не вставлен или не поддерживается.");

                // Защита от случайной перезаписи: принимаем только пустые носители.
                if (!dataWriter.MediaHeuristicallyBlank)
                    throw new Exception("Диск не пустой.");

                // 3. Создаем образ файловой системы, который потом будет записан на диск.
                fileSystemImage = new MsftFileSystemImage();

                /*
                 * ВАЖНО:
                 * Здесь используется dynamic, потому что COM interop-типы
                 * IMAPI2.MsftDiscRecorder2 и IMAPI2FS.IDiscRecorder2
                 * в C# могут считаться несовместимыми.
                 */
                dynamic dynFileSystemImage = fileSystemImage;
                dynFileSystemImage.ChooseImageDefaults(recorder);

                // Записываем на диск сразу три файловые системы — так диск читается
                // везде: ISO9660 (совместимость), Joliet (длинные имена, кириллица),
                // UDF (современные ОС). Записывается только самая "богатая" из них,
                // остальные — как дополнение, реальный выбор делает IMAPI2.
                fileSystemImage.FileSystemsToCreate =
                    FsiFileSystems.FsiFileSystemISO9660 |
                    FsiFileSystems.FsiFileSystemJoliet |
                    FsiFileSystems.FsiFileSystemUDF;

                // Имя тома приводится к допустимому виду
                // (без недопустимых символов и ограничения по длине).
                fileSystemImage.VolumeName = MakeSafeVolumeName(volumeName);

                // Получаем корневой каталог образа — в него добавляем файлы.
                IFsiDirectoryItem root = fileSystemImage.Root;

                /*
                 * AddTree умеет сам добавлять дерево папки.
                 *
                 * includeRootFolder = true:
                 * Диск:
                 *   ПАПКА1\
                 *      file1.txt
                 *
                 * includeRootFolder = false:
                 * Диск:
                 *   file1.txt
                 *   subfolder\
                 */
                root.AddTree(folderPath, includeRootFolder);

                // 4. Собираем образ в памяти: результат содержит поток ImageStream,
                // готовый к поблочной записи на диск.
                IFileSystemImageResult image = fileSystemImage.CreateResultImage();

                // Запрет последующей дозаписи (multisession): диск финализируется,
                // после записи его нельзя дописать, но он читается на любых приводах.
                dataWriter.ForceMediaToBeClosed = true;

                /*
                 * Здесь тоже dynamic из-за несовместимости:
                 * IMAPI2FS.FsiStream и IMAPI2.IStream.
                 */
                // 5. Непосредственно запись образа на диск.
                // Блокируется до полного завершения прожига (может занять минуты).
                dynamic dynDataWriter = dataWriter;
                dynDataWriter.Write(image.ImageStream);
            }
            // catch пробрасывает исключение дальше без изменений —
            // вызывающий код (BurnWorker) сам решает, как его обработать.
            catch
            {
                throw;
            }
        }

        // Приводит произвольную строку к допустимому имени тома диска:
        //   - пустое имя заменяется на "DISC";
        //   - недопустимые для имени файла символы заменяются на "_";
        //   - пробелы заменяются на "_";
        //   - имя обрезается до 32 символов.
        // Возвращает безопасное имя тома.
        private string MakeSafeVolumeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                name = "DISC";

            // Заменяем все символы, недопустимые в именах файлов
            // (символов с такими же кодами обычно нет в именах томов).
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                name = name.Replace(c, '_');
            }

            name = name.Replace(' ', '_');

            /*
             * ISO9660/Joliet/UDF имеют ограничения по имени тома.
             * 32 символа — безопасный минимум.
             */
            if (name.Length > 32)
                name = name.Substring(0, 32);

            return name;
        }

        // Рекурсивно подсчитывает суммарный размер всех файлов в папке (в байтах).
        // path — путь к папке; если папки нет, возвращает 0.
        // Файлы, к которым нет доступа (заняты другим процессом и т.п.),
        // тихо пропускаются, чтобы подсчет не падал на одном "плохом" файле.
        public static long GetDirectorySize(string path)
        {
            long size = 0;

            if (!Directory.Exists(path))
                return 0;

            // Перебираем все файлы во всех вложенных подпапках.
            foreach (string file in Directory.GetFiles(path, "*", SearchOption.AllDirectories))
            {
                try
                {
                    FileInfo fi = new FileInfo(file);
                    size += fi.Length;
                }
                catch
                {
                    // файл может быть недоступен, пропускаем
                }
            }

            return size;
        }

    }
}
