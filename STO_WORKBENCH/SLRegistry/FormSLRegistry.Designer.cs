namespace STO_WORKBENCH.SLRegistry
{
    partial class FormSLRegistry
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
            panelTop = new System.Windows.Forms.Panel();
            lblTitle = new System.Windows.Forms.Label();
            lblSubtitle = new System.Windows.Forms.Label();
            panelControls = new System.Windows.Forms.Panel();
            grpParams = new System.Windows.Forms.GroupBox();
            labelPeriod = new System.Windows.Forms.Label();
            textBoxPeriod = new System.Windows.Forms.TextBox();
            labelProject = new System.Windows.Forms.Label();
            textBoxProject = new System.Windows.Forms.TextBox();
            labelPath = new System.Windows.Forms.Label();
            textBoxPath = new System.Windows.Forms.TextBox();
            buttonPath = new System.Windows.Forms.Button();
            buttonBuildAll = new System.Windows.Forms.Button();
            panelLog = new System.Windows.Forms.Panel();
            lblLogTitle = new System.Windows.Forms.Label();
            richTextBoxLog = new System.Windows.Forms.RichTextBox();
            folderBrowserDialogSLRegistry = new System.Windows.Forms.FolderBrowserDialog();

            panelTop.SuspendLayout();
            panelControls.SuspendLayout();
            grpParams.SuspendLayout();
            panelLog.SuspendLayout();
            SuspendLayout();

            // ── panelTop (шапка) ──────────────────────────────────────────
            panelTop.BackColor = System.Drawing.Color.FromArgb(22, 28, 40);
            panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            panelTop.Height = 72;
            panelTop.Controls.AddRange(new System.Windows.Forms.Control[]
                { lblTitle, lblSubtitle });

            lblTitle.Text = "📄  Сервисные листы — Реестр";
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 13f, System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.White;
            lblTitle.Location = new System.Drawing.Point(20, 14);
            lblTitle.Size = new System.Drawing.Size(500, 26);

            lblSubtitle.Text = "Заполните параметры и выберите папку с det_*.xls файлами";
            lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 8.5f);
            lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(120, 140, 170);
            lblSubtitle.Location = new System.Drawing.Point(20, 44);
            lblSubtitle.Size = new System.Drawing.Size(500, 18);

            // ── panelControls ─────────────────────────────────────────────
            panelControls.BackColor = System.Drawing.Color.FromArgb(242, 244, 248);
            panelControls.Dock = System.Windows.Forms.DockStyle.Top;
            panelControls.Height = 174;
            panelControls.Padding = new System.Windows.Forms.Padding(16, 10, 16, 10);
            panelControls.Controls.AddRange(new System.Windows.Forms.Control[]
                { grpParams, buttonBuildAll });

            // ── grpParams ─────────────────────────────────────────────────
            grpParams.Text = "Параметры реестра";
            grpParams.Font = new System.Drawing.Font("Segoe UI", 9f);
            grpParams.Location = new System.Drawing.Point(16, 10);
            grpParams.Size = new System.Drawing.Size(660, 148);
            grpParams.ForeColor = System.Drawing.Color.FromArgb(80, 90, 110);
            grpParams.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                labelPeriod, textBoxPeriod,
                labelProject, textBoxProject,
                labelPath, textBoxPath, buttonPath
            });

            // Отчётный период
            labelPeriod.Text = "Отчётный период";
            labelPeriod.Font = new System.Drawing.Font("Segoe UI", 8.5f);
            labelPeriod.ForeColor = System.Drawing.Color.FromArgb(80, 90, 110);
            labelPeriod.Location = new System.Drawing.Point(12, 22);
            labelPeriod.Size = new System.Drawing.Size(190, 16);

            textBoxPeriod.Location = new System.Drawing.Point(12, 40);
            textBoxPeriod.Size = new System.Drawing.Size(190, 23);
            textBoxPeriod.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            textBoxPeriod.BackColor = System.Drawing.Color.White;
            textBoxPeriod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Название проекта
            labelProject.Text = "Название проекта";
            labelProject.Font = new System.Drawing.Font("Segoe UI", 8.5f);
            labelProject.ForeColor = System.Drawing.Color.FromArgb(80, 90, 110);
            labelProject.Location = new System.Drawing.Point(220, 22);
            labelProject.Size = new System.Drawing.Size(190, 16);

            textBoxProject.Location = new System.Drawing.Point(220, 40);
            textBoxProject.Size = new System.Drawing.Size(190, 23);
            textBoxProject.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            textBoxProject.BackColor = System.Drawing.Color.White;
            textBoxProject.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Папка с файлами
            labelPath.Text = "Папка с det_*.xls файлами";
            labelPath.Font = new System.Drawing.Font("Segoe UI", 8.5f);
            labelPath.ForeColor = System.Drawing.Color.FromArgb(80, 90, 110);
            labelPath.Location = new System.Drawing.Point(12, 80);
            labelPath.Size = new System.Drawing.Size(300, 16);

            textBoxPath.Location = new System.Drawing.Point(12, 98);
            textBoxPath.Size = new System.Drawing.Size(554, 23);
            textBoxPath.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            textBoxPath.BackColor = System.Drawing.Color.White;
            textBoxPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBoxPath.ReadOnly = true;

            buttonPath.Text = "Обзор...";
            buttonPath.Location = new System.Drawing.Point(574, 97);
            buttonPath.Size = new System.Drawing.Size(74, 26);
            buttonPath.Font = new System.Drawing.Font("Segoe UI", 9f);
            buttonPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonPath.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(180, 190, 210);
            buttonPath.BackColor = System.Drawing.Color.White;
            buttonPath.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
            buttonPath.Click += buttonPath_Click;

            // ── buttonBuildAll ────────────────────────────────────────────
            buttonBuildAll.Text = "▶  Создать реестры";
            buttonBuildAll.Location = new System.Drawing.Point(686, 10);
            buttonBuildAll.Size = new System.Drawing.Size(148, 148);
            buttonBuildAll.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
            buttonBuildAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonBuildAll.FlatAppearance.BorderSize = 0;
            buttonBuildAll.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            buttonBuildAll.ForeColor = System.Drawing.Color.White;
            buttonBuildAll.Enabled = false;
            buttonBuildAll.UseVisualStyleBackColor = false;
            buttonBuildAll.Click += buttonBuildAll_Click;

            // ── panelLog ──────────────────────────────────────────────────
            panelLog.BackColor = System.Drawing.Color.FromArgb(235, 237, 242);
            panelLog.Dock = System.Windows.Forms.DockStyle.Fill;
            panelLog.Padding = new System.Windows.Forms.Padding(20, 8, 20, 16);
            panelLog.Controls.AddRange(new System.Windows.Forms.Control[]
                { lblLogTitle, richTextBoxLog });

            lblLogTitle.Text = "Лог выполнения";
            lblLogTitle.Font = new System.Drawing.Font("Segoe UI", 8.5f, System.Drawing.FontStyle.Bold);
            lblLogTitle.ForeColor = System.Drawing.Color.FromArgb(100, 110, 130);
            lblLogTitle.Location = new System.Drawing.Point(20, 8);
            lblLogTitle.Size = new System.Drawing.Size(200, 18);

            richTextBoxLog.Location = new System.Drawing.Point(20, 30);
            richTextBoxLog.Size = new System.Drawing.Size(800, 200);
            richTextBoxLog.Anchor =
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right;
            richTextBoxLog.Font = new System.Drawing.Font("Consolas", 9f);
            richTextBoxLog.BackColor = System.Drawing.Color.FromArgb(24, 24, 24);
            richTextBoxLog.ForeColor = System.Drawing.Color.FromArgb(180, 220, 180);
            richTextBoxLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            richTextBoxLog.ReadOnly = true;
            richTextBoxLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;

            // ── Form ──────────────────────────────────────────────────────
            AutoScaleDimensions = new System.Drawing.SizeF(7f, 15f);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(860, 560);
            MinimumSize = new System.Drawing.Size(720, 480);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Сервисные листы — Реестр";
            Name = "FormSLRegistry";
            BackColor = System.Drawing.Color.FromArgb(242, 244, 248);

            Controls.Add(panelLog);
            Controls.Add(panelControls);
            Controls.Add(panelTop);

            panelTop.ResumeLayout(false);
            panelControls.ResumeLayout(false);
            grpParams.ResumeLayout(false);
            panelLog.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelControls;
        private System.Windows.Forms.GroupBox grpParams;
        private System.Windows.Forms.Panel panelLog;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblLogTitle;
        private System.Windows.Forms.Label labelPeriod;
        private System.Windows.Forms.Label labelProject;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.TextBox textBoxPeriod;
        private System.Windows.Forms.TextBox textBoxProject;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Button buttonPath;
        private System.Windows.Forms.Button buttonBuildAll;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogSLRegistry;
    }
}