using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ClosedXML.Excel;

namespace STO_WORKBENCH.FileNameExporter
{
    public class FileNameExportItem
    {
        public int Number { get; set; }
        public string SourceFileName { get; set; }
        public string ResultName { get; set; }
        public string FullPath { get; set; }
    }

    public class FileNameExporterOptions
    {
        public string FolderPath { get; set; }
        public string SearchPattern { get; set; } = "*.pdf";
        public string Template { get; set; } = "{4}";
        public string Separator { get; set; } = "_";
        public bool IncludeSubfolders { get; set; } = false;
        public bool IncludeOriginalFileName { get; set; } = true;
    }

    public class FileNameExporter
    {
        public List<FileNameExportItem> GetFileNames(FileNameExporterOptions options)
        {
            ValidateOptions(options);

            var searchOption = options.IncludeSubfolders
                ? SearchOption.AllDirectories
                : SearchOption.TopDirectoryOnly;

            var files = GetFiles(options.FolderPath, options.SearchPattern, searchOption);

            var result = new List<FileNameExportItem>();
            int number = 1;

            foreach (var filePath in files)
            {
                var fileInfo = new FileInfo(filePath);

                string sourceFileName = fileInfo.Name;
                string resultName = ApplyTemplate(
                    sourceFileName,
                    options.Template,
                    options.Separator
                );

                result.Add(new FileNameExportItem
                {
                    Number = number++,
                    SourceFileName = sourceFileName,
                    ResultName = resultName,
                    FullPath = filePath
                });
            }

            return result;
        }

        public void ExportToExcel(
            FileNameExporterOptions options,
            string outputExcelPath)
        {
            if (string.IsNullOrWhiteSpace(outputExcelPath))
                throw new ArgumentException("Не указан путь для сохранения Excel-файла.");

            var items = GetFileNames(options);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Файлы");

                int row = 1;
                int column = 1;

                worksheet.Cell(row, column++).Value = "№";

                if (options.IncludeOriginalFileName)
                    worksheet.Cell(row, column++).Value = "Исходное имя файла";

                worksheet.Cell(row, column++).Value = "Результат";

                worksheet.Row(row).Style.Font.Bold = true;

                row = 2;

                foreach (var item in items)
                {
                    column = 1;

                    worksheet.Cell(row, column++).Value = item.Number;

                    if (options.IncludeOriginalFileName)
                        worksheet.Cell(row, column++).Value = item.SourceFileName;

                    worksheet.Cell(row, column++).Value = item.ResultName;

                    row++;
                }

                worksheet.Columns().AdjustToContents();

                workbook.SaveAs(outputExcelPath);
            }
        }

        private static string ApplyTemplate(
            string fileName,
            string template,
            string separator)
        {
            string nameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            string extension = Path.GetExtension(fileName);

            string[] parts;

            if (string.IsNullOrEmpty(separator))
                parts = new[] { nameWithoutExtension };
            else
                parts = nameWithoutExtension.Split(
                    new[] { separator },
                    StringSplitOptions.None
                );

            string result = template;

            result = result.Replace("{file}", fileName);
            result = result.Replace("{name}", nameWithoutExtension);
            result = result.Replace("{ext}", extension);

            result = Regex.Replace(result, @"\{(\d+)\}", match =>
            {
                int index = int.Parse(match.Groups[1].Value);

                if (index >= 0 && index < parts.Length)
                    return parts[index];

                return string.Empty;
            });

            return result;
        }

        private static IEnumerable<string> GetFiles(
            string folderPath,
            string searchPattern,
            SearchOption searchOption)
        {
            if (string.IsNullOrWhiteSpace(searchPattern))
                searchPattern = "*.*";

            var patterns = searchPattern
                .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();

            if (patterns.Count == 0)
                patterns.Add("*.*");

            var files = new List<string>();

            foreach (var pattern in patterns)
            {
                files.AddRange(Directory.GetFiles(folderPath, pattern, searchOption));
            }

            return files
                .Distinct()
                .OrderBy(x => x)
                .ToList();
        }

        private static void ValidateOptions(FileNameExporterOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (string.IsNullOrWhiteSpace(options.FolderPath))
                throw new ArgumentException("Не указана папка.");

            if (!Directory.Exists(options.FolderPath))
                throw new DirectoryNotFoundException("Папка не найдена.");

            if (string.IsNullOrWhiteSpace(options.Template))
                throw new ArgumentException("Не указан шаблон результата.");
        }
    }
}