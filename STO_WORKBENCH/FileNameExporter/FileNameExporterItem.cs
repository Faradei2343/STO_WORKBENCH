using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ClosedXML.Excel;

namespace STO_WORKBENCH.FileNameExporter
{
    // Модель одной строки результата: связывает исходный файл с вычисленными
    // для него значениями (новое имя, целевая папка сортировки, ключ группировки).
    public class FileNameExportItem
    {
        // Порядковый номер файла в итоговом списке (нумерация с 1; после удаления дублей пересчитывается).
        public int Number { get; set; }
        // Исходное имя файла без пути (с расширением).
        public string SourceFileName { get; set; }
        // Итоговое имя, полученное применением шаблона (TemplateEngine.ApplyTemplate).
        public string ResultName { get; set; }
        // Полный путь к исходному файлу на диске.
        public string FullPath { get; set; }
        // Имя папки, в которую файл должен попасть по правилам сортировки;
        // пустая строка — ни одно правило не подошло.
        public string TargetFolder { get; set; }
        // Ключ группировки (например номер комплекта); пустая строка, если группировка выключена.
        public string GroupKey { get; set; }
    }

    public class FileNameExporterOptions
    {
        public string FolderPath { get; set; }
        public string SearchPattern { get; set; } = "*.*";
        public string Template { get; set; } = "{name}";
        public string Separator { get; set; } = "_";
        public bool IncludeSubfolders { get; set; }
        public bool IncludeOriginalFileName { get; set; } = true;

        public bool RemoveDuplicates { get; set; }
        public bool IgnoreCaseDuplicates { get; set; } = true;

        public bool OrganizeFiles { get; set; }
        public bool CopyFiles { get; set; } = true;
        public string OutputRootFolder { get; set; }

        public List<FileSortRule> SortRules { get; set; } = new List<FileSortRule>();

        public bool EnableGrouping { get; set; }

        /// <summary>
        /// Например:
        /// {match:№\s*(\d+):1}
        /// или
        /// {match:№\s*(\d+):1}_{match:от\s*(\d{2}\.\d{2}\.\d{4}):1}
        /// </summary>
        public string GroupKeyTemplate { get; set; }

        /// <summary>
        /// Например:
        /// Комплект_{group}
        /// или
        /// {group}
        /// </summary>
        public string GroupFolderTemplate { get; set; } = "{group}";
    }

    public class FileSortRule
    {
        public bool Enabled { get; set; } = true;

        public string TargetFolderName { get; set; }

        /// <summary>
        /// Несколько regex-шаблонов.
        /// Если хотя бы один совпал — файл попадает в TargetFolderName.
        /// </summary>
        public List<string> RegexPatterns { get; set; } = new List<string>();

        public bool IgnoreCase { get; set; } = true;
    }

    public class FileNameExporter
    {
        public List<FileNameExportItem> GetFileNames(FileNameExporterOptions options)
        {
            ValidateOptions(options);

            SearchOption searchOption = options.IncludeSubfolders
                ? SearchOption.AllDirectories
                : SearchOption.TopDirectoryOnly;

            List<string> files = GetFiles(
                options.FolderPath,
                options.SearchPattern,
                searchOption
            ).ToList();

            List<FileNameExportItem> result = new List<FileNameExportItem>();

            int number = 1;

            foreach (string filePath in files)
            {
                FileInfo fileInfo = new FileInfo(filePath);
                string fileName = fileInfo.Name;

                string resultName = TemplateEngine.ApplyTemplate(
                    fileName,
                    options.Template,
                    options.Separator
                );

                string targetFolder = GetTargetFolder(fileName, options.SortRules);

                string groupKey = string.Empty;

                if (options.EnableGrouping && !string.IsNullOrWhiteSpace(options.GroupKeyTemplate))
                {
                    groupKey = TemplateEngine.ApplyTemplate(
                        fileName,
                        options.GroupKeyTemplate,
                        options.Separator
                    );
                }

                result.Add(new FileNameExportItem
                {
                    Number = number++,
                    SourceFileName = fileName,
                    ResultName = resultName,
                    FullPath = filePath,
                    TargetFolder = targetFolder,
                    GroupKey = groupKey
                });
            }

            if (options.RemoveDuplicates)
            {
                result = RemoveDuplicates(result, options.IgnoreCaseDuplicates);
            }

            Renumber(result);

            return result;
        }

        public void ExportToExcel(FileNameExporterOptions options, string outputExcelPath)
        {
            if (string.IsNullOrWhiteSpace(outputExcelPath))
                throw new ArgumentException("Не указан путь сохранения Excel.");

            List<FileNameExportItem> items = GetFileNames(options);

            using (XLWorkbook workbook = new XLWorkbook())
            {
                IXLWorksheet sheet = workbook.Worksheets.Add("Файлы");

                int row = 1;
                int col = 1;

                sheet.Cell(row, col++).Value = "№";

                if (options.IncludeOriginalFileName)
                    sheet.Cell(row, col++).Value = "Исходное имя файла";

                sheet.Cell(row, col++).Value = "Результат";
                sheet.Cell(row, col++).Value = "Группа";
                sheet.Cell(row, col++).Value = "Папка";
                sheet.Cell(row, col++).Value = "Полный путь";

                sheet.Row(row).Style.Font.Bold = true;

                row = 2;

                foreach (FileNameExportItem item in items)
                {
                    col = 1;

                    sheet.Cell(row, col++).Value = item.Number;

                    if (options.IncludeOriginalFileName)
                        sheet.Cell(row, col++).Value = item.SourceFileName;

                    sheet.Cell(row, col++).Value = item.ResultName;
                    sheet.Cell(row, col++).Value = item.GroupKey;
                    sheet.Cell(row, col++).Value = item.TargetFolder;
                    sheet.Cell(row, col++).Value = item.FullPath;

                    row++;
                }

                sheet.Columns().AdjustToContents();

                workbook.SaveAs(outputExcelPath);
            }
        }

        public void OrganizeFiles(FileNameExporterOptions options)
        {
            if (!options.OrganizeFiles)
                return;

            if (string.IsNullOrWhiteSpace(options.OutputRootFolder))
                throw new ArgumentException("Не указана папка, куда сортировать файлы.");

            Directory.CreateDirectory(options.OutputRootFolder);

            List<FileNameExportItem> items = GetFileNames(options);

            foreach (FileNameExportItem item in items)
            {
                string targetFolderPath = BuildTargetFolderPath(options, item);

                Directory.CreateDirectory(targetFolderPath);

                string fileName = Path.GetFileName(item.FullPath);
                string destinationPath = Path.Combine(targetFolderPath, fileName);

                destinationPath = GetUniqueFilePath(destinationPath);

                if (options.CopyFiles)
                    File.Copy(item.FullPath, destinationPath);
                else
                    File.Move(item.FullPath, destinationPath);
            }
        }

        private static string BuildTargetFolderPath(
            FileNameExporterOptions options,
            FileNameExportItem item)
        {
            string categoryFolder = string.IsNullOrWhiteSpace(item.TargetFolder)
                ? "Без категории"
                : item.TargetFolder;

            categoryFolder = SanitizeFileName(categoryFolder);

            string path = Path.Combine(options.OutputRootFolder, categoryFolder);

            if (options.EnableGrouping && !string.IsNullOrWhiteSpace(item.GroupKey))
            {
                string groupFolder = options.GroupFolderTemplate;

                if (string.IsNullOrWhiteSpace(groupFolder))
                    groupFolder = "{group}";

                groupFolder = groupFolder.Replace("{group}", item.GroupKey);
                groupFolder = SanitizeFileName(groupFolder);

                path = Path.Combine(path, groupFolder);
            }

            return path;
        }

        private static string GetTargetFolder(string fileName, List<FileSortRule> rules)
        {
            if (rules == null || rules.Count == 0)
                return string.Empty;

            foreach (FileSortRule rule in rules)
            {
                if (rule == null)
                    continue;

                if (!rule.Enabled)
                    continue;

                if (string.IsNullOrWhiteSpace(rule.TargetFolderName))
                    continue;

                if (rule.RegexPatterns == null || rule.RegexPatterns.Count == 0)
                    continue;

                RegexOptions regexOptions = RegexOptions.None;

                if (rule.IgnoreCase)
                    regexOptions |= RegexOptions.IgnoreCase;

                foreach (string pattern in rule.RegexPatterns)
                {
                    if (string.IsNullOrWhiteSpace(pattern))
                        continue;

                    if (Regex.IsMatch(fileName, pattern, regexOptions))
                    {
                        return rule.TargetFolderName;
                    }
                }
            }

            return string.Empty;
        }

        private static List<FileNameExportItem> RemoveDuplicates(
            List<FileNameExportItem> items,
            bool ignoreCase)
        {
            StringComparer comparer = ignoreCase
                ? StringComparer.OrdinalIgnoreCase
                : StringComparer.Ordinal;

            return items
                .GroupBy(x => x.ResultName ?? string.Empty, comparer)
                .Select(x => x.First())
                .ToList();
        }

        private static void Renumber(List<FileNameExportItem> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                items[i].Number = i + 1;
            }
        }

        private static IEnumerable<string> GetFiles(
            string folderPath,
            string searchPattern,
            SearchOption searchOption)
        {
            if (string.IsNullOrWhiteSpace(searchPattern))
                searchPattern = "*.*";

            string[] patterns = searchPattern
                .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();

            if (patterns.Length == 0)
                patterns = new[] { "*.*" };

            List<string> files = new List<string>();

            foreach (string pattern in patterns)
            {
                files.AddRange(Directory.GetFiles(folderPath, pattern, searchOption));
            }

            return files
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(x => x)
                .ToList();
        }

        private static string GetUniqueFilePath(string path)
        {
            if (!File.Exists(path))
                return path;

            string folder = Path.GetDirectoryName(path);
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(path);
            string ext = Path.GetExtension(path);

            int index = 1;

            while (true)
            {
                string newPath = Path.Combine(
                    folder,
                    $"{fileNameWithoutExt} ({index}){ext}"
                );

                if (!File.Exists(newPath))
                    return newPath;

                index++;
            }
        }

        private static string SanitizeFileName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            foreach (char c in Path.GetInvalidFileNameChars())
            {
                value = value.Replace(c, '_');
            }

            return value.Trim();
        }

        private static void ValidateOptions(FileNameExporterOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (string.IsNullOrWhiteSpace(options.FolderPath))
                throw new ArgumentException("Не указана папка с файлами.");

            if (!Directory.Exists(options.FolderPath))
                throw new DirectoryNotFoundException("Папка с файлами не найдена.");

            if (string.IsNullOrWhiteSpace(options.Template))
                throw new ArgumentException("Не указан шаблон результата.");
        }
    }
}