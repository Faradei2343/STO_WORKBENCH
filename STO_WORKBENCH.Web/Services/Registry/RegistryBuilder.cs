using System.Text;
using System.Xml.Linq;
using ExcelDataReader;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace STO_WORKBENCH.Web.Services;

// Построение реестров передачи СЛ из файлов детализации det_*.xls.
// Порт RegistryBuilder из WinForms-версии (STO_WORKBENCH/SLRegistry).
public class RegistryBuilder
{
    private readonly Action<string> log;
    private readonly XNamespace ss = "urn:schemas-microsoft-com:office:spreadsheet";

    public RegistryBuilder(Action<string> logAction)
    {
        log = logAction ?? throw new ArgumentNullException(nameof(logAction));
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    }

    // ==================== Публичные методы ====================

    /// <summary>
    /// Обрабатывает все det_*.xls из папки
    /// </summary>
    public List<string>? BuildAll(string folderPath, string period, string projectName, string? outputDirectory = null)
    {
        if (string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath))
        {
            log("!!Папка не существует!!");
            return null;
        }

        if (string.IsNullOrEmpty(outputDirectory))
        {
            outputDirectory = Path.Combine(Path.GetTempPath(), $"Реестры {period} {projectName} {DateTime.Now:yyyy-MM-dd}");
            Directory.CreateDirectory(outputDirectory);
        }
        else
        {
            Directory.CreateDirectory(outputDirectory);
        }

        log($"Папка сохранения: {outputDirectory}");
        log($"Поиск det_*.xls в: {folderPath}");
        log("----------------------------------------------------");

        var detFiles = Directory.GetFiles(folderPath, "det_*.xls");
        if (detFiles.Length == 0)
        {
            log("!!Файлы det_*.xls не найдены!!");
            return null;
        }

        log($"Найдено файлов: {detFiles.Length}");
        var createdFiles = new List<string>();

        foreach (string detFilePath in detFiles)
        {
            log($"Обработка: {Path.GetFileName(detFilePath)}");
            string? result = BuildSingle(detFilePath, period, projectName, outputDirectory);
            if (!string.IsNullOrEmpty(result))
                createdFiles.Add(result);
            log("----------------------------------------------------");
        }

        log($"**Создано реестров: {createdFiles.Count}**");
        return createdFiles;
    }

    /// <summary>
    /// Создаёт реестр для одного det-файла
    /// </summary>
    public string? BuildSingle(string detFilePath, string period, string projectName, string? outputDirectory = null)
    {
        if (string.IsNullOrEmpty(detFilePath) || !File.Exists(detFilePath))
        {
            log("!!Файл детализации не найден!!");
            return null;
        }

        if (string.IsNullOrEmpty(period))
        {
            log("!!Не указан отчётный период!!");
            return null;
        }

        if (string.IsNullOrEmpty(projectName))
        {
            log("!!Не указано наименование проекта!!");
            return null;
        }

        try
        {
            // 1. Читаем данные (автоопределение формата)
            var (orgName, orgCode) = ReadDetData(detFilePath);
            List<string> slList = ReadSLList(detFilePath);

            if (string.IsNullOrEmpty(orgCode) || string.IsNullOrEmpty(orgName))
            {
                log("!!Не удалось извлечь код или наименование!!");
                return null;
            }

            log($"  Код ЭО: {orgCode}, Наименование: {orgName}");
            log($"  Найдено заявок (СЛ): {slList.Count}");

            // 2. Папка сохранения
            if (string.IsNullOrEmpty(outputDirectory))
            {
                outputDirectory = Path.Combine(Path.GetTempPath(), "Реестры");
                Directory.CreateDirectory(outputDirectory);
            }

            // 3. Создаём Excel-файл через EPPlus
            string fileName = $"Реестр Передачи {orgCode.Trim()}.xlsx";
            string fullPath = Path.Combine(outputDirectory, fileName);

            using (var package = new ExcelPackage())
            {
                var sheet = package.Workbook.Worksheets.Add("Реестр передачи");
                BuildExcelSheet(sheet, orgCode, orgName, slList, period, projectName);
                using (var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    package.SaveAs(stream);
                }
            }

            log($"  Реестр сохранён: {fullPath}");
            return fullPath;
        }
        catch (Exception ex)
        {
            log($"  ОШИБКА в BuildSingle: {ex.Message}");
            return null;
        }
    }

    // ==================== Построение Excel-листа ====================

    private void BuildExcelSheet(ExcelWorksheet sheet, string orgCode, string orgName,
                                 List<string> slList, string period, string projectName)
    {
        void SetFont(ExcelRange cell, string name = "Calibri", int size = 10, bool bold = false)
        {
            cell.Style.Font.Name = name;
            cell.Style.Font.Size = size;
            cell.Style.Font.Bold = bold;
        }

        void SetBorder(ExcelRange cell)
        {
            cell.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            cell.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            cell.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            cell.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        }

        void SetAlignment(ExcelRange cell, ExcelHorizontalAlignment h, ExcelVerticalAlignment v, bool wrap = false)
        {
            cell.Style.HorizontalAlignment = h;
            cell.Style.VerticalAlignment = v;
            cell.Style.WrapText = wrap;
        }

        // Заголовок
        sheet.Cells[2, 1, 2, 8].Merge = true;
        sheet.Cells[2, 1].Value = "РЕЕСТР ПЕРЕДАЧИ СЛ";
        SetFont(sheet.Cells[2, 1], "Calibri", 10, true);
        SetAlignment(sheet.Cells[2, 1], ExcelHorizontalAlignment.Center, ExcelVerticalAlignment.Center);

        // Договор/Исполнитель
        sheet.Cells[3, 1, 3, 4].Merge = true;
        sheet.Cells[3, 1].Value = "Договор/Исполнитель: Мурманский филиал";
        SetFont(sheet.Cells[3, 1], "Calibri", 8, true);
        SetAlignment(sheet.Cells[3, 1], ExcelHorizontalAlignment.Left, ExcelVerticalAlignment.Center);

        // Проект
        sheet.Cells[4, 2, 4, 4].Merge = true;
        sheet.Cells[4, 1].Value = "Проект:";
        sheet.Cells[4, 2].Value = projectName;
        SetFont(sheet.Cells[4, 1], "Calibri", 8, true);
        SetFont(sheet.Cells[4, 2], "Calibri", 8, false);
        SetAlignment(sheet.Cells[4, 1], ExcelHorizontalAlignment.Left, ExcelVerticalAlignment.Center);
        SetAlignment(sheet.Cells[4, 2], ExcelHorizontalAlignment.Left, ExcelVerticalAlignment.Center);

        // Отчётный период
        sheet.Cells[7, 1, 7, 4].Merge = true;
        sheet.Cells[7, 1].Value = $"Отчетный период: {period}";
        SetFont(sheet.Cells[7, 1], "Calibri", 8, true);
        SetAlignment(sheet.Cells[7, 1], ExcelHorizontalAlignment.Left, ExcelVerticalAlignment.Center);

        // Заголовки таблицы (строка 8)
        int headerRow = 8;
        sheet.Cells[headerRow, 1].Value = "Код Адреса ЭО ВТК";
        sheet.Cells[headerRow, 2, headerRow, 3].Merge = true;
        sheet.Cells[headerRow, 2].Value = "Округ";
        sheet.Cells[headerRow, 4, headerRow, 5].Merge = true;
        sheet.Cells[headerRow, 4].Value = "Наименование ЭО";
        sheet.Cells[headerRow, 6, headerRow, 7].Merge = true;
        sheet.Cells[headerRow, 6].Value = "Тип документа";

        var headerRange = sheet.Cells[headerRow, 1, headerRow, 7];
        headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
        headerRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);
        headerRange.Style.Font.Bold = true;
        headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        headerRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        headerRange.Style.WrapText = true;
        SetBorder(headerRange);

        // 47 строк данных (9..55)
        for (int i = 0; i < 47; i++)
        {
            int row = 9 + i;
            for (int col = 1; col <= 7; col++)
            {
                var cell = sheet.Cells[row, col];
                SetBorder(cell);
                cell.Style.Font.Size = 8;
                cell.Style.Font.Name = "Calibri";
            }
        }

        // Заполнение основных данных (объединённые ячейки)
        sheet.Cells[9, 2, 55, 3].Merge = true;
        sheet.Cells[9, 4, 55, 5].Merge = true;
        sheet.Cells[9, 6, 55, 7].Merge = true;

        sheet.Cells[9, 1].Value = orgCode;
        sheet.Cells[9, 2].Value = "ВТК-Мурманск";
        sheet.Cells[9, 4].Value = orgName;
        sheet.Cells[9, 6].Value = "Сервисный лист";

        SetAlignment(sheet.Cells[9, 1], ExcelHorizontalAlignment.Left, ExcelVerticalAlignment.Top, true);
        SetAlignment(sheet.Cells[9, 2], ExcelHorizontalAlignment.Left, ExcelVerticalAlignment.Top, true);
        SetAlignment(sheet.Cells[9, 4], ExcelHorizontalAlignment.Left, ExcelVerticalAlignment.Top, true);
        SetAlignment(sheet.Cells[9, 6], ExcelHorizontalAlignment.Left, ExcelVerticalAlignment.Top, true);

        // Блоки с номерами заявок
        int slCount = slList.Count;
        int rowsPerBlock = 47;
        int blocks = (int)Math.Ceiling((double)slCount / rowsPerBlock);
        int currentIndex = 0;

        for (int block = 0; block < blocks; block++)
        {
            int startCol = 8 + block * 2;
            // Заголовки блока (строка headerRow)
            sheet.Cells[headerRow, startCol].Value = "№ п/п";
            sheet.Cells[headerRow, startCol + 1].Value = "Номер заявки";
            var headerBlock = sheet.Cells[headerRow, startCol, headerRow, startCol + 1];
            headerBlock.Style.Fill.PatternType = ExcelFillStyle.Solid;
            headerBlock.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);
            headerBlock.Style.Font.Bold = true;
            headerBlock.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            headerBlock.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            SetBorder(headerBlock);

            // Данные
            for (int rowOffset = 0; rowOffset < rowsPerBlock; rowOffset++)
            {
                int dataRow = 9 + rowOffset;
                if (currentIndex < slCount)
                {
                    sheet.Cells[dataRow, startCol].Value = currentIndex + 1;
                    sheet.Cells[dataRow, startCol + 1].Value = slList[currentIndex];
                    currentIndex++;
                }
                else
                {
                    sheet.Cells[dataRow, startCol].Value = "";
                    sheet.Cells[dataRow, startCol + 1].Value = "";
                }
                SetBorder(sheet.Cells[dataRow, startCol]);
                SetBorder(sheet.Cells[dataRow, startCol + 1]);
                sheet.Cells[dataRow, startCol].Style.Font.Size = 8;
                sheet.Cells[dataRow, startCol].Style.Font.Name = "Calibri";
                sheet.Cells[dataRow, startCol + 1].Style.Font.Size = 8;
                sheet.Cells[dataRow, startCol + 1].Style.Font.Name = "Calibri";
            }
        }

        // Итого
        int lastCol = 7 + blocks * 2;
        int itogoRow = 56;
        sheet.Cells[itogoRow, lastCol - 1, itogoRow, lastCol].Merge = true;
        sheet.Cells[itogoRow, lastCol - 1].Value = $"Итого передано СЛ: {slCount}";
        SetFont(sheet.Cells[itogoRow, lastCol - 1], "Calibri", 8, false);
        SetAlignment(sheet.Cells[itogoRow, lastCol - 1], ExcelHorizontalAlignment.Left, ExcelVerticalAlignment.Center);

        // Подписи "Передал:" и "Принял:"
        int sign1 = 57, under1 = 58, fio1 = 59;
        sheet.Cells[sign1, lastCol - 8].Value = "Передал:";
        SetFont(sheet.Cells[sign1, lastCol - 8], "Calibri", 8, false);
        SetAlignment(sheet.Cells[sign1, lastCol - 8], ExcelHorizontalAlignment.Left, ExcelVerticalAlignment.Center);

        sheet.Cells[under1, lastCol - 8, under1, lastCol].Merge = true;
        sheet.Cells[under1, lastCol - 8, under1, lastCol].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

        sheet.Cells[fio1, lastCol - 4, fio1, lastCol - 2].Merge = true;
        sheet.Cells[fio1, lastCol - 4].Value = "(ФИО)";
        SetFont(sheet.Cells[fio1, lastCol - 4], "Calibri", 8, false);
        SetAlignment(sheet.Cells[fio1, lastCol - 4], ExcelHorizontalAlignment.Center, ExcelVerticalAlignment.Center);

        sheet.Cells[fio1, lastCol - 1, fio1, lastCol].Merge = true;
        sheet.Cells[fio1, lastCol - 1].Value = "(подпись)";
        SetFont(sheet.Cells[fio1, lastCol - 1], "Calibri", 8, false);
        SetAlignment(sheet.Cells[fio1, lastCol - 1], ExcelHorizontalAlignment.Center, ExcelVerticalAlignment.Center);

        int sign2 = 60, under2 = 61, fio2 = 62;
        sheet.Cells[sign2, lastCol - 8].Value = "Принял:";
        SetFont(sheet.Cells[sign2, lastCol - 8], "Calibri", 8, false);
        SetAlignment(sheet.Cells[sign2, lastCol - 8], ExcelHorizontalAlignment.Left, ExcelVerticalAlignment.Center);

        sheet.Cells[under2, lastCol - 8, under2, lastCol].Merge = true;
        sheet.Cells[under2, lastCol - 8, under2, lastCol].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

        sheet.Cells[fio2, lastCol - 4, fio2, lastCol - 2].Merge = true;
        sheet.Cells[fio2, lastCol - 4].Value = "(ФИО)";
        SetFont(sheet.Cells[fio2, lastCol - 4], "Calibri", 8, false);
        SetAlignment(sheet.Cells[fio2, lastCol - 4], ExcelHorizontalAlignment.Center, ExcelVerticalAlignment.Center);

        sheet.Cells[fio2, lastCol - 1, fio2, lastCol].Merge = true;
        sheet.Cells[fio2, lastCol - 1].Value = "(подпись)";
        SetFont(sheet.Cells[fio2, lastCol - 1], "Calibri", 8, false);
        SetAlignment(sheet.Cells[fio2, lastCol - 1], ExcelHorizontalAlignment.Center, ExcelVerticalAlignment.Center);

        // Автоширина
        sheet.Cells.AutoFitColumns(0);
    }

    // ==================== Чтение данных с автоопределением формата ====================

    private (string? orgName, string? orgCode) ReadDetData(string filePath)
    {
        try
        {
            // Пробуем через ExcelDataReader (для обычных .xls/.xlsx)
            return ReadDetDataExcel(filePath);
        }
        catch (Exception ex) when (ex.Message.Contains("Invalid file signature") || ex is InvalidDataException)
        {
            log("  Это не стандартный Excel, пробуем парсить как XML Spreadsheet ML...");
            if (IsExcelXml(filePath))
                return ReadDetDataXml(filePath);
            throw new Exception("Не удалось распознать формат файла");
        }
    }

    private List<string> ReadSLList(string filePath)
    {
        try
        {
            return ReadSLListExcel(filePath);
        }
        catch (Exception ex) when (ex.Message.Contains("Invalid file signature") || ex is InvalidDataException)
        {
            log("  Это не стандартный Excel, пробуем парсить как XML Spreadsheet ML...");
            if (IsExcelXml(filePath))
                return ReadSLListXml(filePath);
            throw new Exception("Не удалось распознать формат файла");
        }
    }

    // ---- Проверка на XML-таблицу Excel ----
    private bool IsExcelXml(string filePath)
    {
        try
        {
            string content = File.ReadAllText(filePath);
            return content.Contains("<Workbook", StringComparison.Ordinal) &&
                   content.Contains("urn:schemas-microsoft-com:office:spreadsheet", StringComparison.Ordinal);
        }
        catch { return false; }
    }

    // ---- Чтение через ExcelDataReader (для обычных файлов) ----
    private (string? orgName, string? orgCode) ReadDetDataExcel(string filePath)
    {
        using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
        using (var reader = ExcelReaderFactory.CreateReader(stream))
        {
            bool found = false;
            do
            {
                if (reader.Name == "ТД") { found = true; break; }
            } while (reader.NextResult());
            if (!found) throw new Exception("Лист 'ТД' не найден");

            int rowIndex = 0;
            string? orgName = null, orgCode = null;
            while (reader.Read())
            {
                if (rowIndex == 3 && reader.FieldCount > 0)
                {
                    string val = reader.GetString(0) ?? "";
                    orgName = val.Replace("Наименование Получателя:  ", "").Trim();
                }
                else if (rowIndex == 4 && reader.FieldCount > 0)
                {
                    string val = reader.GetString(0) ?? "";
                    string raw = val.Replace("Код ЭО:  ", "").Trim();
                    if (raw.Contains(';')) orgCode = raw.Split(';')[0].Trim();
                    else orgCode = raw;
                }
                rowIndex++;
            }
            return (orgName, orgCode);
        }
    }

    private List<string> ReadSLListExcel(string filePath)
    {
        var result = new List<string>();
        using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
        using (var reader = ExcelReaderFactory.CreateReader(stream))
        {
            bool found = false;
            do
            {
                if (reader.Name == "Детализация") { found = true; break; }
            } while (reader.NextResult());
            if (!found) throw new Exception("Лист 'Детализация' не найден");

            int rowIndex = 0;
            while (reader.Read())
            {
                if (rowIndex >= 4 && reader.FieldCount > 1)
                {
                    string val = reader.GetString(1) ?? "";
                    if (!string.IsNullOrEmpty(val) && !result.Contains(val))
                        result.Add(val);
                }
                rowIndex++;
            }
            if (result.Count > 0 && string.IsNullOrEmpty(result[^1]))
                result.RemoveAt(result.Count - 1);
            return result;
        }
    }

    // ---- Парсинг XML-таблицы Excel (Spreadsheet ML) ----
    private (string? orgName, string? orgCode) ReadDetDataXml(string filePath)
    {
        var doc = XDocument.Load(filePath);
        var sheet = doc.Descendants(ss + "Worksheet")
                       .FirstOrDefault(w => (string?)w.Attribute(ss + "Name") == "ТД");
        if (sheet == null) throw new Exception("Лист 'ТД' не найден в XML");

        var rows = sheet.Descendants(ss + "Row").ToList();
        if (rows.Count < 5) throw new Exception("Недостаточно строк в листе ТД");

        // Строка 3 (индекс 3) – Наименование Получателя
        string? orgName = null;
        var row3 = rows[3];
        var cells3 = row3.Descendants(ss + "Cell").ToList();
        if (cells3.Count > 0)
        {
            var data = cells3[0].Descendants(ss + "Data").FirstOrDefault();
            if (data != null)
            {
                string val = data.Value.Trim();
                orgName = val.Replace("Наименование Получателя:", "").Replace("&#160;", " ").Trim();
            }
        }

        // Строка 4 (индекс 4) – Код ЭО
        string? orgCode = null;
        var row4 = rows[4];
        var cells4 = row4.Descendants(ss + "Cell").ToList();
        if (cells4.Count > 0)
        {
            var data = cells4[0].Descendants(ss + "Data").FirstOrDefault();
            if (data != null)
            {
                string val = data.Value.Trim();
                string raw = val.Replace("Код ЭО:", "").Replace("&#160;", " ").Trim();
                if (raw.Contains(';')) orgCode = raw.Split(';')[0].Trim();
                else orgCode = raw;
            }
        }

        if (string.IsNullOrEmpty(orgName) || string.IsNullOrEmpty(orgCode))
            throw new Exception("Не удалось извлечь наименование или код из XML");
        return (orgName, orgCode);
    }

    private List<string> ReadSLListXml(string filePath)
    {
        var doc = XDocument.Load(filePath);
        var sheet = doc.Descendants(ss + "Worksheet")
                       .FirstOrDefault(w => (string?)w.Attribute(ss + "Name") == "Детализация");
        if (sheet == null) throw new Exception("Лист 'Детализация' не найден в XML");

        var rows = sheet.Descendants(ss + "Row").ToList();
        var result = new List<string>();
        for (int i = 4; i < rows.Count; i++)
        {
            var cells = rows[i].Descendants(ss + "Cell").ToList();
            if (cells.Count > 1)
            {
                var data = cells[1].Descendants(ss + "Data").FirstOrDefault();
                if (data != null)
                {
                    string val = data.Value.Trim();
                    if (!string.IsNullOrEmpty(val) && !result.Contains(val))
                        result.Add(val);
                }
            }
        }
        if (result.Count > 0 && string.IsNullOrEmpty(result[^1]))
            result.RemoveAt(result.Count - 1);
        return result;
    }
}
