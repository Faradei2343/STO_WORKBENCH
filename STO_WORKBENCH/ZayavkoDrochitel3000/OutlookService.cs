using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace STO_WORKBENCH.ZayavkoDrochitel3000
{
    public class OutlookService
    {
        public event Action<int, string> OnProgress;
        public event Action<bool, string> OnCompleted;

        public void Send(SendOptions opts, CancellationToken ct)
        {
            Outlook.Application outlookApp = null;

            try
            {
                outlookApp = new Outlook.Application();

                int total = opts.EndCount - opts.StartCount + 1;
                int current = 0;
                int skipped = 0;

                for (int i = opts.StartCount; i <= opts.EndCount; i++)
                {
                    ct.ThrowIfCancellationRequested();

                    current++;
                    string requestNumber = $"МФ{i}";
                    string pdfPath = Path.Combine(opts.FilesDir, $"{requestNumber}.pdf");
                    string xlsxPath = Path.Combine(opts.FilesDir, $"{requestNumber}.xlsx");

                    // Пропускаем, если файлы не найдены
                    if (!File.Exists(pdfPath) || !File.Exists(xlsxPath))
                    {
                        skipped++;
                        Report(current, $"[{current}/{total}] {requestNumber}: файлы не найдены, пропускаем.");
                        continue;
                    }

                    Report(current, $"[{current}/{total}] Отправка {requestNumber}...");

                    Outlook.MailItem mail = (Outlook.MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);
                    mail.To = opts.RecipientEmail;
                    if (!string.IsNullOrWhiteSpace(opts.CcEmail))
                        mail.CC = opts.CcEmail;

                    mail.Subject = $"Заявка в СЦ от Мурманского филиала. {requestNumber}";
                    mail.Body =
                        $"Направляю заявки в СЦ от Мурманского филиала\n\n" +
                        $"Во вложении заявка {requestNumber} в форматах PDF и XLSX.\n\n" +
                        "С уважением.\r\nФедирчик Юрий Андреевич\r\n" +
                        "Главный региональный инженер \r\nМурманского филиала АО «Воентелеком» -\r\n" +
                        "826 РЗСС\r\nРаб.: 8(815)255 25 01 (доб. 51816)\r\n" +
                        "Моб.: 8(921)156 60 30\r\nE-mail: y.fedirchik@voentelecom.ru\r\n" +
                        "183035 г. Мурманск, ул. А. Невского, д. 55\r\n";

                    mail.Attachments.Add(pdfPath, Outlook.OlAttachmentType.olByValue, Type.Missing, Type.Missing);
                    mail.Attachments.Add(xlsxPath, Outlook.OlAttachmentType.olByValue, Type.Missing, Type.Missing);
                    mail.Send();

                    string ccInfo = string.IsNullOrWhiteSpace(opts.CcEmail) ? "" : $", копия: {opts.CcEmail}";
                    Report(current, $"[{current}/{total}] {requestNumber}: отправлено на {opts.RecipientEmail}{ccInfo}.");

                    Marshal.ReleaseComObject(mail);
                    Thread.Sleep(1000);
                }

                string summary = skipped > 0
                    ? $"Отправка завершена. Отправлено: {total - skipped}, пропущено (нет файлов): {skipped}."
                    : $"Отправка завершена. Отправлено: {total}.";

                OnCompleted?.Invoke(true, summary);
            }
            catch (OperationCanceledException)
            {
                OnCompleted?.Invoke(false, "Отправка отменена.");
            }
            catch (Exception ex)
            {
                OnCompleted?.Invoke(false, $"Ошибка: {ex.Message}");
            }
            finally
            {
                if (outlookApp != null)
                    Marshal.ReleaseComObject(outlookApp);

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        private void Report(int step, string message) =>
            OnProgress?.Invoke(step, message);
    }
}