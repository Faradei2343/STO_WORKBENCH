namespace STO_WORKBENCH
{
    partial class FormSort
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
            lblPathLabel = new System.Windows.Forms.Label();
            textBox1 = new System.Windows.Forms.TextBox();
            buttonPath = new System.Windows.Forms.Button();
            buttonSort = new System.Windows.Forms.Button();
            panelLog = new System.Windows.Forms.Panel();
            lblLogTitle = new System.Windows.Forms.Label();
            richTextBox1 = new System.Windows.Forms.RichTextBox();
            folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();

            panelTop.SuspendLayout();
            panelControls.SuspendLayout();
            panelLog.SuspendLayout();
            SuspendLayout();

            // ── panelTop (шапка) ──────────────────────────────────────────
            panelTop.BackColor = System.Drawing.Color.FromArgb(22, 28, 40);
            panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            panelTop.Height = 72;
            panelTop.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            panelTop.Controls.AddRange(new System.Windows.Forms.Control[]
                { lblTitle, lblSubtitle });

            lblTitle.Text = "📋  Сортировка детализаций";
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 13f, System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.White;
            lblTitle.Location = new System.Drawing.Point(20, 14);
            lblTitle.Size = new System.Drawing.Size(500, 26);

            lblSubtitle.Text = "Выберите папку и запустите сортировку файлов";
            lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 8.5f);
            lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(120, 140, 170);
            lblSubtitle.Location = new System.Drawing.Point(20, 44);
            lblSubtitle.Size = new System.Drawing.Size(500, 18);

            // ── panelControls (поле пути + кнопки) ───────────────────────
            panelControls.BackColor = System.Drawing.Color.FromArgb(242, 244, 248);
            panelControls.Dock = System.Windows.Forms.DockStyle.Top;
            panelControls.Height = 90;
            panelControls.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            panelControls.Controls.AddRange(new System.Windows.Forms.Control[]
                { lblPathLabel, textBox1, buttonPath, buttonSort });

            lblPathLabel.Text = "Папка с файлами:";
            lblPathLabel.Font = new System.Drawing.Font("Segoe UI", 9f);
            lblPathLabel.ForeColor = System.Drawing.Color.FromArgb(80, 90, 110);
            lblPathLabel.Location = new System.Drawing.Point(20, 14);
            lblPathLabel.Size = new System.Drawing.Size(140, 18);

            textBox1.Location = new System.Drawing.Point(20, 36);
            textBox1.Size = new System.Drawing.Size(530, 23);
            textBox1.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            textBox1.BackColor = System.Drawing.Color.White;
            textBox1.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
            textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBox1.ReadOnly = true;

            buttonPath.Text = "Обзор...";
            buttonPath.Location = new System.Drawing.Point(558, 35);
            buttonPath.Size = new System.Drawing.Size(90, 26);
            buttonPath.Font = new System.Drawing.Font("Segoe UI", 9f);
            buttonPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonPath.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(180, 190, 210);
            buttonPath.BackColor = System.Drawing.Color.White;
            buttonPath.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
            buttonPath.Click += buttonPath_Click;

            buttonSort.Text = "▶  Сортировать";
            buttonSort.Location = new System.Drawing.Point(656, 35);
            buttonSort.Size = new System.Drawing.Size(124, 26);
            buttonSort.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            buttonSort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonSort.FlatAppearance.BorderSize = 0;
            buttonSort.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            buttonSort.ForeColor = System.Drawing.Color.White;
            buttonSort.Enabled = false;
            buttonSort.UseVisualStyleBackColor = false;
            buttonSort.Click += buttonSort_Click;

            // ── panelLog (лог) ────────────────────────────────────────────
            panelLog.BackColor = System.Drawing.Color.FromArgb(235, 237, 242);
            panelLog.Dock = System.Windows.Forms.DockStyle.Fill;
            panelLog.Padding = new System.Windows.Forms.Padding(20, 8, 20, 16);
            panelLog.Controls.AddRange(new System.Windows.Forms.Control[]
                { lblLogTitle, richTextBox1 });

            lblLogTitle.Text = "Лог выполнения";
            lblLogTitle.Font = new System.Drawing.Font("Segoe UI", 8.5f, System.Drawing.FontStyle.Bold);
            lblLogTitle.ForeColor = System.Drawing.Color.FromArgb(100, 110, 130);
            lblLogTitle.Location = new System.Drawing.Point(20, 8);
            lblLogTitle.Size = new System.Drawing.Size(200, 18);

            richTextBox1.Location = new System.Drawing.Point(20, 30);
            richTextBox1.Size = new System.Drawing.Size(760, 270);
            richTextBox1.Anchor =
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right;
            richTextBox1.Font = new System.Drawing.Font("Consolas", 9f);
            richTextBox1.BackColor = System.Drawing.Color.FromArgb(24, 24, 24);
            richTextBox1.ForeColor = System.Drawing.Color.FromArgb(180, 220, 180);
            richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            richTextBox1.ReadOnly = true;
            richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;

            // ── Form ──────────────────────────────────────────────────────
            AutoScaleDimensions = new System.Drawing.SizeF(7f, 15f);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(820, 500);
            MinimumSize = new System.Drawing.Size(700, 420);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Сортировка детализаций";
            Name = "FormSort";
            BackColor = System.Drawing.Color.FromArgb(242, 244, 248);

            // Fill заполняет оставшееся место — добавляем первым
            Controls.Add(panelLog);
            Controls.Add(panelControls);
            Controls.Add(panelTop);

            panelTop.ResumeLayout(false);
            panelControls.ResumeLayout(false);
            panelLog.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelControls;
        private System.Windows.Forms.Panel panelLog;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblPathLabel;
        private System.Windows.Forms.Label lblLogTitle;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonPath;
        private System.Windows.Forms.Button buttonSort;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}