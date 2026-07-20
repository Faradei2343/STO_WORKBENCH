using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STO_WORKBENCH.ZayavkoDrochitel3000
{
    public partial class FormZayavkodrochitel3000 : Form
    {
        private CancellationTokenSource _cts;

        public FormZayavkodrochitel3000()
        {
            InitializeComponent();
        }


// ===================== ВКЛАДКА "ГЕНЕРАЦИЯ" =====================

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

        private async void btnGenerate_Click(object sender, EventArgs e)
        {
            if (!ValidateGenerationInput(out var opts)) return;

            int total = opts.EndCount - opts.StartCount + 1;
            PrepareProgress(pgGenerate, total);
            txtGenLog.Clear();
            SetGenerateUiRunning(true);
          
            _cts = new CancellationTokenSource();
            var service = new ExcelService();

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

            await Task.Run(() => service.Generate(opts, _cts.Token));
        }

        private void btnCancelGenerate_Click(object sender, EventArgs e)
        {
            _cts?.Cancel();
            btnCancelGenerate.Enabled = false;
        }

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

        private void btnBrowseDir_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Выберите папку с PDF и XLSX файлами";
                if (dlg.ShowDialog() == DialogResult.OK)
                    txtFilesDir.Text = dlg.SelectedPath;
            }
        }

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

        private void btnCancelSend_Click(object sender, EventArgs e)
        {
            _cts?.Cancel();
            btnCancelSend.Enabled = false;
        }

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

        private static void AppendLog(System.Windows.Forms.TextBox log, string message)
        {
            log.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
            log.ScrollToCaret();
        }

        private static void PrepareProgress(System.Windows.Forms.ProgressBar bar, int total)
        {
            bar.Minimum = 0;
            bar.Maximum = total;
            bar.Value = 0;
        }

        private void lblGenEnd_Click(object sender, EventArgs e)
        {

        }
    }
}
