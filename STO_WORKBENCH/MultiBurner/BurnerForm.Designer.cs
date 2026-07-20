namespace STO_WORKBENCH.MultiBurner
{
    partial class BurnerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BurnerForm));
            panelTop = new Panel();
            lblTitle = new Label();
            lblSubtitle = new Label();
            panelControls = new Panel();
            lblRootFolder = new Label();
            txtRootFolder = new TextBox();
            btnSelectRootFolder = new Button();
            btnDetectDrives = new Button();
            btnStart = new Button();
            btnStop = new Button();
            panelMain = new Panel();
            panelJobs = new Panel();
            lblJobsTitle = new Label();
            gridJobs = new DataGridView();
            panelRight = new Panel();
            panelLog = new Panel();
            lblLogTitle = new Label();
            txtLog = new TextBox();
            panelDrives = new Panel();
            lblDrivesTitle = new Label();
            checkedDrives = new CheckedListBox();
            folderBrowserDialog1 = new FolderBrowserDialog();
            notifyIcon1 = new NotifyIcon(components);
            panelTop.SuspendLayout();
            panelControls.SuspendLayout();
            panelMain.SuspendLayout();
            panelJobs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridJobs).BeginInit();
            panelRight.SuspendLayout();
            panelLog.SuspendLayout();
            panelDrives.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.FromArgb(22, 28, 40);
            panelTop.Controls.Add(lblTitle);
            panelTop.Controls.Add(lblSubtitle);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Padding = new Padding(20, 0, 0, 0);
            panelTop.Size = new Size(1301, 72);
            panelTop.TabIndex = 2;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 14);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(640, 26);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "💿  Многопоточная запись дисков";
            // 
            // lblSubtitle
            // 
            lblSubtitle.Font = new Font("Segoe UI", 8.5F);
            lblSubtitle.ForeColor = Color.FromArgb(120, 140, 170);
            lblSubtitle.Location = new Point(20, 44);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(760, 18);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Запись содержимого подготовленных папок на несколько приводов одновременно";
            // 
            // panelControls
            // 
            panelControls.BackColor = Color.FromArgb(242, 244, 248);
            panelControls.Controls.Add(lblRootFolder);
            panelControls.Controls.Add(txtRootFolder);
            panelControls.Controls.Add(btnSelectRootFolder);
            panelControls.Controls.Add(btnStart);
            panelControls.Controls.Add(btnStop);
            panelControls.Dock = DockStyle.Top;
            panelControls.Location = new Point(0, 72);
            panelControls.Name = "panelControls";
            panelControls.Padding = new Padding(20, 0, 20, 0);
            panelControls.Size = new Size(1301, 92);
            panelControls.TabIndex = 1;
            // 
            // lblRootFolder
            // 
            lblRootFolder.Font = new Font("Segoe UI", 9F);
            lblRootFolder.ForeColor = Color.FromArgb(80, 90, 110);
            lblRootFolder.Location = new Point(20, 14);
            lblRootFolder.Name = "lblRootFolder";
            lblRootFolder.Size = new Size(280, 18);
            lblRootFolder.TabIndex = 0;
            lblRootFolder.Text = "Папка с подготовленными подпапками:";
            // 
            // txtRootFolder
            // 
            txtRootFolder.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtRootFolder.BackColor = Color.White;
            txtRootFolder.BorderStyle = BorderStyle.FixedSingle;
            txtRootFolder.Font = new Font("Segoe UI", 9.5F);
            txtRootFolder.ForeColor = Color.FromArgb(40, 40, 40);
            txtRootFolder.Location = new Point(20, 36);
            txtRootFolder.Name = "txtRootFolder";
            txtRootFolder.ReadOnly = true;
            txtRootFolder.Size = new Size(1151, 24);
            txtRootFolder.TabIndex = 1;
            // 
            // btnSelectRootFolder
            // 
            btnSelectRootFolder.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSelectRootFolder.BackColor = Color.White;
            btnSelectRootFolder.FlatAppearance.BorderColor = Color.FromArgb(180, 190, 210);
            btnSelectRootFolder.FlatStyle = FlatStyle.Flat;
            btnSelectRootFolder.Font = new Font("Segoe UI", 9F);
            btnSelectRootFolder.ForeColor = Color.FromArgb(40, 40, 40);
            btnSelectRootFolder.Location = new Point(1188, 36);
            btnSelectRootFolder.Name = "btnSelectRootFolder";
            btnSelectRootFolder.Size = new Size(90, 26);
            btnSelectRootFolder.TabIndex = 2;
            btnSelectRootFolder.Text = "Обзор...";
            btnSelectRootFolder.UseVisualStyleBackColor = false;
            btnSelectRootFolder.Click += btnSelectRootFolder_Click;
            // 
            // btnDetectDrives
            // 
            btnDetectDrives.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDetectDrives.BackColor = Color.White;
            btnDetectDrives.FlatAppearance.BorderColor = Color.FromArgb(180, 190, 210);
            btnDetectDrives.FlatStyle = FlatStyle.Flat;
            btnDetectDrives.Font = new Font("Segoe UI", 9F);
            btnDetectDrives.ForeColor = Color.FromArgb(40, 40, 40);
            btnDetectDrives.Location = new Point(222, 158);
            btnDetectDrives.Name = "btnDetectDrives";
            btnDetectDrives.Size = new Size(105, 26);
            btnDetectDrives.TabIndex = 3;
            btnDetectDrives.Text = "🔎  Приводы";
            btnDetectDrives.UseVisualStyleBackColor = false;
            btnDetectDrives.Click += btnDetectDrives_Click;
            // 
            // btnStart
            // 
            btnStart.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnStart.BackColor = Color.FromArgb(0, 122, 204);
            btnStart.FlatAppearance.BorderSize = 0;
            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnStart.ForeColor = Color.White;
            btnStart.Location = new Point(20, 63);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(95, 26);
            btnStart.TabIndex = 4;
            btnStart.Text = "▶  Старт";
            btnStart.UseVisualStyleBackColor = false;
            btnStart.Click += btnStart_Click;
            // 
            // btnStop
            // 
            btnStop.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnStop.BackColor = Color.FromArgb(190, 70, 70);
            btnStop.Enabled = false;
            btnStop.FlatAppearance.BorderSize = 0;
            btnStop.FlatStyle = FlatStyle.Flat;
            btnStop.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnStop.ForeColor = Color.White;
            btnStop.Location = new Point(121, 63);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(90, 26);
            btnStop.TabIndex = 5;
            btnStop.Text = "■  Стоп";
            btnStop.UseVisualStyleBackColor = false;
            btnStop.Click += btnStop_Click;
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(235, 237, 242);
            panelMain.Controls.Add(panelJobs);
            panelMain.Controls.Add(panelRight);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 164);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(20, 8, 20, 16);
            panelMain.Size = new Size(1301, 466);
            panelMain.TabIndex = 0;
            // 
            // panelJobs
            // 
            panelJobs.BackColor = Color.FromArgb(235, 237, 242);
            panelJobs.Controls.Add(lblJobsTitle);
            panelJobs.Controls.Add(gridJobs);
            panelJobs.Dock = DockStyle.Fill;
            panelJobs.Location = new Point(20, 8);
            panelJobs.Name = "panelJobs";
            panelJobs.Padding = new Padding(0, 0, 12, 0);
            panelJobs.Size = new Size(931, 442);
            panelJobs.TabIndex = 0;
            // 
            // lblJobsTitle
            // 
            lblJobsTitle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblJobsTitle.ForeColor = Color.FromArgb(100, 110, 130);
            lblJobsTitle.Location = new Point(0, 0);
            lblJobsTitle.Name = "lblJobsTitle";
            lblJobsTitle.Size = new Size(260, 18);
            lblJobsTitle.TabIndex = 0;
            lblJobsTitle.Text = "Очередь папок для записи";
            // 
            // gridJobs
            // 
            gridJobs.AllowUserToAddRows = false;
            gridJobs.AllowUserToDeleteRows = false;
            gridJobs.AllowUserToResizeRows = false;
            gridJobs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridJobs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridJobs.BackgroundColor = Color.White;
            gridJobs.BorderStyle = BorderStyle.None;
            gridJobs.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            gridJobs.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(230, 233, 240);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(70, 80, 100);
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            gridJobs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 8.5F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(40, 40, 40);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(210, 230, 250);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(30, 30, 30);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            gridJobs.DefaultCellStyle = dataGridViewCellStyle2;
            gridJobs.EnableHeadersVisualStyles = false;
            gridJobs.Location = new Point(3, 21);
            gridJobs.Name = "gridJobs";
            gridJobs.RowHeadersVisible = false;
            gridJobs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridJobs.Size = new Size(922, 418);
            gridJobs.TabIndex = 1;
            // 
            // panelRight
            // 
            panelRight.BackColor = Color.FromArgb(235, 237, 242);
            panelRight.Controls.Add(panelLog);
            panelRight.Controls.Add(panelDrives);
            panelRight.Dock = DockStyle.Right;
            panelRight.Location = new Point(951, 8);
            panelRight.Name = "panelRight";
            panelRight.Size = new Size(330, 442);
            panelRight.TabIndex = 1;
            // 
            // panelLog
            // 
            panelLog.BackColor = Color.FromArgb(235, 237, 242);
            panelLog.Controls.Add(lblLogTitle);
            panelLog.Controls.Add(txtLog);
            panelLog.Dock = DockStyle.Fill;
            panelLog.Location = new Point(0, 190);
            panelLog.Name = "panelLog";
            panelLog.Padding = new Padding(0, 8, 0, 0);
            panelLog.Size = new Size(330, 252);
            panelLog.TabIndex = 0;
            // 
            // lblLogTitle
            // 
            lblLogTitle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblLogTitle.ForeColor = Color.FromArgb(100, 110, 130);
            lblLogTitle.Location = new Point(0, 8);
            lblLogTitle.Name = "lblLogTitle";
            lblLogTitle.Size = new Size(220, 18);
            lblLogTitle.TabIndex = 0;
            lblLogTitle.Text = "Лог выполнения";
            // 
            // txtLog
            // 
            txtLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtLog.BackColor = Color.FromArgb(24, 24, 24);
            txtLog.BorderStyle = BorderStyle.None;
            txtLog.Font = new Font("Consolas", 9F);
            txtLog.ForeColor = Color.FromArgb(180, 220, 180);
            txtLog.Location = new Point(0, 32);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new Size(327, 217);
            txtLog.TabIndex = 1;
            // 
            // panelDrives
            // 
            panelDrives.BackColor = Color.FromArgb(235, 237, 242);
            panelDrives.Controls.Add(lblDrivesTitle);
            panelDrives.Controls.Add(checkedDrives);
            panelDrives.Controls.Add(btnDetectDrives);
            panelDrives.Dock = DockStyle.Top;
            panelDrives.Location = new Point(0, 0);
            panelDrives.Name = "panelDrives";
            panelDrives.Padding = new Padding(0, 0, 0, 10);
            panelDrives.Size = new Size(330, 190);
            panelDrives.TabIndex = 1;
            // 
            // lblDrivesTitle
            // 
            lblDrivesTitle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblDrivesTitle.ForeColor = Color.FromArgb(100, 110, 130);
            lblDrivesTitle.Location = new Point(0, 0);
            lblDrivesTitle.Name = "lblDrivesTitle";
            lblDrivesTitle.Size = new Size(220, 18);
            lblDrivesTitle.TabIndex = 0;
            lblDrivesTitle.Text = "Доступные приводы";
            // 
            // checkedDrives
            // 
            checkedDrives.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            checkedDrives.BackColor = Color.White;
            checkedDrives.BorderStyle = BorderStyle.None;
            checkedDrives.CheckOnClick = true;
            checkedDrives.Font = new Font("Segoe UI", 8.5F);
            checkedDrives.ForeColor = Color.FromArgb(40, 40, 40);
            checkedDrives.HorizontalScrollbar = true;
            checkedDrives.Location = new Point(0, 24);
            checkedDrives.Name = "checkedDrives";
            checkedDrives.Size = new Size(330, 126);
            checkedDrives.TabIndex = 1;
            // 
            // folderBrowserDialog1
            // 
            folderBrowserDialog1.Description = "Выберите папку с подготовленными подпапками для записи";
            // 
            // notifyIcon1
            // 
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "MultiDriveBurner";
            notifyIcon1.Visible = true;
            // 
            // BurnerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(242, 244, 248);
            ClientSize = new Size(1301, 630);
            Controls.Add(panelMain);
            Controls.Add(panelControls);
            Controls.Add(panelTop);
            MinimumSize = new Size(880, 560);
            Name = "BurnerForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Многопоточная запись дисков";
            panelTop.ResumeLayout(false);
            panelControls.ResumeLayout(false);
            panelControls.PerformLayout();
            panelMain.ResumeLayout(false);
            panelJobs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridJobs).EndInit();
            panelRight.ResumeLayout(false);
            panelLog.ResumeLayout(false);
            panelLog.PerformLayout();
            panelDrives.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;

        private System.Windows.Forms.Panel panelControls;
        private System.Windows.Forms.Label lblRootFolder;
        private System.Windows.Forms.TextBox txtRootFolder;
        private System.Windows.Forms.Button btnSelectRootFolder;
        private System.Windows.Forms.Button btnDetectDrives;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;

        private System.Windows.Forms.Panel panelMain;

        private System.Windows.Forms.Panel panelJobs;
        private System.Windows.Forms.Label lblJobsTitle;
        private System.Windows.Forms.DataGridView gridJobs;

        private System.Windows.Forms.Panel panelRight;

        private System.Windows.Forms.Panel panelDrives;
        private System.Windows.Forms.Label lblDrivesTitle;
        private System.Windows.Forms.CheckedListBox checkedDrives;

        private System.Windows.Forms.Panel panelLog;
        private System.Windows.Forms.Label lblLogTitle;
        private System.Windows.Forms.TextBox txtLog;

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }

        #endregion
    }