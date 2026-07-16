namespace STO_WORKBENCH.PDFFiller
{
    partial class FormPDFFiller
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // Инициализация компонентов
            this.buttonProcess = new System.Windows.Forms.Button();
            this.dataGridViewFiles = new System.Windows.Forms.DataGridView();
            this.columnFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSelected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuItemSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSettingsOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxMode = new System.Windows.Forms.GroupBox();
            this.groupBoxOptions = new System.Windows.Forms.GroupBox();
            this.checkBoxMonthYearOnly = new System.Windows.Forms.CheckBox();
            this.checkBoxCustomDate = new System.Windows.Forms.CheckBox();
            this.checkBoxCustomApplicant = new System.Windows.Forms.CheckBox();
            this.radioButtonProcessAll = new System.Windows.Forms.RadioButton();
            this.radioButtonProcessSingle = new System.Windows.Forms.RadioButton();
            this.panelSettings = new System.Windows.Forms.Panel();
            this.panelApplicant = new System.Windows.Forms.Panel();
            this.labelPatronymic = new System.Windows.Forms.Label();
            this.labelFirstName = new System.Windows.Forms.Label();
            this.labelLastName = new System.Windows.Forms.Label();
            this.textBoxApplicantPatronymic = new System.Windows.Forms.TextBox();
            this.textBoxApplicantFirstName = new System.Windows.Forms.TextBox();
            this.textBoxApplicantLastName = new System.Windows.Forms.TextBox();
            this.panelDateTime = new System.Windows.Forms.Panel();
            this.maskedTextBoxTime = new System.Windows.Forms.MaskedTextBox();
            this.labelDateTime = new System.Windows.Forms.Label();
            this.dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
            this.labelTime = new System.Windows.Forms.Label();
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelCount = new System.Windows.Forms.ToolStripStatusLabel();

            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFiles)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.groupBoxMode.SuspendLayout();
            this.groupBoxOptions.SuspendLayout();
            this.panelSettings.SuspendLayout();
            this.panelApplicant.SuspendLayout();
            this.panelDateTime.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();

            // 
            // buttonProcess
            // 
            this.buttonProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonProcess.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonProcess.Location = new System.Drawing.Point(572, 479);
            this.buttonProcess.Name = "buttonProcess";
            this.buttonProcess.Size = new System.Drawing.Size(300, 40);
            this.buttonProcess.TabIndex = 0;
            this.buttonProcess.Text = "Обработать СЛ";
            this.buttonProcess.UseVisualStyleBackColor = true;
            this.buttonProcess.Click += new System.EventHandler(this.buttonProcess_Click);

            // 
            // dataGridViewFiles
            // 
            this.dataGridViewFiles.AllowUserToAddRows = false;
            this.dataGridViewFiles.AllowUserToDeleteRows = false;
            this.dataGridViewFiles.AllowUserToResizeRows = false;
            this.dataGridViewFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewFiles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewFiles.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewFiles.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewFiles.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridViewFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.columnFileName,
                this.columnSelected});
            this.dataGridViewFiles.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridViewFiles.Location = new System.Drawing.Point(12, 27);
            this.dataGridViewFiles.MultiSelect = false;
            this.dataGridViewFiles.Name = "dataGridViewFiles";
            this.dataGridViewFiles.RowHeadersVisible = false;
            this.dataGridViewFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewFiles.Size = new System.Drawing.Size(550, 487);
            this.dataGridViewFiles.TabIndex = 1;
            this.dataGridViewFiles.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewFiles_CellContentClick);
            this.dataGridViewFiles.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewFiles_CellDoubleClick);

            // 
            // columnFileName
            // 
            this.columnFileName.FillWeight = 70F;
            this.columnFileName.HeaderText = "Имя файла";
            this.columnFileName.Name = "columnFileName";
            this.columnFileName.ReadOnly = true;

            // 
            // columnSelected
            // 
            this.columnSelected.FillWeight = 30F;
            this.columnSelected.HeaderText = "Выбрать";
            this.columnSelected.Name = "columnSelected";
            this.columnSelected.Resizable = System.Windows.Forms.DataGridViewTriState.False;

            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.White;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuItemSettings,
                this.menuItemRefresh});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(884, 24);
            this.menuStrip.TabIndex = 2;

            // 
            // menuItemSettings
            // 
            this.menuItemSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuItemSettingsOpen});
            this.menuItemSettings.Name = "menuItemSettings";
            this.menuItemSettings.Size = new System.Drawing.Size(79, 20);
            this.menuItemSettings.Text = "Настройки";

            // 
            // menuItemSettingsOpen
            // 
            this.menuItemSettingsOpen.Name = "menuItemSettingsOpen";
            this.menuItemSettingsOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuItemSettingsOpen.Size = new System.Drawing.Size(200, 22);
            this.menuItemSettingsOpen.Text = "Открыть настройки";
            this.menuItemSettingsOpen.Click += new System.EventHandler(this.menuItemSettingsOpen_Click);

            // 
            // menuItemRefresh
            // 
            this.menuItemRefresh.Name = "menuItemRefresh";
            this.menuItemRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.menuItemRefresh.Size = new System.Drawing.Size(121, 20);
            this.menuItemRefresh.Text = "Обновить таблицу";
            this.menuItemRefresh.Click += new System.EventHandler(this.menuItemRefresh_Click);

            // 
            // groupBoxMode
            // 
            this.groupBoxMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxMode.Controls.Add(this.groupBoxOptions);
            this.groupBoxMode.Controls.Add(this.radioButtonProcessSingle);
            this.groupBoxMode.Controls.Add(this.radioButtonProcessAll);
            this.groupBoxMode.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxMode.Location = new System.Drawing.Point(572, 27);
            this.groupBoxMode.Name = "groupBoxMode";
            this.groupBoxMode.Size = new System.Drawing.Size(300, 170);
            this.groupBoxMode.TabIndex = 3;
            this.groupBoxMode.TabStop = false;
            this.groupBoxMode.Text = "Режим заполнения";

            // 
            // groupBoxOptions
            // 
            this.groupBoxOptions.Controls.Add(this.checkBoxMonthYearOnly);
            this.groupBoxOptions.Controls.Add(this.checkBoxCustomDate);
            this.groupBoxOptions.Controls.Add(this.checkBoxCustomApplicant);
            this.groupBoxOptions.Location = new System.Drawing.Point(6, 72);
            this.groupBoxOptions.Name = "groupBoxOptions";
            this.groupBoxOptions.Size = new System.Drawing.Size(288, 92);
            this.groupBoxOptions.TabIndex = 4;
            this.groupBoxOptions.TabStop = false;
            this.groupBoxOptions.Text = "Опции";

            // 
            // checkBoxMonthYearOnly
            // 
            this.checkBoxMonthYearOnly.AutoSize = true;
            this.checkBoxMonthYearOnly.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.checkBoxMonthYearOnly.Location = new System.Drawing.Point(6, 62);
            this.checkBoxMonthYearOnly.Name = "checkBoxMonthYearOnly";
            this.checkBoxMonthYearOnly.Size = new System.Drawing.Size(202, 19);
            this.checkBoxMonthYearOnly.TabIndex = 4;
            this.checkBoxMonthYearOnly.Text = "Заполнить только месяц и год";
            this.checkBoxMonthYearOnly.UseVisualStyleBackColor = true;
            this.checkBoxMonthYearOnly.CheckedChanged += new System.EventHandler(this.checkBoxMonthYearOnly_CheckedChanged);

            // 
            // checkBoxCustomDate
            // 
            this.checkBoxCustomDate.AutoSize = true;
            this.checkBoxCustomDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.checkBoxCustomDate.Location = new System.Drawing.Point(6, 25);
            this.checkBoxCustomDate.Name = "checkBoxCustomDate";
            this.checkBoxCustomDate.Size = new System.Drawing.Size(82, 19);
            this.checkBoxCustomDate.TabIndex = 2;
            this.checkBoxCustomDate.Text = "Своя дата";
            this.checkBoxCustomDate.UseVisualStyleBackColor = true;
            this.checkBoxCustomDate.CheckedChanged += new System.EventHandler(this.checkBoxCustomDate_CheckedChanged);

            // 
            // checkBoxCustomApplicant
            // 
            this.checkBoxCustomApplicant.AutoSize = true;
            this.checkBoxCustomApplicant.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.checkBoxCustomApplicant.Location = new System.Drawing.Point(6, 44);
            this.checkBoxCustomApplicant.Name = "checkBoxCustomApplicant";
            this.checkBoxCustomApplicant.Size = new System.Drawing.Size(113, 19);
            this.checkBoxCustomApplicant.TabIndex = 3;
            this.checkBoxCustomApplicant.Text = "Свой заявитель";
            this.checkBoxCustomApplicant.UseVisualStyleBackColor = true;
            this.checkBoxCustomApplicant.CheckedChanged += new System.EventHandler(this.checkBoxCustomApplicant_CheckedChanged);

            // 
            // radioButtonProcessAll
            // 
            this.radioButtonProcessAll.AutoSize = true;
            this.radioButtonProcessAll.Checked = true;
            this.radioButtonProcessAll.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioButtonProcessAll.Location = new System.Drawing.Point(6, 25);
            this.radioButtonProcessAll.Name = "radioButtonProcessAll";
            this.radioButtonProcessAll.Size = new System.Drawing.Size(161, 19);
            this.radioButtonProcessAll.TabIndex = 0;
            this.radioButtonProcessAll.TabStop = true;
            this.radioButtonProcessAll.Text = "Все выбранные файлы";
            this.radioButtonProcessAll.UseVisualStyleBackColor = true;

            // 
            // radioButtonProcessSingle
            // 
            this.radioButtonProcessSingle.AutoSize = true;
            this.radioButtonProcessSingle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radioButtonProcessSingle.Location = new System.Drawing.Point(6, 47);
            this.radioButtonProcessSingle.Name = "radioButtonProcessSingle";
            this.radioButtonProcessSingle.Size = new System.Drawing.Size(131, 19);
            this.radioButtonProcessSingle.TabIndex = 1;
            this.radioButtonProcessSingle.Text = "Только текущий";
            this.radioButtonProcessSingle.UseVisualStyleBackColor = true;

            // 
            // panelSettings
            // 
            this.panelSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSettings.Controls.Add(this.panelApplicant);
            this.panelSettings.Controls.Add(this.panelDateTime);
            this.panelSettings.Location = new System.Drawing.Point(572, 203);
            this.panelSettings.Name = "panelSettings";
            this.panelSettings.Size = new System.Drawing.Size(300, 270);
            this.panelSettings.TabIndex = 5;

            // 
            // panelApplicant
            // 
            this.panelApplicant.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelApplicant.Controls.Add(this.labelPatronymic);
            this.panelApplicant.Controls.Add(this.labelFirstName);
            this.panelApplicant.Controls.Add(this.labelLastName);
            this.panelApplicant.Controls.Add(this.textBoxApplicantPatronymic);
            this.panelApplicant.Controls.Add(this.textBoxApplicantFirstName);
            this.panelApplicant.Controls.Add(this.textBoxApplicantLastName);
            this.panelApplicant.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelApplicant.Location = new System.Drawing.Point(0, 75);
            this.panelApplicant.Name = "panelApplicant";
            this.panelApplicant.Padding = new System.Windows.Forms.Padding(5);
            this.panelApplicant.Size = new System.Drawing.Size(300, 110);
            this.panelApplicant.TabIndex = 6;
            this.panelApplicant.Visible = false;

            // 
            // labelPatronymic
            // 
            this.labelPatronymic.AutoSize = true;
            this.labelPatronymic.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelPatronymic.Location = new System.Drawing.Point(8, 81);
            this.labelPatronymic.Name = "labelPatronymic";
            this.labelPatronymic.Size = new System.Drawing.Size(58, 15);
            this.labelPatronymic.TabIndex = 10;
            this.labelPatronymic.Text = "Отчество";

            // 
            // labelFirstName
            // 
            this.labelFirstName.AutoSize = true;
            this.labelFirstName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelFirstName.Location = new System.Drawing.Point(8, 53);
            this.labelFirstName.Name = "labelFirstName";
            this.labelFirstName.Size = new System.Drawing.Size(31, 15);
            this.labelFirstName.TabIndex = 9;
            this.labelFirstName.Text = "Имя";

            // 
            // labelLastName
            // 
            this.labelLastName.AutoSize = true;
            this.labelLastName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelLastName.Location = new System.Drawing.Point(8, 8);
            this.labelLastName.Name = "labelLastName";
            this.labelLastName.Size = new System.Drawing.Size(58, 15);
            this.labelLastName.TabIndex = 8;
            this.labelLastName.Text = "Фамилия";

            // 
            // textBoxApplicantPatronymic
            // 
            this.textBoxApplicantPatronymic.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxApplicantPatronymic.Location = new System.Drawing.Point(90, 78);
            this.textBoxApplicantPatronymic.Name = "textBoxApplicantPatronymic";
            this.textBoxApplicantPatronymic.Size = new System.Drawing.Size(196, 23);
            this.textBoxApplicantPatronymic.TabIndex = 2;

            // 
            // textBoxApplicantFirstName
            // 
            this.textBoxApplicantFirstName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxApplicantFirstName.Location = new System.Drawing.Point(90, 50);
            this.textBoxApplicantFirstName.Name = "textBoxApplicantFirstName";
            this.textBoxApplicantFirstName.Size = new System.Drawing.Size(196, 23);
            this.textBoxApplicantFirstName.TabIndex = 1;

            // 
            // textBoxApplicantLastName
            // 
            this.textBoxApplicantLastName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxApplicantLastName.Location = new System.Drawing.Point(90, 5);
            this.textBoxApplicantLastName.Name = "textBoxApplicantLastName";
            this.textBoxApplicantLastName.Size = new System.Drawing.Size(196, 23);
            this.textBoxApplicantLastName.TabIndex = 0;

            // 
            // panelDateTime
            // 
            this.panelDateTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDateTime.Controls.Add(this.labelTime);
            this.panelDateTime.Controls.Add(this.maskedTextBoxTime);
            this.panelDateTime.Controls.Add(this.labelDateTime);
            this.panelDateTime.Controls.Add(this.dateTimePickerDate);
            this.panelDateTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDateTime.Location = new System.Drawing.Point(0, 0);
            this.panelDateTime.Name = "panelDateTime";
            this.panelDateTime.Padding = new System.Windows.Forms.Padding(5);
            this.panelDateTime.Size = new System.Drawing.Size(300, 75);
            this.panelDateTime.TabIndex = 5;
            this.panelDateTime.Visible = false;

            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelTime.Location = new System.Drawing.Point(193, 28);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(42, 15);
            this.labelTime.TabIndex = 9;
            this.labelTime.Text = "Время";

            // 
            // maskedTextBoxTime
            // 
            this.maskedTextBoxTime.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.maskedTextBoxTime.Location = new System.Drawing.Point(196, 45);
            this.maskedTextBoxTime.Mask = "00:00";
            this.maskedTextBoxTime.Name = "maskedTextBoxTime";
            this.maskedTextBoxTime.Size = new System.Drawing.Size(60, 25);
            this.maskedTextBoxTime.TabIndex = 8;
            this.maskedTextBoxTime.Text = "1200";
            this.maskedTextBoxTime.ValidatingType = typeof(System.DateTime);

            // 
            // labelDateTime
            // 
            this.labelDateTime.AutoSize = true;
            this.labelDateTime.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelDateTime.Location = new System.Drawing.Point(8, 8);
            this.labelDateTime.Name = "labelDateTime";
            this.labelDateTime.Size = new System.Drawing.Size(172, 15);
            this.labelDateTime.TabIndex = 7;
            this.labelDateTime.Text = "Дата и время закрытия заявки";

            // 
            // dateTimePickerDate
            // 
            this.dateTimePickerDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dateTimePickerDate.Location = new System.Drawing.Point(8, 45);
            this.dateTimePickerDate.Name = "dateTimePickerDate";
            this.dateTimePickerDate.Size = new System.Drawing.Size(180, 23);
            this.dateTimePickerDate.TabIndex = 6;

            // 
            // timerRefresh
            // 
            this.timerRefresh.Interval = 30000;
            this.timerRefresh.Tick += new System.EventHandler(this.timerRefresh_Tick);

            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(12, 525);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(550, 20);
            this.progressBar.TabIndex = 6;
            this.progressBar.Visible = false;

            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.statusLabel,
                this.statusLabelCount});
            this.statusStrip.Location = new System.Drawing.Point(0, 550);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(884, 22);
            this.statusStrip.TabIndex = 7;

            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(39, 17);
            this.statusLabel.Text = "Готов";

            // 
            // statusLabelCount
            // 
            this.statusLabelCount.Name = "statusLabelCount";
            this.statusLabelCount.Size = new System.Drawing.Size(0, 17);

            // 
            // FormPDFFiller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(884, 572);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.panelSettings);
            this.Controls.Add(this.groupBoxMode);
            this.Controls.Add(this.dataGridViewFiles);
            this.Controls.Add(this.buttonProcess);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "FormPDFFiller";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Автозаполнение СЛ";
            this.Load += new System.EventHandler(this.FormPDFFiller_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFiles)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.groupBoxMode.ResumeLayout(false);
            this.groupBoxMode.PerformLayout();
            this.groupBoxOptions.ResumeLayout(false);
            this.groupBoxOptions.PerformLayout();
            this.panelSettings.ResumeLayout(false);
            this.panelApplicant.ResumeLayout(false);
            this.panelApplicant.PerformLayout();
            this.panelDateTime.ResumeLayout(false);
            this.panelDateTime.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button buttonProcess;
        private System.Windows.Forms.DataGridView dataGridViewFiles;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnFileName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn columnSelected;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuItemSettings;
        private System.Windows.Forms.ToolStripMenuItem menuItemSettingsOpen;
        private System.Windows.Forms.ToolStripMenuItem menuItemRefresh;
        private System.Windows.Forms.GroupBox groupBoxMode;
        private System.Windows.Forms.GroupBox groupBoxOptions;
        private System.Windows.Forms.CheckBox checkBoxMonthYearOnly;
        private System.Windows.Forms.CheckBox checkBoxCustomDate;
        private System.Windows.Forms.CheckBox checkBoxCustomApplicant;
        private System.Windows.Forms.RadioButton radioButtonProcessAll;
        private System.Windows.Forms.RadioButton radioButtonProcessSingle;
        private System.Windows.Forms.Panel panelSettings;
        private System.Windows.Forms.Panel panelApplicant;
        private System.Windows.Forms.Label labelPatronymic;
        private System.Windows.Forms.Label labelFirstName;
        private System.Windows.Forms.Label labelLastName;
        private System.Windows.Forms.TextBox textBoxApplicantPatronymic;
        private System.Windows.Forms.TextBox textBoxApplicantFirstName;
        private System.Windows.Forms.TextBox textBoxApplicantLastName;
        private System.Windows.Forms.Panel panelDateTime;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxTime;
        private System.Windows.Forms.Label labelDateTime;
        private System.Windows.Forms.DateTimePicker dateTimePickerDate;
        private System.Windows.Forms.Timer timerRefresh;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelCount;
    }
}

