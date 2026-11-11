namespace STO_WORKBENCH.Web.Services;

// Сервис записи данных на диск через IMAPI2 (Windows Image Mastering API v2).
// Работает через late binding (ComInterop), без interop-сборок.
// ВСЕ методы выполняются в STA-потоке: из собственного потока BurnWorker
// или через StaComRunner.
public class ImapiBurnService
{
    // Открывает лоток привода (извлекает диск).
    public void Eject(string recorderId)
    {
        dynamic recorder = ComInterop.Create(ComInterop.ImapiDiscRecorder2);
        recorder.InitializeDiscRecorder(recorderId);
        recorder.EjectMedia();
    }

    // Закрывает лоток привода (втягивает диск обратно).
    public void CloseTray(string recorderId)
    {
        dynamic recorder = ComInterop.Create(ComInterop.ImapiDiscRecorder2);
        recorder.InitializeDiscRecorder(recorderId);
        recorder.CloseTray();
    }

    // Главный метод модуля: записывает содержимое папки folderPath на диск.
    // Блокирует вызвавший поток на все время записи,
    // поэтому вызывается только из фонового потока BurnWorker.
    public void BurnFolderToDisc(
        string recorderId,
        string folderPath,
        string volumeName,
        bool includeRootFolder)
    {
        if (!Directory.Exists(folderPath))
            throw new DirectoryNotFoundException(folderPath);

        dynamic recorder = ComInterop.Create(ComInterop.ImapiDiscRecorder2);
        recorder.InitializeDiscRecorder(recorderId);

        dynamic dataWriter = ComInterop.Create(ComInterop.ImapiDiscFormat2Data);

        // Проверяем, что IMAPI2 умеет работать с этим приводом.
        if (!(bool)dataWriter.IsRecorderSupported(recorder))
            throw new Exception("Привод не поддерживается IMAPI2.");

        // Привязываем привод и задаем имя клиента — IMAPI2 показывает
        // его в системе, когда привод "занят записью".
        dataWriter.Recorder = recorder;
        dataWriter.ClientName = "STO_WORKBENCH MultiDriveBurner";

        // Диск в приводе должен существовать и поддерживаться для записи.
        if (!(bool)dataWriter.IsCurrentMediaSupported(recorder))
            throw new Exception("Диск не вставлен или не поддерживается.");

        // Защита от случайной перезаписи: принимаем только пустые носители.
        if (!(bool)dataWriter.MediaHeuristicallyBlank)
            throw new Exception("Диск не пустой.");

        dynamic fileSystemImage = ComInterop.Create(ComInterop.ImapiFileSystemImage);

        // Выбираем параметры образа по умолчанию для этого привода/носителя.
        fileSystemImage.ChooseImageDefaults(recorder);

        // Записываем на диск сразу три файловые системы — так диск читается
        // везде: ISO9660 (совместимость), Joliet (длинные имена, кириллица),
        // UDF (современные ОС). 1 | 2 | 4.
        fileSystemImage.FileSystemsToCreate = 7;

        fileSystemImage.VolumeName = MakeSafeVolumeName(volumeName);

        dynamic root = fileSystemImage.Root;

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

        dynamic image = fileSystemImage.CreateResultImage();

        // Запрет последующей дозаписи (multisession): диск финализируется,
        // после записи его нельзя дописать, но он читается на любых приводах.
        dataWriter.ForceMediaToBeClosed = true;

        // Непосредственно запись образа на диск.
        // Блокируется до полного завершения прожига (может занять минуты).
        dataWriter.Write(image.ImageStream);
    }

    // Приводит произвольную строку к допустимому имени тома диска:
    //   - пустое имя заменяется на "DISC";
    //   - недопустимые для имени файла символы заменяются на "_";
    //   - пробелы заменяются на "_";
    //   - имя обрезается до 32 символов.
    private static string MakeSafeVolumeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            name = "DISC";

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
    // Файлы, к которым нет доступа, тихо пропускаются.
    public static long GetDirectorySize(string path)
    {
        long size = 0;

        if (!Directory.Exists(path))
            return 0;

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
