using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STO_WORKBENCH.ZayavkoDrochitel3000
{
    // Главная форма модуля «Заявки МФ» (ZayavkoDrochitel3000).
    // Двухвкладочный интерфейс: «Генерация» создаёт PDF/XLSX заявок из Excel-шаблона,
    // «Отправка» рассылает готовые файлы по e-mail через Outlook.
    // Тяжёлые операции (Excel/Outlook Interop) выполняются в фоновых задачах Task.Run,
    // чтобы не блокировать UI; результаты возвращаются в UI-поток через Control.Invoke.
    public partial class FormZayavkodrochitel3000 : Form
    {
        // Источник токена отмены для текущей фоновой операции (генерация или отправка).
        // Создаётся при старте операции, отменяется кнопкой «Отмена».
        private CancellationTokenSource _cts;

        // Конструктор формы: разметка и контролы создаются в InitializeComponent()
        // (автогенерируемая часть в FormZayavkodrochitel3000.Designer.cs).
        public FormZayavkodrochitel3000()
        {
            InitializeComponent();
        }


// ===================== ВКЛАДКА "ГЕНЕРАЦИЯ" =====================

        // Обработчик кнопки «Обзор...» рядом с полем пути к Excel-шаблону.
        // Открывает стандартный диалог выбора файла; при подтверждении пишет путь в txtExcelPath.
        private void btnBrowseExcel_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = "Выберите файл Excel";
                dlg.Filter = "Excel файлы (*.xlsx;*.xls;*.xlsm)|*.xlsx;*.xls;*.xlsm|Все файлы|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                    txtExcelPath.Text = dlg.FileName;
            }
        }

        // Обработчик кнопки «Создать файлы» (вкладка «Генерация»).
        // Валидирует ввод, готовит прогресс и лог, затем запускает ExcelService.Generate
        // в фоновом потоке. Обработчики событий сервиса маршалятся в UI-поток через this.Invoke,
        // так как сервис дёргает их из рабочего потока (прямое обращение к контролам
        // из чужого потока упало бы с InvalidOperationException).
        private async void btnGenerate_Click(object sender, EventArgs e)
        {
            if (!ValidateGenerationInput(out var opts)) return;

            int total = opts.EndCount - opts.StartCount + 1;
            PrepareProgress(pgGenerate, total);
            txtGenLog.Clear();
            SetGenerateUiRunning(true);
          
            _cts = new CancellationTokenSource();
            var service = new ExcelService();

            // Прогресс: двигаем ProgressBar, обновляем счётчик «N / total» и пишем сообщение в лог.
            service.OnProgress += (step, msg) => this.Invoke((Action)(() =>
            {
                pgGenerate.Value = Math.Min(step, total);
                lblGenProgress.Text = $"{step} / {total}";
                AppendLog(txtGenLog, msg);
            }));

            // Завершение: финальная запись в лог, разблокировка UI, доводка прогресса до 100%
            // и показ итогового MessageBox (иконка зависит от успеха).
            service.OnCompleted += (success, msg) => this.Invoke((Action)(() =>
            {
                AppendLog(txtGenLog, msg);
                SetGenerateUiRunning(false);
                if (success)
                {
                    pgGenerate.Value = total;
                    lblGenProgress.Text = $"{total} / {total}";
                }
                MessageBox.Show(msg, success ? "Готово" : "Завершено",
                    MessageBoxButtons.OK,
                    success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            }));

            // Сам запуск фоновой генерации; async/await позволяет UI оставаться отзывчивым.
            await Task.Run(() => service.Generate(opts, _cts.Token));
        }

        // Обработчик кнопки «Отмена» (генерация): сигналит токену отмены,
        // а саму кнопку блокирует, чтобы повторные клики не порождали лишних вызовов Cancel.
        private void btnCancelGenerate_Click(object sender, EventArgs e)
        {
            _cts?.Cancel();
            btnCancelGenerate.Enabled = false;
        }

        // Валидация полей вкладки «Генерация»: файл должен существовать,
        // начальный номер — положительное число, конечный — не меньше начального.
        // При ошибке показывает MessageBox и возвращает false; при успехе заполняет opts.
        private bool ValidateGenerationInput(out GenerationOptions opts)
        {
            opts = null;
            if (string.IsNullOrWhiteSpace(txtExcelPath.Text) || !File.Exists(txtExcelPath.Text))
            {
                MessageBox.Show("Укажите существующий файл Excel.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!int.TryParse(txtGenStart.Text, out int start) || start <= 0)
            {
                MessageBox.Show("Начальный номер должен быть положительным числом.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!int.TryParse(txtGenEnd.Text, out int end) || end < start)
            {
                MessageBox.Show("Конечный номер должен быть >= начального.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            opts = new GenerationOptions
            {
                ExcelFilePath = txtExcelPath.Text.Trim(),
                StartCount = start,
                EndCount = end
            };
            return true;
        }

        // Переключает состояние элементов вкладки «Генерация»:
        // пока идёт фоновая работа — поля ввода и кнопку запуска блокируем, «Отмену» включаем.
        private void SetGenerateUiRunning(bool running)
        {
            btnGenerate.Enabled = !running;
            btnCancelGenerate.Enabled = running;
            txtExcelPath.Enabled = !running;
            btnBrowseExcel.Enabled = !running;
            txtGenStart.Enabled = !running;
            txtGenEnd.Enabled = !running;
        }

        // ===================== ВКЛАДКА "ОТПРАВКА" =====================

        // Обработчик кнопки «Обзор...» рядом с полем папки с готовыми файлами.
        // Открывает FolderBrowserDialog; при подтверждении пишет путь в txtFilesDir.
        private void btnBrowseDir_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Выберите папку с PDF и XLSX файлами";
                if (dlg.ShowDialog() == DialogResult.OK)
                    txtFilesDir.Text = dlg.SelectedPath;
            }
        }

        // Обработчик кнопки «Отправить письма» (вкладка «Отправка»).
        // Логика зеркальна btnGenerate_Click: валидация, подготовка прогресса/лога,
        // подписка на события OutlookService с маршалингом в UI-поток и запуск в Task.Run.
        private async void btnSend_Click(object sender, EventArgs e)
        {
            if (!ValidateSendInput(out var opts)) return;

            int total = opts.EndCount - opts.StartCount + 1;
            PrepareProgress(pgSend, total);
            txtSendLog.Clear();
            SetSendUiRunning(true);

            _cts = new CancellationTokenSource();
            var service = new OutlookService();

            service.OnProgress += (step, msg) => this.Invoke((Action)(() =>
            {
                pgGenerate.Value = Math.Min(step, total);
                lblGenProgress.Text = $"{step} / {total}";
                AppendLog(txtGenLog, msg);
            }));

            service.OnCompleted += (success, msg) => this.Invoke((Action)(() =>
            {
                AppendLog(txtGenLog, msg);
                SetGenerateUiRunning(false);
                if (success)
                {
                    pgGenerate.Value = total;
                    lblGenProgress.Text = $"{total} / {total}";
                }
                MessageBox.Show(msg, success ? "Готово" : "Завершено",
                    MessageBoxButtons.OK,
                    success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            }));

            await Task.Run(() => service.Send(opts, _cts.Token));
        }

        // Обработчик кнопки «Отмена» (отправка): аналог btnCancelGenerate_Click —
        // отменяет текущую фоновую задачу и блокирует саму себя.
        private void btnCancelSend_Click(object sender, EventArgs e)
        {
            _cts?.Cancel();
            btnCancelSend.Enabled = false;
        }

        // Валидация полей вкладки «Отправка»: папка должна существовать,
        // номера — положительные и в правильном порядке, обязателен email получателя
        // (email копии необязателен). При успехе заполняет opts.
        private bool ValidateSendInput(out SendOptions opts)
        {
            opts = null;
            if (string.IsNullOrWhiteSpace(txtFilesDir.Text) || !Directory.Exists(txtFilesDir.Text))
            {
                MessageBox.Show("Укажите существующую папку с файлами.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!int.TryParse(txtSendStart.Text, out int start) || start <= 0)
            {
                MessageBox.Show("Начальный номер должен быть положительным числом.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!int.TryParse(txtSendEnd.Text, out int end) || end < start)
            {
                MessageBox.Show("Конечный номер должен быть >= начального.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtRecipient.Text))
            {
                MessageBox.Show("Укажите email основного получателя.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            opts = new SendOptions
            {
                FilesDir = txtFilesDir.Text.Trim(),
                StartCount = start,
                EndCount = end,
                RecipientEmail = txtRecipient.Text.Trim(),
                CcEmail = txtCc.Text.Trim()
            };
            return true;
        }

        // Переключает состояние элементов вкладки «Отправка» на время фоновой работы:
        // блокирует кнопку отправки и все поля, включает кнопку отмены.
        private void SetSendUiRunning(bool running)
        {
            btnSend.Enabled = !running;
            btnCancelSend.Enabled = running;
            txtFilesDir.Enabled = !running;
            btnBrowseDir.Enabled = !running;
            txtSendStart.Enabled = !running;
            txtSendEnd.Enabled = !running;
            txtRecipient.Enabled = !running;
            txtCc.Enabled = !running;
        }

        // ===================== ОБЩИЕ ХЕЛПЕРЫ =====================

        // Дописывает строку в многострочный TextBox-лог с меткой времени [HH:mm:ss]
        // и прокручивает каретку вниз, чтобы всегда было видно последнее сообщение.
        private static void AppendLog(System.Windows.Forms.TextBox log, string message)
        {
            log.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
            log.ScrollToCaret();
        }

        // Сбрасывает ProgressBar перед началом операции: диапазон 0..total, текущее значение 0.
        private static void PrepareProgress(System.Windows.Forms.ProgressBar bar, int total)
        {
            bar.Minimum = 0;
            bar.Maximum = total;
            bar.Value = 0;
        }

        // Пустой обработчик клика по метке lblGenEnd (автогенерированный дизайнером,
        // подписка висит в Designer.cs). Осознанно ничего не делает — оставлен для совместимости.
        private void lblGenEnd_Click(object sender, EventArgs e)
        {

        }
    }
}
