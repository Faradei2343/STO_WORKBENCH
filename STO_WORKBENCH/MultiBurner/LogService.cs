using System;
using System.IO;
using System.Text;

namespace STO_WORKBENCH.MultiBurner
{
    // Простой файловый логгер модуля MultiBurner.
    // Пишет текстовые строки с временной меткой в файл
    // C:\ProgramData\MultiDriveBurner\logs\yyyy-MM-dd.log (один файл на день).
    // Все методы статические, экземпляр не нужен.
    internal class LogService
    {
        // Объект синхронизации: несколько фоновых потоков записи (BurnWorker)
        // могут логировать одновременно, lock защищает файл от смешивания строк.
        private static readonly object _lock = new object();

        // Путь к каталогу с логами. Путь вычисляется заново при каждом обращении,
        // чтобы подхватывать смену даты (имя файла содержит yyyy-MM-dd).
        private static string LogDirectory
        {
            get
            {
                // CommonApplicationData = C:\ProgramData — доступен всем пользователям,
                // не требует прав администратора на запись.
                string dir = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                    "MultiDriveBurner",
                    "logs");

                // Каталог создается при первом обращении, чтобы AppendAllText не падал.
                Directory.CreateDirectory(dir);
                return dir;
            }
        }

        // Дописывает одну строку в лог-файл текущего дня.
        // message — произвольный текст события; временная метка добавляется автоматически.
        // Потокобезопасно (внутри lock). Файл открывается на каждую запись,
        // что медленно, но надежно и не держит дескриптор открытым.
        public static void Write(string message)
        {
            lock (_lock)
            {
                // Имя файла формируется по текущей дате: один файл на календарный день.
                string file = Path.Combine(LogDirectory, DateTime.Now.ToString("yyyy-MM-dd") + ".log");

                // Формируем строку вида "2024-01-01 12:30:45; сообщение"
                // с разделителем ";" — удобно потом разбирать в Excel.
                string line = string.Format(
                    "{0:yyyy-MM-dd HH:mm:ss}; {1}",
                    DateTime.Now,
                    message);

                // UTF-8 без BOM — кириллица в логах читается любым редактором.
                File.AppendAllText(file, line + Environment.NewLine, Encoding.UTF8);
            }
        }

    }
}
