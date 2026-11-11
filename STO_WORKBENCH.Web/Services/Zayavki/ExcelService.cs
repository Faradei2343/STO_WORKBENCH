using System.Runtime.InteropServices;

namespace STO_WORKBENCH.Web.Services.Zayavki;

// Сервис генерации заявок «МФ» из Excel-шаблона (порт WinForms-версии).
// Работает через Excel COM late binding (без interop-сборок): для каждого номера диапазона
// подставляет номер в ячейку, принудительно пересчитывает формулы, экспортирует лист в PDF
// и сохраняет копию в XLSX без картинок.
// Метод должен вызываться внутри StaComRunner.RunAsync (Excel требует STA-поток);
// результаты возвращаются через события OnProgress/OnCompleted.
public class ExcelService
{
    // Константы Excel ( late binding — вместо enum из interop-сборки).
    private const int XlTypePDF = 0;           // XlFixedFormatType.xlTypePDF
    private const int XlQualityStandard = 0;   // XlFixedFormatQuality.xlQualityStandard
    private const int XlValues = -4163;        // XlFindLookIn.xlValues
    private const int XlPart = 2;              // XlLookAt.xlPart
    private const int XlWhole = 1;             // XlLookAt.xlWhole
    private const int XlOpenXMLWorkbook = 51;  // XlFileFormat.xlOpenXMLWorkbook

    // Событие промежуточного прогресса: (номер текущего шага, текстовое сообщение для лога).
    public event Action<int, string>? OnProgress;

    // Событие завершения: (успех/отмена, итоговое сообщение). Вызывается всегда — и при успехе, и при ошибке/отмене.
    public event Action<bool, string>? OnCompleted;

    // Основной метод генерации. ct проверяется перед каждой заявкой и после экспорта PDF;
    // при отмене выбрасывает OperationCanceledException, перехваченный ниже.
    public void Generate(GenerationOptions opts, CancellationToken ct)
    {
        dynamic? excelApp = null;
        dynamic? templateWorkbook = null;

        try
        {
            // Создаём невидимый экземпляр Excel и гушим все диалоги (иначе Excel встанет на MsgBox).
            excelApp = ComInterop.Create(ComInterop.ExcelApplication);
            excelApp.Visible = false;
            excelApp.DisplayAlerts = false;

            // Открываем шаблон и ищем лист «Заявка №» — именно он будет экспортироваться.
            templateWorkbook = excelApp.Workbooks.Open(opts.ExcelFilePath);
            dynamic? sheet;
            try
            {
                sheet = templateWorkbook.Sheets["Заявка №"];
            }
            catch
            {
                sheet = null;
            }
            if (sheet is null)
                throw new Exception("Лист 'Заявка №' не найден в файле.");

            // Ищем на листе ячейку с текстом, начинающимся на «МФ» — это ячейка с номером заявки.
            object foundCellObj = sheet.Cells.Find("МФ*", Type.Missing, XlValues, XlPart);
            if (foundCellObj is null)
                throw new Exception("Не найдена ячейка с номером заявки (шаблон 'МФ...').");
            dynamic foundCell = foundCellObj;

            // Первый лист книги — реестр заявок; оттуда берётся суффикс для имени файла.
            dynamic firstSheet = templateWorkbook.Sheets[1];

            // Результаты сохраняем рядом с шаблоном, в ту же папку.
            string outputDir = Path.GetDirectoryName(opts.ExcelFilePath)!;
            int total = opts.EndCount - opts.StartCount + 1;
            int current = 0;

            // Основной цикл: по одной заявке на каждый номер диапазона.
            for (int i = opts.StartCount; i <= opts.EndCount; i++)
            {
                ct.ThrowIfCancellationRequested();

                current++;
                string requestNumber = $"МФ{i}";
                Report(current, $"[{current}/{total}] Обработка {requestNumber}...");

                // Подставляем номер и пересчитываем книгу целиком —
                // зависимые формулы (в т.ч. на первом листе-реестре) обновятся.
                foundCell.Value = requestNumber;
                excelApp.CalculateFull();

                // Значение из ячейки справа от "МФ{i}" на первом листе (реестр)
                string suffix = FindSuffixOnFirstSheet(firstSheet, requestNumber);
                string baseName = string.IsNullOrEmpty(suffix)
                    ? requestNumber
                    : $"{requestNumber} {suffix}";
                // Заменяем символы, запрещённые в именах файлов Windows, на подчёркивания.
                foreach (char c in Path.GetInvalidFileNameChars())
                    baseName = baseName.Replace(c, '_');

                // Экспорт PDF
                string pdfPath = Path.Combine(outputDir, $"{baseName}.pdf");
                sheet.ExportAsFixedFormat(
                    XlTypePDF,
                    pdfPath,
                    XlQualityStandard,
                    true, false,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing
                );

                ct.ThrowIfCancellationRequested();

                // Создаём XLSX без картинок: копируем лист в новую книгу,
                // удаляем из неё все фигуры/картинки и сохраняем в формате Open XML (.xlsx).
                string xlsxPath = Path.Combine(outputDir, $"{baseName}.xlsx");
                sheet.Copy();
                dynamic newWorkbook = excelApp.ActiveWorkbook;
                dynamic newSheet = newWorkbook.ActiveSheet;

                // Идём от конца к началу, чтобы индексы не сбивались при удалении.
                int shapesCount = newSheet.Shapes.Count;
                for (int s = shapesCount; s >= 1; s--)
                {
                    dynamic shape = newSheet.Shapes.Item(s);
                    shape.Delete();
                    Marshal.ReleaseComObject(shape);
                }

                newWorkbook.SaveAs(xlsxPath, XlOpenXMLWorkbook);
                newWorkbook.Close();
                // Явно отпускаем COM-объекты новой книги, чтобы не копить неуправляемые ссылки.
                Marshal.ReleaseComObject(newSheet);
                Marshal.ReleaseComObject(newWorkbook);

                Report(current, $"[{current}/{total}] {baseName}: PDF и XLSX сохранены.");
            }

            OnCompleted?.Invoke(true, $"Готово. Создано заявок: {total}.");
        }
        catch (OperationCanceledException)
        {
            // Отмена по токену — не ошибка, просто сообщаем о прерывании.
            OnCompleted?.Invoke(false, "Генерация отменена.");
        }
        catch (Exception ex)
        {
            // Любая ошибка COM/файловой системы превращается в сообщение для пользователя.
            OnCompleted?.Invoke(false, $"Ошибка: {ex.Message}");
        }
        finally
        {
            // Гарантированная очистка COM-ресурсов: закрываем книгу без сохранения,
            // выходим из Excel и отпускаем обёртки RCW.
            try
            {
                if (templateWorkbook is not null)
                {
                    templateWorkbook.Close(false);
                    Marshal.ReleaseComObject(templateWorkbook);
                }
            }
            catch { }
            try
            {
                if (excelApp is not null)
                {
                    excelApp.Quit();
                    Marshal.ReleaseComObject(excelApp);
                }
            }
            catch { }

            // Принудительный сбор мусора — надёжный способ дождаться финализаторов COM-объектов
            // (иначе процесс EXCEL.EXE может остаться висеть в памяти).
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }

    // Ищет на первом листе (реестре) ячейку с точным номером заявки и возвращает текст
    // из соседней ячейки справа. Возвращает null, если ячейка не найдена или справа пусто.
    private string? FindSuffixOnFirstSheet(dynamic firstSheet, string requestNumber)
    {
        object? found = null;
        object? rightCell = null;
        try
        {
            // Поиск по точному совпадению значения — иначе «МФ10» нашёл бы и «МФ100».
            found = firstSheet.Cells.Find(requestNumber, Type.Missing, XlValues, XlWhole);
            if (found is null)
                return null;

            dynamic foundRange = found;
            dynamic right = foundRange.Offset[0, 1];
            rightCell = right;
            object? value = right.Value2;
            // Редкий случай: Value2 может вернуть Range (объединённые ячейки/формула) —
            // тогда берём отображаемый текст.
            if (value is null || value.GetType().IsCOMObject)
                value = right.Text;

            string? text = Convert.ToString(value)?.Trim();
            return string.IsNullOrEmpty(text) ? null : text;
        }
        finally
        {
            // Отпускаем COM-объекты в обратном порядке создания (сначала правую ячейку, потом найденную).
            if (rightCell is not null)
                Marshal.ReleaseComObject(rightCell);
            if (found is not null)
                Marshal.ReleaseComObject(found);
        }
    }

    private void Report(int step, string message) =>
        OnProgress?.Invoke(step, message);
}
