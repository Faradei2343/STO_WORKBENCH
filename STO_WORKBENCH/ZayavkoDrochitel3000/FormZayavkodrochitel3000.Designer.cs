namespace STO_WORKBENCH.ZayavkoDrochitel3000
{
    partial class FormZayavkodrochitel3000
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabGenerate;
        private System.Windows.Forms.TabPage tabSend;

        // ── Вкладка "Генерация" ──
        private System.Windows.Forms.GroupBox grpGenFile;
        private System.Windows.Forms.Label lblExcelPath;
        private System.Windows.Forms.TextBox txtExcelPath;
        private System.Windows.Forms.Button btnBrowseExcel;

        private System.Windows.Forms.GroupBox grpGenRange;
        private System.Windows.Forms.Label lblGenStart;
        private System.Windows.Forms.TextBox txtGenStart;
        private System.Windows.Forms.Label lblGenEnd;
        private System.Windows.Forms.TextBox txtGenEnd;

        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnCancelGenerate;
        private System.Windows.Forms.ProgressBar pgGenerate;
        private System.Windows.Forms.Label lblGenProgress;
        private System.Windows.Forms.TextBox txtGenLog;

        // ── Вкладка "Отправка" ──
        private System.Windows.Forms.GroupBox grpSendDir;
        private System.Windows.Forms.Label lblFilesDir;
        private System.Windows.Forms.TextBox txtFilesDir;
        private System.Windows.Forms.Button btnBrowseDir;

        private System.Windows.Forms.GroupBox grpSendRange;
        private System.Windows.Forms.Label lblSendStart;
        private System.Windows.Forms.TextBox txtSendStart;
        private System.Windows.Forms.Label lblSendEnd;
        private System.Windows.Forms.TextBox txtSendEnd;

        private System.Windows.Forms.GroupBox grpSendEmail;
        private System.Windows.Forms.Label lblRecipient;
        private System.Windows.Forms.TextBox txtRecipient;
        private System.Windows.Forms.Label lblCc;
        private System.Windows.Forms.TextBox txtCc;

        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnCancelSend;
        private System.Windows.Forms.ProgressBar pgSend;
        private System.Windows.Forms.Label lblSendProgress;
        private System.Windows.Forms.TextBox txtSendLog;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // ── Создаём все контролы ──────────────────────────────────────
            tabControl = new System.Windows.Forms.TabControl();
            tabGenerate = new System.Windows.Forms.TabPage();
            tabSend = new System.Windows.Forms.TabPage();

            grpGenFile = new System.Windows.Forms.GroupBox();
            lblExcelPath = new System.Windows.Forms.Label();
            txtExcelPath = new System.Windows.Forms.TextBox();
            btnBrowseExcel = new System.Windows.Forms.Button();

            grpGenRange = new System.Windows.Forms.GroupBox();
            lblGenStart = new System.Windows.Forms.Label();
            txtGenStart = new System.Windows.Forms.TextBox();
            lblGenEnd = new System.Windows.Forms.Label();
            txtGenEnd = new System.Windows.Forms.TextBox();

            btnGenerate = new System.Windows.Forms.Button();
            btnCancelGenerate = new System.Windows.Forms.Button();
            pgGenerate = new System.Windows.Forms.ProgressBar();
            lblGenProgress = new System.Windows.Forms.Label();
            txtGenLog = new System.Windows.Forms.TextBox();

            grpSendDir = new System.Windows.Forms.GroupBox();
            lblFilesDir = new System.Windows.Forms.Label();
            txtFilesDir = new System.Windows.Forms.TextBox();
            btnBrowseDir = new System.Windows.Forms.Button();

            grpSendRange = new System.Windows.Forms.GroupBox();
            lblSendStart = new System.Windows.Forms.Label();
            txtSendStart = new System.Windows.Forms.TextBox();
            lblSendEnd = new System.Windows.Forms.Label();
            txtSendEnd = new System.Windows.Forms.TextBox();

            grpSendEmail = new System.Windows.Forms.GroupBox();
            lblRecipient = new System.Windows.Forms.Label();
            txtRecipient = new System.Windows.Forms.TextBox();
            lblCc = new System.Windows.Forms.Label();
            txtCc = new System.Windows.Forms.TextBox();

            btnSend = new System.Windows.Forms.Button();
            btnCancelSend = new System.Windows.Forms.Button();
            pgSend = new System.Windows.Forms.ProgressBar();
            lblSendProgress = new System.Windows.Forms.Label();
            txtSendLog = new System.Windows.Forms.TextBox();

            this.SuspendLayout();

            // =================================================================
            // ВКЛАДКА "ГЕНЕРАЦИЯ"
            // =================================================================

            // ── GroupBox: Исходный файл ───────────────────────────────────
            grpGenFile.Text = "Исходный файл Excel";
            grpGenFile.Location = new System.Drawing.Point(12, 12);
            grpGenFile.Size = new System.Drawing.Size(648, 68);

            lblExcelPath.Text = "Путь к файлу:";
            lblExcelPath.Location = new System.Drawing.Point(12, 22);
            lblExcelPath.Size = new System.Drawing.Size(90, 20);
            lblExcelPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            txtExcelPath.Location = new System.Drawing.Point(106, 20);
            txtExcelPath.Size = new System.Drawing.Size(430, 23);
            txtExcelPath.Font = _uiFont;

            btnBrowseExcel.Text = "Обзор...";
            btnBrowseExcel.Location = new System.Drawing.Point(544, 19);
            btnBrowseExcel.Size = new System.Drawing.Size(88, 26);
            btnBrowseExcel.Click += new System.EventHandler(this.btnBrowseExcel_Click);

            grpGenFile.Controls.AddRange(new System.Windows.Forms.Control[]
                { lblExcelPath, txtExcelPath, btnBrowseExcel });

            // ── GroupBox: Диапазон заявок ─────────────────────────────────
            grpGenRange.Text = "Диапазон заявок";
            grpGenRange.Location = new System.Drawing.Point(12, 92);
            grpGenRange.Size = new System.Drawing.Size(648, 72);

            lblGenStart.Text = "С номера (МФ):";
            lblGenStart.Location = new System.Drawing.Point(12, 22);
            lblGenStart.Size = new System.Drawing.Size(110, 20);
            lblGenStart.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            txtGenStart.Location = new System.Drawing.Point(126, 20);
            txtGenStart.Size = new System.Drawing.Size(80, 23);
            txtGenStart.Font = _uiFont;

            lblGenEnd.Text = "По номер (МФ):";
            lblGenEnd.Location = new System.Drawing.Point(240, 22);
            lblGenEnd.Size = new System.Drawing.Size(110, 20);
            lblGenEnd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            txtGenEnd.Location = new System.Drawing.Point(354, 20);
            txtGenEnd.Size = new System.Drawing.Size(80, 23);
            txtGenEnd.Font = _uiFont;

            grpGenRange.Controls.AddRange(new System.Windows.Forms.Control[]
                { lblGenStart, txtGenStart, lblGenEnd, txtGenEnd });

            // ── Кнопки и прогресс ─────────────────────────────────────────
            btnGenerate.Text = "▶  Создать файлы";
            btnGenerate.Location = new System.Drawing.Point(12, 178);
            btnGenerate.Size = new System.Drawing.Size(150, 32);
            btnGenerate.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            btnGenerate.ForeColor = System.Drawing.Color.White;
            btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnGenerate.FlatAppearance.BorderSize = 0;
            btnGenerate.Font = _boldFont;
            btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);

            btnCancelGenerate.Text = "✕  Отмена";
            btnCancelGenerate.Location = new System.Drawing.Point(170, 178);
            btnCancelGenerate.Size = new System.Drawing.Size(100, 32);
            btnCancelGenerate.Enabled = false;
            btnCancelGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCancelGenerate.Click += new System.EventHandler(this.btnCancelGenerate_Click);

            pgGenerate.Location = new System.Drawing.Point(12, 220);
            pgGenerate.Size = new System.Drawing.Size(560, 18);

            lblGenProgress.Text = "";
            lblGenProgress.Location = new System.Drawing.Point(580, 220);
            lblGenProgress.Size = new System.Drawing.Size(68, 18);
            lblGenProgress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            lblGenProgress.ForeColor = System.Drawing.Color.Gray;
            lblGenProgress.Font = _smallFont;

            // ── Лог генерации ─────────────────────────────────────────────
            txtGenLog.Location = new System.Drawing.Point(12, 248);
            txtGenLog.Size = new System.Drawing.Size(636, 178);
            txtGenLog.Multiline = true;
            txtGenLog.ReadOnly = true;
            txtGenLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            txtGenLog.BackColor = System.Drawing.Color.FromArgb(24, 24, 24);
            txtGenLog.ForeColor = System.Drawing.Color.FromArgb(180, 220, 180);
            txtGenLog.Font = new System.Drawing.Font("Consolas", 9f);
            txtGenLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            tabGenerate.Text = "  📄 Генерация  ";
            tabGenerate.Padding = new System.Windows.Forms.Padding(4);
            tabGenerate.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                grpGenFile, grpGenRange,
                btnGenerate, btnCancelGenerate,
                pgGenerate, lblGenProgress,
                txtGenLog
            });

            // =================================================================
            // ВКЛАДКА "ОТПРАВКА"
            // =================================================================

            // ── GroupBox: Папка с файлами ─────────────────────────────────
            grpSendDir.Text = "Папка с готовыми файлами (PDF и XLSX)";
            grpSendDir.Location = new System.Drawing.Point(12, 12);
            grpSendDir.Size = new System.Drawing.Size(648, 68);

            lblFilesDir.Text = "Путь к папке:";
            lblFilesDir.Location = new System.Drawing.Point(12, 22);
            lblFilesDir.Size = new System.Drawing.Size(90, 20);
            lblFilesDir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            txtFilesDir.Location = new System.Drawing.Point(106, 20);
            txtFilesDir.Size = new System.Drawing.Size(430, 23);
            txtFilesDir.Font = _uiFont;

            btnBrowseDir.Text = "Обзор...";
            btnBrowseDir.Location = new System.Drawing.Point(544, 19);
            btnBrowseDir.Size = new System.Drawing.Size(88, 26);
            btnBrowseDir.Click += new System.EventHandler(this.btnBrowseDir_Click);

            grpSendDir.Controls.AddRange(new System.Windows.Forms.Control[]
                { lblFilesDir, txtFilesDir, btnBrowseDir });

            // ── GroupBox: Диапазон заявок ─────────────────────────────────
            grpSendRange.Text = "Диапазон заявок для отправки";
            grpSendRange.Location = new System.Drawing.Point(12, 92);
            grpSendRange.Size = new System.Drawing.Size(648, 72);

            lblSendStart.Text = "С номера (МФ):";
            lblSendStart.Location = new System.Drawing.Point(12, 22);
            lblSendStart.Size = new System.Drawing.Size(110, 20);
            lblSendStart.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            txtSendStart.Location = new System.Drawing.Point(126, 20);
            txtSendStart.Size = new System.Drawing.Size(80, 23);
            txtSendStart.Font = _uiFont;

            lblSendEnd.Text = "По номер (МФ):";
            lblSendEnd.Location = new System.Drawing.Point(240, 22);
            lblSendEnd.Size = new System.Drawing.Size(110, 20);
            lblSendEnd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            txtSendEnd.Location = new System.Drawing.Point(354, 20);
            txtSendEnd.Size = new System.Drawing.Size(80, 23);
            txtSendEnd.Font = _uiFont;

            grpSendRange.Controls.AddRange(new System.Windows.Forms.Control[]
                { lblSendStart, txtSendStart, lblSendEnd, txtSendEnd });

            // ── GroupBox: Получатели ──────────────────────────────────────
            grpSendEmail.Text = "Получатели";
            grpSendEmail.Location = new System.Drawing.Point(12, 176);
            grpSendEmail.Size = new System.Drawing.Size(648, 72);

            lblRecipient.Text = "Кому (To):";
            lblRecipient.Location = new System.Drawing.Point(12, 22);
            lblRecipient.Size = new System.Drawing.Size(80, 20);
            lblRecipient.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            txtRecipient.Location = new System.Drawing.Point(96, 20);
            txtRecipient.Size = new System.Drawing.Size(220, 23);
            txtRecipient.Font = _uiFont;

            lblCc.Text = "Копия (CC):";
            lblCc.Location = new System.Drawing.Point(336, 22);
            lblCc.Size = new System.Drawing.Size(80, 20);
            lblCc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            txtCc.Location = new System.Drawing.Point(420, 20);
            txtCc.Size = new System.Drawing.Size(212, 23);
            txtCc.Font = _uiFont;

            grpSendEmail.Controls.AddRange(new System.Windows.Forms.Control[]
                { lblRecipient, txtRecipient, lblCc, txtCc });

            // ── Кнопки и прогресс ─────────────────────────────────────────
            btnSend.Text = "✉  Отправить письма";
            btnSend.Location = new System.Drawing.Point(12, 262);
            btnSend.Size = new System.Drawing.Size(170, 32);
            btnSend.BackColor = System.Drawing.Color.FromArgb(16, 124, 16);
            btnSend.ForeColor = System.Drawing.Color.White;
            btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSend.FlatAppearance.BorderSize = 0;
            btnSend.Font = _boldFont;
            btnSend.Click += new System.EventHandler(this.btnSend_Click);

            btnCancelSend.Text = "✕  Отмена";
            btnCancelSend.Location = new System.Drawing.Point(190, 262);
            btnCancelSend.Size = new System.Drawing.Size(100, 32);
            btnCancelSend.Enabled = false;
            btnCancelSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCancelSend.Click += new System.EventHandler(this.btnCancelSend_Click);

            pgSend.Location = new System.Drawing.Point(12, 304);
            pgSend.Size = new System.Drawing.Size(560, 18);

            lblSendProgress.Text = "";
            lblSendProgress.Location = new System.Drawing.Point(580, 304);
            lblSendProgress.Size = new System.Drawing.Size(68, 18);
            lblSendProgress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            lblSendProgress.ForeColor = System.Drawing.Color.Gray;
            lblSendProgress.Font = _smallFont;

            // ── Лог отправки ──────────────────────────────────────────────
            txtSendLog.Location = new System.Drawing.Point(12, 332);
            txtSendLog.Size = new System.Drawing.Size(636, 94);
            txtSendLog.Multiline = true;
            txtSendLog.ReadOnly = true;
            txtSendLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            txtSendLog.BackColor = System.Drawing.Color.FromArgb(24, 24, 24);
            txtSendLog.ForeColor = System.Drawing.Color.FromArgb(180, 220, 180);
            txtSendLog.Font = new System.Drawing.Font("Consolas", 9f);
            txtSendLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            tabSend.Text = "  ✉ Отправка  ";
            tabSend.Padding = new System.Windows.Forms.Padding(4);
            tabSend.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                grpSendDir, grpSendRange, grpSendEmail,
                btnSend, btnCancelSend,
                pgSend, lblSendProgress,
                txtSendLog
            });

            // =================================================================
            // TABCONTROL + FORM
            // =================================================================

            tabControl.Location = new System.Drawing.Point(0, 0);
            tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            tabControl.Font = _uiFont;
            tabControl.TabPages.Add(tabGenerate);
            tabControl.TabPages.Add(tabSend);

            this.Text = "Генерация заявок МФ — Мурманский филиал";
            this.ClientSize = new System.Drawing.Size(676, 466);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Font = _uiFont;
            this.Controls.Add(tabControl);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // ── Шрифты (статические, переиспользуются) ───────────────────────
        private static readonly System.Drawing.Font _uiFont = new System.Drawing.Font("Segoe UI", 9.5f);
        private static readonly System.Drawing.Font _boldFont = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
        private static readonly System.Drawing.Font _smallFont = new System.Drawing.Font("Segoe UI", 8.5f);
    }
}