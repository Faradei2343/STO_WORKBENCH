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
    public class ExcelService
    {
        public event Action<int, string> OnProgress;
        public event Action<bool, string> OnCompleted;

        public void Generate(GenerationOptions opts, CancellationToken ct)
        {
            Excel.Application excelApp = null;
            Excel.Workbook templateWorkbook = null;

            try
            {
                excelApp = new Excel.Application();
                excelApp.Visible = false;
                excelApp.DisplayAlerts = false;

                templateWorkbook = excelApp.Workbooks.Open(opts.ExcelFilePath);
                Excel.Worksheet sheet = templateWorkbook.Sheets["Заявка №"] as Excel.Worksheet;
                if (sheet == null)
                    throw new Exception("Лист 'Заявка №' не найден в файле.");

                Excel.Range foundCell = sheet.Cells.Find(
                    What: "МФ*",
                    LookIn: Excel.XlFindLookIn.xlValues,
                    LookAt: Excel.XlLookAt.xlPart
                );
                if (foundCell == null)
                    throw new Exception("Не найдена ячейка с номером заявки (шаблон 'МФ...').");

                string outputDir = Path.GetDirectoryName(opts.ExcelFilePath);
                int total = opts.EndCount - opts.StartCount + 1;
                int current = 0;

                for (int i = opts.StartCount; i <= opts.EndCount; i++)
                {
                    ct.ThrowIfCancellationRequested();

                    current++;
                    string requestNumber = $"МФ{i}";
                    Report(current, $"[{current}/{total}] Обработка {requestNumber}...");

                    foundCell.Value = requestNumber;
                    excelApp.CalculateFull();

                    // Экспорт PDF
                    string pdfPath = Path.Combine(outputDir, $"{requestNumber}.pdf");
                    sheet.ExportAsFixedFormat(
                        Excel.XlFixedFormatType.xlTypePDF,
                        pdfPath,
                        Excel.XlFixedFormatQuality.xlQualityStandard,
                        true, false,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing
                    );

                    ct.ThrowIfCancellationRequested();

                    // Создаём XLSX без картинок
                    string xlsxPath = Path.Combine(outputDir, $"{requestNumber}.xlsx");
                    sheet.Copy();
                    Excel.Workbook newWorkbook = excelApp.ActiveWorkbook;
                    Excel.Worksheet newSheet = newWorkbook.ActiveSheet as Excel.Worksheet;

                    for (int s = newSheet.Shapes.Count; s >= 1; s--)
                    {
                        Excel.Shape shape = newSheet.Shapes.Item(s);
                        shape.Delete();
                        Marshal.ReleaseComObject(shape);
                    }

                    newWorkbook.SaveAs(xlsxPath, Excel.XlFileFormat.xlOpenXMLWorkbook);
                    newWorkbook.Close();
                    Marshal.ReleaseComObject(newSheet);
                    Marshal.ReleaseComObject(newWorkbook);

                    Report(current, $"[{current}/{total}] {requestNumber}: PDF и XLSX сохранены.");
                }

                OnCompleted?.Invoke(true, $"Готово. Создано заявок: {total}.");
            }
            catch (OperationCanceledException)
            {
                OnCompleted?.Invoke(false, "Генерация отменена.");
            }
            catch (Exception ex)
            {
                OnCompleted?.Invoke(false, $"Ошибка: {ex.Message}");
            }
            finally
            {
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

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        private void Report(int step, string message) =>
            OnProgress?.Invoke(step, message);
    }
}
