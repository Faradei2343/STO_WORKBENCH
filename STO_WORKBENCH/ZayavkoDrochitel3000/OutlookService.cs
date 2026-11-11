using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace STO_WORKBENCH.ZayavkoDrochitel3000
{
    // Сервис отправки готовых заявок «МФ» по электронной почте через Outlook Interop (COM).
    // Для каждого номера диапазона ищет в папке пару файлов «МФNNN.pdf» + «МФNNN.xlsx»,
    // формирует письмо с заданной темой/текстом и вложениями и отправляет его через Outlook
    // (письмо уходит из профиля Outlook пользователя; диалогов безопасности не обходится —
    // при блокировке доступа к Outlook отправка упадёт с ошибкой COM).
    // Как и ExcelService, работает в фоновом потоке и отчитывается через события.
    public class OutlookService
    {
        // Событие промежуточного прогресса: (номер шага, сообщение для лога).
        public event Action<int, string> OnProgress;

        // Событие завершения: (успех/отмена, итоговое сообщение с количеством отправленных/пропущенных).
        public event Action<bool, string> OnCompleted;

        // Основной метод отправки. Аргументы:
        //   opts — папка с файлами, диапазон номеров, адреса получателя и копии;
        //   ct   — токен отмены; проверяется перед каждой отправкой.
        // Результат не возвращается — итог передаётся через OnCompleted.
        public void Send(SendOptions opts, CancellationToken ct)
        {
            Outlook.Application outlookApp = null;

            try
            {
                // Подключаемся к уже запущенному Outlook или запускаем новый экземпляр.
                outlookApp = new Outlook.Application();

                int total = opts.EndCount - opts.StartCount + 1;
                int current = 0;
                int skipped = 0;

                // Цикл по номерам заявок диапазона.
                for (int i = opts.StartCount; i <= opts.EndCount; i++)
                {
                    ct.ThrowIfCancellationRequested();

                    current++;
                    string requestNumber = $"МФ{i}";
                    string pdfPath = Path.Combine(opts.FilesDir, $"{requestNumber}.pdf");
                    string xlsxPath = Path.Combine(opts.FilesDir, $"{requestNumber}.xlsx");

                    // Пропускаем, если файлы не найдены
                    // Такое возможно, если генерация шла не по всем номерам — это не ошибка.
                    if (!File.Exists(pdfPath) || !File.Exists(xlsxPath))
                    {
                        skipped++;
                        Report(current, $"[{current}/{total}] {requestNumber}: файлы не найдены, пропускаем.");
                        continue;
                    }

                    Report(current, $"[{current}/{total}] Отправка {requestNumber}...");

                    // Создаём почтовое сообщение и заполняем адресатов.
                    Outlook.MailItem mail = (Outlook.MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);
                    mail.To = opts.RecipientEmail;
                    if (!string.IsNullOrWhiteSpace(opts.CcEmail))
                        mail.CC = opts.CcEmail;

                    // Тема и текст письма — стандартная подпись отправителя (Федирчик Ю.А.).
                    mail.Subject = $"Заявка в СЦ от Мурманского филиала. {requestNumber}";
                    mail.Body =
                        $"Направляю заявки в СЦ от Мурманского филиала\n\n" +
                        $"Во вложении заявка {requestNumber} в форматах PDF и XLSX.\n\n" +
                        "С уважением.\r\nФедирчик Юрий Андреевич\r\n" +
                        "Главный региональный инженер \r\nМурманского филиала АО «Воентелеком» -\r\n" +
                        "826 РЗСС\r\nРаб.: 8(815)255 25 01 (доб. 51816)\r\n" +
                        "Моб.: 8(921)156 60 30\r\nE-mail: y.fedirchik@voentelecom.ru\r\n" +
                        "183035 г. Мурманск, ул. А. Невского, д. 55\r\n";

                    // Вложения добавляем «по значению» (копия файла в письмо).
                    mail.Attachments.Add(pdfPath, Outlook.OlAttachmentType.olByValue, Type.Missing, Type.Missing);
                    mail.Attachments.Add(xlsxPath, Outlook.OlAttachmentType.olByValue, Type.Missing, Type.Missing);
                    mail.Send();

                    string ccInfo = string.IsNullOrWhiteSpace(opts.CcEmail) ? "" : $", копия: {opts.CcEmail}";
                    Report(current, $"[{current}/{total}] {requestNumber}: отправлено на {opts.RecipientEmail}{ccInfo}.");

                    // Отпускаем COM-объект письма и даём Outlook секунду перед следующим —
                    // без паузы сервер/Outlook может начать отбрасывать письма при массовой отправке.
                    Marshal.ReleaseComObject(mail);
                    Thread.Sleep(1000);
                }

                // Итоговое сообщение отличается, если часть номеров была пропущена.
                string summary = skipped > 0
                    ? $"Отправка завершена. Отправлено: {total - skipped}, пропущено (нет файлов): {skipped}."
                    : $"Отправка завершена. Отправлено: {total}.";

                OnCompleted?.Invoke(true, summary);
            }
            catch (OperationCanceledException)
            {
                // Отмена по токену — сообщаем о прерывании без деталей ошибки.
                OnCompleted?.Invoke(false, "Отправка отменена.");
            }
            catch (Exception ex)
            {
                // Ошибки COM/Outlook (нет профиля, запрет безопасности и т.п.) превращаем в сообщение.
                OnCompleted?.Invoke(false, $"Ошибка: {ex.Message}");
            }
            finally
            {
                // Отпускаем COM-объект Outlook и форсируем сборку мусора,
                // чтобы дождаться финализаторов RCW (иначе OUTLOOK.EXE может остаться висеть).
                if (outlookApp != null)
                    Marshal.ReleaseComObject(outlookApp);

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        // Короткий хелпер: безопасно дёргает событие прогресса, если есть подписчики.
        private void Report(int step, string message) =>
            OnProgress?.Invoke(step, message);
    }
}