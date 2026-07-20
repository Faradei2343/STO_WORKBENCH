using Microsoft.Office.Interop.Excel;
using STO_WORKBENCH.PDFFiller;

namespace STO_WORKBENCH.PDFFiller
{
    partial class FormPDFFiller
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // Контролы
            menuStrip = new System.Windows.Forms.MenuStrip();
            menuItemSettings = new System.Windows.Forms.ToolStripMenuItem();
            menuItemSettingsOpen = new System.Windows.Forms.ToolStripMenuItem();
            menuItemRefresh = new System.Windows.Forms.ToolStripMenuItem();
            panelTop = new System.Windows.Forms.Panel();
            lblTitle = new System.Windows.Forms.Label();
            lblSubtitle = new System.Windows.Forms.Label();
            panelMain = new System.Windows.Forms.Panel();
            panelGrid = new System.Windows.Forms.Panel();
            lblFilesTitle = new System.Windows.Forms.Label();
            dataGridViewFiles = new System.Windows.Forms.DataGridView();
            columnFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            columnSelected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            progressBar = new System.Windows.Forms.ProgressBar();
            panelRight = new System.Windows.Forms.Panel();
            groupBoxMode = new System.Windows.Forms.GroupBox();
            radioButtonProcessAll = new System.Windows.Forms.RadioButton();
            radioButtonProcessSingle = new System.Windows.Forms.RadioButton();
            groupBoxOptions = new System.Windows.Forms.GroupBox();
            checkBoxCustomDate = new System.Windows.Forms.CheckBox();
            checkBoxMonthYearOnly = new System.Windows.Forms.CheckBox();
            checkBoxCustomApplicant = new System.Windows.Forms.CheckBox();
            panelSettings = new System.Windows.Forms.Panel();
            panelDateTime = new System.Windows.Forms.Panel();
            lblDateTimeHeader = new System.Windows.Forms.Label();
            labelDateTime = new System.Windows.Forms.Label();
            dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
            labelTime = new System.Windows.Forms.Label();
            maskedTextBoxTime = new System.Windows.Forms.MaskedTextBox();
            panelApplicant = new System.Windows.Forms.Panel();
            lblApplicantHeader = new System.Windows.Forms.Label();
            labelLastName = new System.Windows.Forms.Label();
            textBoxApplicantLastName = new System.Windows.Forms.TextBox();
            labelFirstName = new System.Windows.Forms.Label();
            textBoxApplicantFirstName = new System.Windows.Forms.TextBox();
            labelPatronymic = new System.Windows.Forms.Label();
            textBoxApplicantPatronymic = new System.Windows.Forms.TextBox();
            buttonProcess = new System.Windows.Forms.Button();
            statusStrip = new System.Windows.Forms.StatusStrip();
            statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            statusLabelCount = new System.Windows.Forms.ToolStripStatusLabel();
            timerRefresh = new System.Windows.Forms.Timer(this.components);

            ((System.ComponentModel.ISupportInitialize)dataGridViewFiles).BeginInit();
            menuStrip.SuspendLayout();
            panelTop.SuspendLayout();
            panelMain.SuspendLayout();
            panelGrid.SuspendLayout();
            panelRight.SuspendLayout();
            groupBoxMode.SuspendLayout();
            groupBoxOptions.SuspendLayout();
            panelSettings.SuspendLayout();
            panelDateTime.SuspendLayout();
            panelApplicant.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();

            // ── menuStrip ─────────────────────────────────────────────────
            menuStrip.BackColor = System.Drawing.Color.FromArgb(22, 28, 40);
            menuStrip.ForeColor = System.Drawing.Color.White;
            menuStrip.Dock = System.Windows.Forms.DockStyle.Top;
            menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
                { menuItemSettings, menuItemRefresh });

            menuItemSettings.Text = "Настройки";
            menuItemSettings.ForeColor = System.Drawing.Color.FromArgb(190, 200, 220);
            menuItemSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
                { menuItemSettingsOpen });

            menuItemSettingsOpen.Text = "Открыть настройки";
            menuItemSettingsOpen.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S;
            menuItemSettingsOpen.Click += menuItemSettingsOpen_Click;

            menuItemRefresh.Text = "Обновить таблицу";
            menuItemRefresh.ForeColor = System.Drawing.Color.FromArgb(190, 200, 220);
            menuItemRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
            menuItemRefresh.Click += menuItemRefresh_Click;

            // ── panelTop (шапка) ──────────────────────────────────────────
            panelTop.BackColor = System.Drawing.Color.FromArgb(22, 28, 40);
            panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            panelTop.Height = 72;
            panelTop.Controls.AddRange(new System.Windows.Forms.Control[]
                { lblTitle, lblSubtitle });

            lblTitle.Text = "✍  Автозаполнение сервисных листов";
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 13f, System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.White;
            lblTitle.Location = new System.Drawing.Point(20, 14);
            lblTitle.Size = new System.Drawing.Size(600, 26);

            lblSubtitle.Text = "Выберите файлы, настройте параметры и запустите обработку";
            lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 8.5f);
            lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(120, 140, 170);
            lblSubtitle.Location = new System.Drawing.Point(20, 44);
            lblSubtitle.Size = new System.Drawing.Size(600, 18);

            // ── panelMain (всё тело формы под шапкой) ────────────────────
            panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            panelMain.BackColor = System.Drawing.Color.FromArgb(242, 244, 248);
            panelMain.Padding = new System.Windows.Forms.Padding(16);
            panelMain.Controls.AddRange(new System.Windows.Forms.Control[]
                { panelGrid, panelRight });

            // ── panelGrid (левая колонка — таблица + прогресс) ───────────
            panelGrid.BackColor = System.Drawing.Color.Transparent;
            panelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            panelGrid.Padding = new System.Windows.Forms.Padding(0, 0, 12, 0);
            panelGrid.Controls.AddRange(new System.Windows.Forms.Control[]
                { lblFilesTitle, dataGridViewFiles, progressBar });

            lblFilesTitle.Text = "Список PDF-файлов";
            lblFilesTitle.Font = new System.Drawing.Font("Segoe UI", 8.5f, System.Drawing.FontStyle.Bold);
            lblFilesTitle.ForeColor = System.Drawing.Color.FromArgb(100, 110, 130);
            lblFilesTitle.Dock = System.Windows.Forms.DockStyle.Top;
            lblFilesTitle.Height = 24;
            lblFilesTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            dataGridViewFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridViewFiles.AllowUserToAddRows = false;
            dataGridViewFiles.AllowUserToDeleteRows = false;
            dataGridViewFiles.AllowUserToResizeRows = false;
            dataGridViewFiles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewFiles.BackgroundColor = System.Drawing.Color.White;
            dataGridViewFiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewFiles.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewFiles.GridColor = System.Drawing.Color.FromArgb(220, 225, 235);
            dataGridViewFiles.MultiSelect = false;
            dataGridViewFiles.RowHeadersVisible = false;
            dataGridViewFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridViewFiles.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            dataGridViewFiles.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(28, 35, 49);
            dataGridViewFiles.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dataGridViewFiles.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            dataGridViewFiles.EnableHeadersVisualStyles = false;
            dataGridViewFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
                { columnFileName, columnSelected });
            dataGridViewFiles.CellContentClick += dataGridViewFiles_CellContentClick;
            dataGridViewFiles.CellDoubleClick += dataGridViewFiles_CellDoubleClick;

            columnFileName.HeaderText = "Имя файла";
            columnFileName.Name = "columnFileName";
            columnFileName.ReadOnly = true;
            columnFileName.FillWeight = 75f;

            columnSelected.HeaderText = "✔";
            columnSelected.Name = "columnSelected";
            columnSelected.FillWeight = 25f;
            columnSelected.Resizable = System.Windows.Forms.DataGridViewTriState.False;

            progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            progressBar.Height = 6;
            progressBar.Visible = false;
            progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            progressBar.BackColor = System.Drawing.Color.FromArgb(220, 225, 235);
            progressBar.ForeColor = System.Drawing.Color.FromArgb(0, 122, 204);

            // ── panelRight (правая колонка — настройки + кнопка) ─────────
            panelRight.BackColor = System.Drawing.Color.Transparent;
            panelRight.Dock = System.Windows.Forms.DockStyle.Right;
            panelRight.Width = 300;
            panelRight.Controls.AddRange(new System.Windows.Forms.Control[]
                { groupBoxMode, panelSettings, buttonProcess });

            // ── groupBoxMode ──────────────────────────────────────────────
            groupBoxMode.Text = "Режим обработки";
            groupBoxMode.Font = new System.Drawing.Font("Segoe UI", 9f);
            groupBoxMode.ForeColor = System.Drawing.Color.FromArgb(80, 90, 110);
            groupBoxMode.Dock = System.Windows.Forms.DockStyle.Top;
            groupBoxMode.Height = 180;
            groupBoxMode.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            groupBoxMode.Controls.AddRange(new System.Windows.Forms.Control[]
                { radioButtonProcessAll, radioButtonProcessSingle, groupBoxOptions });

            radioButtonProcessAll.Text = "Все выбранные файлы";
            radioButtonProcessAll.Font = new System.Drawing.Font("Segoe UI", 9f);
            radioButtonProcessAll.Location = new System.Drawing.Point(12, 24);
            radioButtonProcessAll.Size = new System.Drawing.Size(270, 20);
            radioButtonProcessAll.Checked = true;

            radioButtonProcessSingle.Text = "Только текущий (выделенный)";
            radioButtonProcessSingle.Font = new System.Drawing.Font("Segoe UI", 9f);
            radioButtonProcessSingle.Location = new System.Drawing.Point(12, 48);
            radioButtonProcessSingle.Size = new System.Drawing.Size(270, 20);

            groupBoxOptions.Text = "Опции заполнения";
            groupBoxOptions.Font = new System.Drawing.Font("Segoe UI", 9f);
            groupBoxOptions.ForeColor = System.Drawing.Color.FromArgb(80, 90, 110);
            groupBoxOptions.Location = new System.Drawing.Point(10, 74);
            groupBoxOptions.Size = new System.Drawing.Size(276, 92);
            groupBoxOptions.Controls.AddRange(new System.Windows.Forms.Control[]
                { checkBoxCustomDate, checkBoxMonthYearOnly, checkBoxCustomApplicant });

            checkBoxCustomDate.Text = "Своя дата";
            checkBoxCustomDate.Font = new System.Drawing.Font("Segoe UI", 9f);
            checkBoxCustomDate.Location = new System.Drawing.Point(10, 22);
            checkBoxCustomDate.AutoSize = true;
            checkBoxCustomDate.CheckedChanged += checkBoxCustomDate_CheckedChanged;

            checkBoxMonthYearOnly.Text = "Только месяц и год";
            checkBoxMonthYearOnly.Font = new System.Drawing.Font("Segoe UI", 9f);
            checkBoxMonthYearOnly.Location = new System.Drawing.Point(10, 46);
            checkBoxMonthYearOnly.AutoSize = true;
            checkBoxMonthYearOnly.CheckedChanged += checkBoxMonthYearOnly_CheckedChanged;

            checkBoxCustomApplicant.Text = "Свой заявитель";
            checkBoxCustomApplicant.Font = new System.Drawing.Font("Segoe UI", 9f);
            checkBoxCustomApplicant.Location = new System.Drawing.Point(10, 68);
            checkBoxCustomApplicant.AutoSize = true;
            checkBoxCustomApplicant.CheckedChanged += checkBoxCustomApplicant_CheckedChanged;

            // ── panelSettings (динамические панели дата/заявитель) ────────
            panelSettings.Dock = System.Windows.Forms.DockStyle.Top;
            panelSettings.Height = 200;
            panelSettings.BackColor = System.Drawing.Color.Transparent;
            panelSettings.Controls.AddRange(new System.Windows.Forms.Control[]
                { panelDateTime, panelApplicant });

            // ── panelDateTime ─────────────────────────────────────────────
            panelDateTime.Dock = System.Windows.Forms.DockStyle.Top;
            panelDateTime.Height = 96;
            panelDateTime.BackColor = System.Drawing.Color.White;
            panelDateTime.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            panelDateTime.Visible = false;
            panelDateTime.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                lblDateTimeHeader, labelDateTime,
                dateTimePickerDate, labelTime, maskedTextBoxTime
            });

            lblDateTimeHeader.Text = "ДАТА И ВРЕМЯ";
            lblDateTimeHeader.Font = new System.Drawing.Font("Segoe UI", 7.5f, System.Drawing.FontStyle.Bold);
            lblDateTimeHeader.ForeColor = System.Drawing.Color.FromArgb(0, 122, 204);
            lblDateTimeHeader.Location = new System.Drawing.Point(12, 8);
            lblDateTimeHeader.Size = new System.Drawing.Size(270, 14);

            labelDateTime.Text = "Дата и время закрытия заявки";
            labelDateTime.Font = new System.Drawing.Font("Segoe UI", 8.5f);
            labelDateTime.ForeColor = System.Drawing.Color.FromArgb(80, 90, 110);
            labelDateTime.Location = new System.Drawing.Point(12, 26);
            labelDateTime.Size = new System.Drawing.Size(270, 16);

            dateTimePickerDate.Location = new System.Drawing.Point(12, 46);
            dateTimePickerDate.Size = new System.Drawing.Size(168, 23);
            dateTimePickerDate.Font = new System.Drawing.Font("Segoe UI", 9f);

            labelTime.Text = "Время";
            labelTime.Font = new System.Drawing.Font("Segoe UI", 8.5f);
            labelTime.ForeColor = System.Drawing.Color.FromArgb(80, 90, 110);
            labelTime.Location = new System.Drawing.Point(188, 28);
            labelTime.Size = new System.Drawing.Size(48, 16);

            maskedTextBoxTime.Location = new System.Drawing.Point(188, 46);
            maskedTextBoxTime.Size = new System.Drawing.Size(60, 23);
            maskedTextBoxTime.Font = new System.Drawing.Font("Segoe UI", 9f);
            maskedTextBoxTime.Mask = "00:00";
            maskedTextBoxTime.Text = "1200";
            maskedTextBoxTime.ValidatingType = typeof(System.DateTime);

            // ── panelApplicant ────────────────────────────────────────────
            panelApplicant.Dock = System.Windows.Forms.DockStyle.Top;
            panelApplicant.Height = 120;
            panelApplicant.BackColor = System.Drawing.Color.White;
            panelApplicant.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            panelApplicant.Visible = false;
            panelApplicant.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                lblApplicantHeader,
                labelLastName,   textBoxApplicantLastName,
                labelFirstName,  textBoxApplicantFirstName,
                labelPatronymic, textBoxApplicantPatronymic
            });

            lblApplicantHeader.Text = "ЗАЯВИТЕЛЬ";
            lblApplicantHeader.Font = new System.Drawing.Font("Segoe UI", 7.5f, System.Drawing.FontStyle.Bold);
            lblApplicantHeader.ForeColor = System.Drawing.Color.FromArgb(0, 122, 204);
            lblApplicantHeader.Location = new System.Drawing.Point(12, 8);
            lblApplicantHeader.Size = new System.Drawing.Size(270, 14);

            labelLastName.Text = "Фамилия";
            labelLastName.Font = new System.Drawing.Font("Segoe UI", 8.5f);
            labelLastName.ForeColor = System.Drawing.Color.FromArgb(80, 90, 110);
            labelLastName.Location = new System.Drawing.Point(12, 26);
            labelLastName.Size = new System.Drawing.Size(80, 16);

            textBoxApplicantLastName.Location = new System.Drawing.Point(96, 23);
            textBoxApplicantLastName.Size = new System.Drawing.Size(188, 23);
            textBoxApplicantLastName.Font = new System.Drawing.Font("Segoe UI", 9f);
            textBoxApplicantLastName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            labelFirstName.Text = "Имя";
            labelFirstName.Font = new System.Drawing.Font("Segoe UI", 8.5f);
            labelFirstName.ForeColor = System.Drawing.Color.FromArgb(80, 90, 110);
            labelFirstName.Location = new System.Drawing.Point(12, 54);
            labelFirstName.Size = new System.Drawing.Size(80, 16);

            textBoxApplicantFirstName.Location = new System.Drawing.Point(96, 51);
            textBoxApplicantFirstName.Size = new System.Drawing.Size(188, 23);
            textBoxApplicantFirstName.Font = new System.Drawing.Font("Segoe UI", 9f);
            textBoxApplicantFirstName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            labelPatronymic.Text = "Отчество";
            labelPatronymic.Font = new System.Drawing.Font("Segoe UI", 8.5f);
            labelPatronymic.ForeColor = System.Drawing.Color.FromArgb(80, 90, 110);
            labelPatronymic.Location = new System.Drawing.Point(12, 82);
            labelPatronymic.Size = new System.Drawing.Size(80, 16);

            textBoxApplicantPatronymic.Location = new System.Drawing.Point(96, 79);
            textBoxApplicantPatronymic.Size = new System.Drawing.Size(188, 23);
            textBoxApplicantPatronymic.Font = new System.Drawing.Font("Segoe UI", 9f);
            textBoxApplicantPatronymic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // ── buttonProcess ─────────────────────────────────────────────
            buttonProcess.Text = "▶  Обработать СЛ";
            buttonProcess.Dock = System.Windows.Forms.DockStyle.Bottom;
            buttonProcess.Height = 40;
            buttonProcess.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
            buttonProcess.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            buttonProcess.ForeColor = System.Drawing.Color.White;
            buttonProcess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonProcess.FlatAppearance.BorderSize = 0;
            buttonProcess.UseVisualStyleBackColor = false;
            buttonProcess.Click += buttonProcess_Click;

            // ── statusStrip ───────────────────────────────────────────────
            statusStrip.BackColor = System.Drawing.Color.FromArgb(28, 35, 49);
            statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
                { statusLabel, statusLabelCount });

            statusLabel.Text = "Готов";
            statusLabel.ForeColor = System.Drawing.Color.FromArgb(190, 200, 220);

            statusLabelCount.ForeColor = System.Drawing.Color.FromArgb(120, 140, 170);
            statusLabelCount.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;

            // ── timerRefresh ──────────────────────────────────────────────
            timerRefresh.Interval = 30000;
            timerRefresh.Tick += timerRefresh_Tick;

            // ── Form ──────────────────────────────────────────────────────
            AutoScaleDimensions = new System.Drawing.SizeF(7f, 15f);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(920, 620);
            MinimumSize = new System.Drawing.Size(780, 520);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Автозаполнение сервисных листов";
            Name = "FormPDFFiller";
            MainMenuStrip = menuStrip;
            BackColor = System.Drawing.Color.FromArgb(242, 244, 248);
            Load += FormPDFFiller_Load;

            // Порядок важен: Fill заполняет остаток после всех Dock
            Controls.Add(panelMain);
            Controls.Add(panelTop);
            Controls.Add(menuStrip);
            Controls.Add(statusStrip);

            ((System.ComponentModel.ISupportInitialize)dataGridViewFiles).EndInit();
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            panelTop.ResumeLayout(false);
            panelMain.ResumeLayout(false);
            panelGrid.ResumeLayout(false);
            panelRight.ResumeLayout(false);
            groupBoxMode.ResumeLayout(false);
            groupBoxOptions.ResumeLayout(false);
            panelSettings.ResumeLayout(false);
            panelDateTime.ResumeLayout(false);
            panelApplicant.ResumeLayout(false);
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuItemSettings;
        private System.Windows.Forms.ToolStripMenuItem menuItemSettingsOpen;
        private System.Windows.Forms.ToolStripMenuItem menuItemRefresh;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelGrid;
        private System.Windows.Forms.Label lblFilesTitle;
        private System.Windows.Forms.DataGridView dataGridViewFiles;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnFileName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn columnSelected;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.GroupBox groupBoxMode;
        private System.Windows.Forms.RadioButton radioButtonProcessAll;
        private System.Windows.Forms.RadioButton radioButtonProcessSingle;
        private System.Windows.Forms.GroupBox groupBoxOptions;
        private System.Windows.Forms.CheckBox checkBoxCustomDate;
        private System.Windows.Forms.CheckBox checkBoxMonthYearOnly;
        private System.Windows.Forms.CheckBox checkBoxCustomApplicant;
        private System.Windows.Forms.Panel panelSettings;
        private System.Windows.Forms.Panel panelDateTime;
        private System.Windows.Forms.Label lblDateTimeHeader;
        private System.Windows.Forms.Label labelDateTime;
        private System.Windows.Forms.DateTimePicker dateTimePickerDate;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxTime;
        private System.Windows.Forms.Panel panelApplicant;
        private System.Windows.Forms.Label lblApplicantHeader;
        private System.Windows.Forms.Label labelLastName;
        private System.Windows.Forms.TextBox textBoxApplicantLastName;
        private System.Windows.Forms.Label labelFirstName;
        private System.Windows.Forms.TextBox textBoxApplicantFirstName;
        private System.Windows.Forms.Label labelPatronymic;
        private System.Windows.Forms.TextBox textBoxApplicantPatronymic;
        private System.Windows.Forms.Button buttonProcess;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelCount;
        private System.Windows.Forms.Timer timerRefresh;
    }
}