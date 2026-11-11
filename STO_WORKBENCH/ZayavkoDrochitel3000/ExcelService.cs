using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Excel = Microsoft.Office.Interop.Excel;

namespace STO_WORKBENCH.ZayavkoDrochitel3000
{
    // Сервис генерации заявок «МФ» из Excel-шаблона.
    // Работает через Excel Interop (COM): для каждого номера диапазона подставляет номер в ячейку,
    // принудительно пересчитывает формулы, экспортирует лист в PDF и сохраняет копию в XLSX без картинок.
    // Весь длительный процесс выполняется в фоновом потоке (вызывается через Task.Run из формы),
    // поэтому все обращения к UI идут через события OnProgress/OnCompleted.
    public class ExcelService
    {
        // Событие промежуточного прогресса: (номер текущего шага, текстовое сообщение для лога).
        public event Action<int, string> OnProgress;

        // Событие завершения: (успех/отмена, итоговое сообщение). Вызывается всегда — и при успехе, и при ошибке/отмене.
        public event Action<bool, string> OnCompleted;

        // Основной метод генерации. Аргументы:
        //   opts — параметры генерации (путь к шаблону, диапазон номеров);
        //   ct   — токен отмены; проверяется перед каждой заявкой и после экспорта PDF,
        //          при отмене выбрасывает OperationCanceledException, перехваченный ниже.
        // Метод не возвращает результат — итог сообщается через OnCompleted.
        public void Generate(GenerationOptions opts, CancellationToken ct)
        {
            Excel.Application excelApp = null;
            Excel.Workbook templateWorkbook = null;

            try
            {
                // Создаём невидимый экземпляр Excel и глушим все диалоги (иначе Excel встанет на MsgBox).
                excelApp = new Excel.Application();
                excelApp.Visible = false;
                excelApp.DisplayAlerts = false;

                // Открываем шаблон и ищем лист «Заявка №» — именно он будет экспортироваться.
                templateWorkbook = excelApp.Workbooks.Open(opts.ExcelFilePath);
                Excel.Worksheet sheet = templateWorkbook.Sheets["Заявка №"] as Excel.Worksheet;
                if (sheet == null)
                    throw new Exception("Лист 'Заявка №' не найден в файле.");

                // Ищем на листе ячейку с текстом, начинающимся на «МФ» — это ячейка с номером заявки,
                // в которую далее будут подставляться номера «МФ100», «МФ101» и т.д.
                Excel.Range foundCell = sheet.Cells.Find(
                    What: "МФ*",
                    LookIn: Excel.XlFindLookIn.xlValues,
                    LookAt: Excel.XlLookAt.xlPart
                );
                if (foundCell == null)
                    throw new Exception("Не найдена ячейка с номером заявки (шаблон 'МФ...').");

                // Первый лист книги — реестр заявок; оттуда берётся суффикс для имени файла.
                Excel.Worksheet firstSheet = templateWorkbook.Sheets[1] as Excel.Worksheet;

                // Результаты сохраняем рядом с шаблоном, в ту же папку.
                string outputDir = Path.GetDirectoryName(opts.ExcelFilePath);
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
                    // Параметры ExportAsFixedFormat: тип PDF, путь, качество,
                    // далее — флаги включения свойств документа и т.п., неиспользуемые параметры передаются как Type.Missing.
                    string pdfPath = Path.Combine(outputDir, $"{baseName}.pdf");
                    sheet.ExportAsFixedFormat(
                        Excel.XlFixedFormatType.xlTypePDF,
                        pdfPath,
                        Excel.XlFixedFormatQuality.xlQualityStandard,
                        true, false,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing
                    );

                    ct.ThrowIfCancellationRequested();

                    // Создаём XLSX без картинок
                    // Копируем лист в новую книгу (ActiveWorkbook после Copy — это новая книга),
                    // удаляем из неё все фигуры/картинки и сохраняем в формате Open XML (.xlsx).
                    string xlsxPath = Path.Combine(outputDir, $"{baseName}.xlsx");
                    sheet.Copy();
                    Excel.Workbook newWorkbook = excelApp.ActiveWorkbook;
                    Excel.Worksheet newSheet = newWorkbook.ActiveSheet as Excel.Worksheet;

                    // Идём от конца к началу, чтобы индексы не сбивались при удалении.
                    for (int s = newSheet.Shapes.Count; s >= 1; s--)
                    {
                        Excel.Shape shape = newSheet.Shapes.Item(s);
                        shape.Delete();
                        Marshal.ReleaseComObject(shape);
                    }

                    newWorkbook.SaveAs(xlsxPath, Excel.XlFileFormat.xlOpenXMLWorkbook);
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
                if (templateWorkbook != null)
                {
                    templateWorkbook.Close(false);
                    Marshal.ReleaseComObject(templateWorkbook);
                }
                if (excelApp != null)
                {
                    excelApp.Quit();
                    Marshal.ReleaseComObject(excelApp);
                }

                // Принудительный сбор мусора — надёжный способ дождаться финализаторов COM-объектов
                // (иначе процесс EXCEL.EXE может остаться висеть в памяти).
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        // Ищет на первом листе (реестре) ячейку с точным номером заявки и возвращает текст
        // из соседней ячейки справа. Нужен, чтобы включить в имя файла дополнительное описание
        // заявки из реестра. Возвращает null, если ячейка не найдена или справа пусто.
        private string FindSuffixOnFirstSheet(Excel.Worksheet firstSheet, string requestNumber)
        {
            Excel.Range found = null;
            Excel.Range rightCell = null;
            try
            {
                // Поиск по точному совпадению значения — иначе «МФ10» нашёл бы и «МФ100».
                found = firstSheet.Cells.Find(
                    What: requestNumber,
                    LookIn: Excel.XlFindLookIn.xlValues,
                    LookAt: Excel.XlLookAt.xlWhole
                );
                if (found == null)
                    return null;

                rightCell = found.Offset[0, 1];
                object value = rightCell.Value2;
                // Редкий случай: Value2 может вернуть Range (объединённые ячейки/формула) —
                // тогда берём отображаемый текст.
                if (value == null || value is Excel.Range)
                    value = rightCell.Text;

                string text = Convert.ToString(value)?.Trim();
                return string.IsNullOrEmpty(text) ? null : text;
            }
            finally
            {
                // Отпускаем COM-объекты в обратном порядке создания (сначала правую ячейку, потом найденную).
                if (rightCell != null)
                    Marshal.ReleaseComObject(rightCell);
                if (found != null)
                    Marshal.ReleaseComObject(found);
            }
        }

        // Короткий хелпер-обёртка: безопасно дёргает событие прогресса, если есть подписчики.
        private void Report(int step, string message) =>
            OnProgress?.Invoke(step, message);
    }
}
