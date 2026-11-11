namespace STO_WORKBENCH.Web.Services;

/// <summary>
/// Порт DetOdSorter/Sorter.cs из WinForms-версии: сортировка det_*.xls / od_*_{имя}_*.xls
/// по подпапкам внутри "Сортировка детализаций yyyy-MM-dd" в папке Downloads.
/// </summary>
public class Sorter
{
    private readonly Action<string> log;

    public Sorter(Action<string> logAction)
    {
        log = logAction ?? throw new ArgumentNullException(nameof(logAction));
    }

    public string? LastTargetRoot { get; private set; }

    public bool Sort(string sourcePath, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(sourcePath) || !Directory.Exists(sourcePath))
        {
            log("!!Исходная папка не существует!!");
            return false;
        }

        try
        {
            // 1. Определяем папку Downloads пользователя
            string downloads = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            if (!Directory.Exists(downloads))
            {
                log("!!Папка Downloads не найдена!!");
                return false;
            }

            // 2. Создаём целевую папку с текущей датой
            string targetRoot = Path.Combine(downloads, $"Сортировка детализаций {DateTime.Now:yyyy-MM-dd}");
            Directory.CreateDirectory(targetRoot);
            LastTargetRoot = targetRoot;
            log($"Целевая папка: {targetRoot}");

            log("**Начинаем сортировку документов**");
            log("----------------------------------------------------");

            DirectoryInfo sourceDir = new DirectoryInfo(sourcePath);

            // 3. Получаем список det_*.xls
            List<string> detFiles = GetDetFilesList(sourceDir);
            if (detFiles == null || detFiles.Count == 0)
            {
                log("!!Файлы det_*.xls не найдены!!");
                return false;
            }

            // 4. Обрабатываем каждый det-файл
            foreach (string detFilePath in detFiles)
            {
                cancellationToken.ThrowIfCancellationRequested();

                string fileName = Path.GetFileName(detFilePath);
                string directoryName = GetDirectoryName(fileName);
                log($"Обработка файла: {fileName}");

                SortDetFiles(directoryName, fileName, sourcePath, targetRoot);
                SortOdFiles(directoryName, sourcePath, targetRoot);
            }

            log("----------------------------------------------------");
            log("**Сортировка завершена**");
            return true;
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            log($"ОШИБКА: {ex.Message}");
            return false;
        }
    }

    // ---------- Вспомогательные методы (приватные) ----------

    private List<string> GetDetFilesList(DirectoryInfo directoryInfo)
    {
        var list = new List<string>();
        foreach (var f in directoryInfo.GetFiles("det_*.xls"))
            list.Add(f.FullName);
        return list.Count > 0 ? list : null!;
    }

    private string GetDirectoryName(string fileName)
    {
        var parts = fileName.Split('_');
        if (parts.Length >= 3)
            return parts[2];
        throw new InvalidOperationException($"Неверный формат имени файла: {fileName}");
    }

    private void SortDetFiles(string directoryName, string fileName, string sourcePath, string targetRoot)
    {
        log($"  Создаём подпапку: {directoryName}");
        string targetSubDir = Path.Combine(targetRoot, directoryName);
        Directory.CreateDirectory(targetSubDir);

        string src = Path.Combine(sourcePath, fileName);
        string dst = Path.Combine(targetSubDir, fileName);

        if (File.Exists(src))
        {
            File.Copy(src, dst, overwrite: true); // копируем (исходник остаётся)
            log($"    Скопирован det: {fileName}");
        }
        else
            log($"    Файл {src} не найден!");
    }

    private string? GetOdFile(string directoryName, DirectoryInfo directoryInfo)
    {
        foreach (var f in directoryInfo.GetFiles($"od_*_{directoryName}_*.xls"))
            return f.FullName;
        return null;
    }

    private void SortOdFiles(string directoryName, string sourcePath, string targetRoot)
    {
        DirectoryInfo sourceDir = new DirectoryInfo(sourcePath);
        string? odPath = GetOdFile(directoryName, sourceDir);
        if (string.IsNullOrEmpty(odPath))
        {
            log($"    od-файл для {directoryName} не найден");
            return;
        }

        string fileName = Path.GetFileName(odPath);
        string targetSubDir = Path.Combine(targetRoot, directoryName);
        Directory.CreateDirectory(targetSubDir); // на всякий случай
        string dst = Path.Combine(targetSubDir, fileName);

        if (File.Exists(odPath))
        {
            File.Copy(odPath, dst, overwrite: true);
            log($"    Скопирован od: {fileName}");
        }
        else
            log($"    od-файл {odPath} не найден!");
    }
}
