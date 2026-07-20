using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using static STO_WORKBENCH.MultiBurner.Models;

namespace STO_WORKBENCH.MultiBurner
{
    public partial class BurnerForm : Form
    {
        private List<DriveInfoModel> _drives = new List<DriveInfoModel>();
        private ConcurrentQueue<BurnJob> _jobQueue;
        private CancellationTokenSource _cts;

        private readonly object _gridLock = new object();
        public BurnerForm()
        {
            InitializeComponent();
            InitGrid();
            InitState();
        }

        private void InitState()
        {
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void InitGrid()
        {
            gridJobs.Columns.Clear();

            DataGridViewCheckBoxColumn colUse = new DataGridViewCheckBoxColumn();
            colUse.Name = "Use";
            colUse.HeaderText = "Выбрать";
            colUse.Width = 70;
            colUse.FillWeight = 55;
            gridJobs.Columns.Add(colUse);

            DataGridViewTextBoxColumn colFolder = new DataGridViewTextBoxColumn();
            colFolder.Name = "Folder";
            colFolder.HeaderText = "Папка";
            colFolder.ReadOnly = true;
            colFolder.FillWeight = 180;
            gridJobs.Columns.Add(colFolder);

            DataGridViewTextBoxColumn colPath = new DataGridViewTextBoxColumn();
            colPath.Name = "Path";
            colPath.HeaderText = "Путь";
            colPath.ReadOnly = true;
            colPath.Visible = false;
            gridJobs.Columns.Add(colPath);

            DataGridViewTextBoxColumn colSize = new DataGridViewTextBoxColumn();
            colSize.Name = "Size";
            colSize.HeaderText = "Размер";
            colSize.ReadOnly = true;
            colSize.FillWeight = 80;
            gridJobs.Columns.Add(colSize);

            DataGridViewTextBoxColumn colStatus = new DataGridViewTextBoxColumn();
            colStatus.Name = "Status";
            colStatus.HeaderText = "Статус";
            colStatus.ReadOnly = true;
            colStatus.FillWeight = 90;
            gridJobs.Columns.Add(colStatus);

            DataGridViewTextBoxColumn colDrive = new DataGridViewTextBoxColumn();
            colDrive.Name = "Drive";
            colDrive.HeaderText = "Привод";
            colDrive.ReadOnly = true;
            colDrive.FillWeight = 140;
            gridJobs.Columns.Add(colDrive);

            DataGridViewTextBoxColumn colError = new DataGridViewTextBoxColumn();
            colError.Name = "Error";
            colError.HeaderText = "Ошибка";
            colError.ReadOnly = true;
            colError.FillWeight = 160;
            gridJobs.Columns.Add(colError);

            gridJobs.AllowUserToAddRows = false;
            gridJobs.AllowUserToDeleteRows = false;
            gridJobs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridJobs.MultiSelect = true;
            gridJobs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnSelectRootFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = "Выберите папку с подготовленными подпапками для записи";

            if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
                return;

            LoadSubFolders(folderBrowserDialog1.SelectedPath);
        }

        private void LoadSubFolders(string rootPath)
        {
            txtRootFolder.Text = rootPath;
            gridJobs.Rows.Clear();

            string[] folders;

            try
            {
                folders = Directory.GetDirectories(rootPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось прочитать выбранную папку:" + Environment.NewLine + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            foreach (string folder in folders.OrderBy(x => x))
            {
                string folderName = Path.GetFileName(folder);
                string sizeText = "н/д";

                try
                {
                    long size = ImapiBurnService.GetDirectorySize(folder);
                    sizeText = FormatBytes(size);
                }
                catch
                {
                    sizeText = "ошибка";
                }

                int rowIndex = gridJobs.Rows.Add();
                DataGridViewRow row = gridJobs.Rows[rowIndex];

                row.Cells["Use"].Value = true;
                row.Cells["Folder"].Value = folderName;
                row.Cells["Path"].Value = folder;
                row.Cells["Size"].Value = sizeText;
                row.Cells["Status"].Value = "Ожидает";
                row.Cells["Drive"].Value = "";
                row.Cells["Error"].Value = "";
            }

            AddLogSafe("Загружено папок: " + folders.Length);
        }

        private void btnDetectDrives_Click(object sender, EventArgs e)
        {
            try
            {
                checkedDrives.Items.Clear();

                _drives = ImapiDriveDetector.GetRecorders();

                foreach (DriveInfoModel drive in _drives)
                {
                    checkedDrives.Items.Add(drive, true);
                }

                AddLogSafe("Найдено приводов: " + _drives.Count);

                if (_drives.Count == 0)
                {
                    MessageBox.Show(
                        "Пишущие приводы не найдены.",
                        "Приводы",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка поиска приводов:" + Environment.NewLine + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                AddLogSafe("Ошибка поиска приводов: " + ex.Message);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            List<BurnJob> selectedJobs = GetSelectedJobs();
            List<DriveInfoModel> selectedDrives = GetSelectedDrives();

            if (selectedJobs.Count == 0)
            {
                MessageBox.Show(
                    "Не выбраны папки для записи.",
                    "Проверка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (selectedDrives.Count == 0)
            {
                MessageBox.Show(
                    "Не выбраны приводы.",
                    "Проверка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Будет запущена запись." + Environment.NewLine +
                "Папок: " + selectedJobs.Count + Environment.NewLine +
                "Приводов: " + selectedDrives.Count + Environment.NewLine + Environment.NewLine +
                "Продолжить?",
                "Запуск записи",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            if (!CheckFolderSizes(selectedJobs))
                return;

            _jobQueue = new ConcurrentQueue<BurnJob>(selectedJobs);
            _cts = new CancellationTokenSource();

            btnStart.Enabled = false;
            btnStop.Enabled = true;
            btnSelectRootFolder.Enabled = false;
            btnDetectDrives.Enabled = false;
            checkedDrives.Enabled = false;

            AddLogSafe("Запись запущена. Приводов: " + selectedDrives.Count + ", заданий: " + selectedJobs.Count);

            foreach (DriveInfoModel drive in selectedDrives)
            {
                BurnWorker worker = new BurnWorker(drive, _jobQueue, _cts.Token);

                worker.Log += AddLogSafe;
                worker.JobUpdated += UpdateJobSafe;

                worker.NeedDisc += delegate (string driveName, string folderName)
                {
                    AddLogSafe(driveName + ": вставьте чистый диск для " + folderName);
                    ShowBalloonSafe("Требуется диск", driveName + ": вставьте чистый диск для " + folderName);
                };

                worker.JobCompleted += delegate (string driveName, string folderName)
                {
                    AddLogSafe(driveName + ": диск записан: " + folderName);
                    ShowBalloonSafe("Запись завершена", folderName + " записана на " + driveName);
                };

                worker.JobError += delegate (string folderName, string error)
                {
                    AddLogSafe("Ошибка записи " + folderName + ": " + error);
                    ShowBalloonSafe("Ошибка записи", folderName + ": " + error);
                };

                worker.Start();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (_cts != null)
            {
                _cts.Cancel();
                AddLogSafe("Запрошена остановка. Если диск уже пишется, операция может завершиться не сразу.");
            }

            btnStop.Enabled = false;
            btnStart.Enabled = true;
            btnSelectRootFolder.Enabled = true;
            btnDetectDrives.Enabled = true;
            checkedDrives.Enabled = true;
        }

        private List<BurnJob> GetSelectedJobs()
        {
            List<BurnJob> jobs = new List<BurnJob>();

            foreach (DataGridViewRow row in gridJobs.Rows)
            {
                bool use = false;

                if (row.Cells["Use"].Value != null)
                    bool.TryParse(row.Cells["Use"].Value.ToString(), out use);

                if (!use)
                    continue;

                string path = Convert.ToString(row.Cells["Path"].Value);

                if (string.IsNullOrWhiteSpace(path))
                    continue;

                if (!Directory.Exists(path))
                {
                    row.Cells["Status"].Value = "Ошибка";
                    row.Cells["Error"].Value = "Папка не найдена";
                    continue;
                }

                BurnJob job = new BurnJob
                {
                    FolderPath = path,
                    Status = BurnJobStatus.Waiting,
                    AssignedDrive = "",
                    ErrorMessage = ""
                };

                jobs.Add(job);

                row.Cells["Status"].Value = "В очереди";
                row.Cells["Drive"].Value = "";
                row.Cells["Error"].Value = "";
                row.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            }

            return jobs;
        }

        private List<DriveInfoModel> GetSelectedDrives()
        {
            List<DriveInfoModel> result = new List<DriveInfoModel>();

            foreach (object item in checkedDrives.CheckedItems)
            {
                DriveInfoModel drive = item as DriveInfoModel;

                if (drive != null)
                    result.Add(drive);
            }

            return result;
        }

        private bool CheckFolderSizes(List<BurnJob> selectedJobs)
        {
            // MVP-проверка под обычный DVD-R 4.7 GB.
            // Фактическая полезная емкость обычно около 4.3 GiB.
            // Если будете писать BD-R, этот лимит надо сделать настройкой.
            const long dvdLimit = 4300L * 1024L * 1024L;

            List<string> oversized = new List<string>();

            foreach (BurnJob job in selectedJobs)
            {
                try
                {
                    long size = ImapiBurnService.GetDirectorySize(job.FolderPath);

                    if (size > dvdLimit)
                    {
                        oversized.Add(job.FolderName + " — " + FormatBytes(size));
                    }
                }
                catch
                {
                    oversized.Add(job.FolderName + " — не удалось определить размер");
                }
            }

            if (oversized.Count == 0)
                return true;

            string message =
                "Некоторые папки больше типовой емкости DVD-R 4.7 GB:" +
                Environment.NewLine +
                Environment.NewLine +
                string.Join(Environment.NewLine, oversized.Take(20).ToArray());

            if (oversized.Count > 20)
                message += Environment.NewLine + "... и еще: " + (oversized.Count - 20);

            message += Environment.NewLine + Environment.NewLine + "Продолжить?";

            DialogResult result = MessageBox.Show(
                message,
                "Проверка размера",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            return result == DialogResult.Yes;
        }

        private void UpdateJobSafe(BurnJob job)
        {
            if (gridJobs.InvokeRequired)
            {
                gridJobs.BeginInvoke(new Action<BurnJob>(UpdateJobSafe), job);
                return;
            }

            lock (_gridLock)
            {
                foreach (DataGridViewRow row in gridJobs.Rows)
                {
                    string path = Convert.ToString(row.Cells["Path"].Value);

                    if (!string.Equals(path, job.FolderPath, StringComparison.OrdinalIgnoreCase))
                        continue;

                    row.Cells["Status"].Value = StatusToText(job.Status);
                    row.Cells["Drive"].Value = job.AssignedDrive ?? "";
                    row.Cells["Error"].Value = job.ErrorMessage ?? "";

                    if (job.Status == BurnJobStatus.Waiting)
                    {
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.White;
                    }
                    else if (job.Status == BurnJobStatus.Burning)
                    {
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.LightYellow;
                    }
                    else if (job.Status == BurnJobStatus.Done)
                    {
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
                        row.Cells["Use"].Value = false;
                    }
                    else if (job.Status == BurnJobStatus.Error)
                    {
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.LightPink;
                    }

                    break;
                }
            }

            CheckAllJobsFinishedSafe();
        }

        private void CheckAllJobsFinishedSafe()
        {
            if (gridJobs.InvokeRequired)
            {
                gridJobs.BeginInvoke(new Action(CheckAllJobsFinishedSafe));
                return;
            }

            if (_cts == null)
                return;

            bool hasActive = false;

            foreach (DataGridViewRow row in gridJobs.Rows)
            {
                string status = Convert.ToString(row.Cells["Status"].Value);

                if (status == "В очереди" || status == "Запись")
                {
                    hasActive = true;
                    break;
                }
            }

            if (!hasActive)
            {
                btnStart.Enabled = true;
                btnStop.Enabled = false;
                btnSelectRootFolder.Enabled = true;
                btnDetectDrives.Enabled = true;
                checkedDrives.Enabled = true;

                AddLogSafe("Все доступные задания завершены или обработаны.");
                ShowBalloonSafe("Запись завершена", "Очередь заданий обработана.");
            }
        }

        private string StatusToText(BurnJobStatus status)
        {
            switch (status)
            {
                case BurnJobStatus.Waiting:
                    return "Ожидает";

                case BurnJobStatus.Burning:
                    return "Запись";

                case BurnJobStatus.Done:
                    return "Готово";

                case BurnJobStatus.Error:
                    return "Ошибка";

                default:
                    return status.ToString();
            }
        }

        private void AddLogSafe(string text)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.BeginInvoke(new Action<string>(AddLogSafe), text);
                return;
            }

            string line = string.Format("{0:HH:mm:ss} {1}", DateTime.Now, text);

            txtLog.AppendText(line + Environment.NewLine);

            try
            {
                LogService.Write(text);
            }
            catch
            {
                // Лог в файл не должен ломать работу приложения
            }
        }

        private void ShowBalloonSafe(string title, string text)
        {
            if (notifyIcon1 == null)
                return;

            if (InvokeRequired)
            {
                BeginInvoke(new Action<string, string>(ShowBalloonSafe), title, text);
                return;
            }

            try
            {
                notifyIcon1.BalloonTipTitle = title;
                notifyIcon1.BalloonTipText = text;
                notifyIcon1.ShowBalloonTip(5000);
            }
            catch
            {
                // Уведомления не критичны
            }
        }

        private string FormatBytes(long bytes)
        {
            double value = bytes;

            string[] suffixes =
            {
                "B",
                "KB",
                "MB",
                "GB",
                "TB"
            };

            int suffixIndex = 0;

            while (value >= 1024 && suffixIndex < suffixes.Length - 1)
            {
                value /= 1024;
                suffixIndex++;
            }

            return value.ToString("0.##") + " " + suffixes[suffixIndex];
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_cts != null)
            {
                DialogResult result = MessageBox.Show(
                    "Если сейчас выполняется запись, она может быть прервана некорректно." + Environment.NewLine +
                    "Закрыть приложение?",
                    "Закрытие",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result != DialogResult.Yes)
                {
                    e.Cancel = true;
                    return;
                }

                _cts.Cancel();
            }

            if (notifyIcon1 != null)
                notifyIcon1.Visible = false;

            base.OnFormClosing(e);
        }


    }
}
