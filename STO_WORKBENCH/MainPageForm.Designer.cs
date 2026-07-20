namespace STO_WORKBENCH
{
    partial class MainPageForm
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
            panelLeft = new System.Windows.Forms.Panel();
            panelHeader = new System.Windows.Forms.Panel();
            lblAppTitle = new System.Windows.Forms.Label();
            lblAppSubtitle = new System.Windows.Forms.Label();
            buttonSort = new System.Windows.Forms.Button();
            buttonSL = new System.Windows.Forms.Button();
            buttonAutoSL = new System.Windows.Forms.Button();
            buttonZayavki = new System.Windows.Forms.Button();
            panelContent = new System.Windows.Forms.Panel();
            lblWelcome = new System.Windows.Forms.Label();
            lblWelcomeSub = new System.Windows.Forms.Label();
            buttonMultiburner = new System.Windows.Forms.Button();

            panelLeft.SuspendLayout();
            panelHeader.SuspendLayout();
            panelContent.SuspendLayout();
            SuspendLayout();

            // ── panelLeft ─────────────────────────────────────────────────
            panelLeft.BackColor = System.Drawing.Color.FromArgb(28, 35, 49);
            panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            panelLeft.Width = 220;
            panelLeft.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                panelHeader,
                buttonSort,
                buttonSL,
                buttonAutoSL,
                buttonZayavki,
                buttonMultiburner
            });

            // ── panelHeader ───────────────────────────────────────────────
            panelHeader.BackColor = System.Drawing.Color.FromArgb(22, 28, 40);
            panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            panelHeader.Height = 90;
            panelHeader.Controls.AddRange(new System.Windows.Forms.Control[]
                { lblAppTitle, lblAppSubtitle });

            lblAppTitle.Text = "STO WORKBENCH";
            lblAppTitle.Font = new System.Drawing.Font("Segoe UI", 11f, System.Drawing.FontStyle.Bold);
            lblAppTitle.ForeColor = System.Drawing.Color.White;
            lblAppTitle.Location = new System.Drawing.Point(18, 22);
            lblAppTitle.Size = new System.Drawing.Size(184, 24);

            lblAppSubtitle.Text = "Инструменты СТО";
            lblAppSubtitle.Font = new System.Drawing.Font("Segoe UI", 8.5f);
            lblAppSubtitle.ForeColor = System.Drawing.Color.FromArgb(120, 140, 170);
            lblAppSubtitle.Location = new System.Drawing.Point(18, 50);
            lblAppSubtitle.Size = new System.Drawing.Size(184, 18);

            // ── Кнопки навигации ──────────────────────────────────────────
            const int btnY = 90;
            const int btnH = 54;
            const int btnW = 220;

            StyleNavButton(buttonSort, "📋  Сортировка детализаций");
            buttonSort.Location = new System.Drawing.Point(0, btnY);
            buttonSort.Size = new System.Drawing.Size(btnW, btnH);
            buttonSort.Click += buttonSort_Click;

            StyleNavButton(buttonSL, "📄  Сервисные листы");
            buttonSL.Location = new System.Drawing.Point(0, btnY + btnH);
            buttonSL.Size = new System.Drawing.Size(btnW, btnH);
            buttonSL.Click += buttonSL_Click;

            StyleNavButton(buttonAutoSL, "✍  Авто подпись СЛ");
            buttonAutoSL.Location = new System.Drawing.Point(0, btnY + btnH * 2);
            buttonAutoSL.Size = new System.Drawing.Size(btnW, btnH);
            buttonAutoSL.Click += buttonAutoSL_Click;

            StyleNavButton(buttonZayavki, "📧  Заявки МФ");
            buttonZayavki.Location = new System.Drawing.Point(0, btnY + btnH * 3);
            buttonZayavki.Size = new System.Drawing.Size(btnW, btnH);
            buttonZayavki.Click += buttonZayavki_Click;

            StyleNavButton(buttonMultiburner, "Мультизапись дисков");
            buttonMultiburner.Location = new System.Drawing.Point(0, btnY + btnH * 4);
            buttonMultiburner.Size = new System.Drawing.Size(btnW, btnH);
            buttonMultiburner.Click += buttonMultiburner_Click;

            // ── panelContent ──────────────────────────────────────────────
            panelContent.BackColor = System.Drawing.Color.FromArgb(242, 244, 248);
            panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            panelContent.Controls.AddRange(new System.Windows.Forms.Control[]
                { lblWelcome, lblWelcomeSub });

            lblWelcome.Text = "Добро пожаловать";
            lblWelcome.Font = new System.Drawing.Font("Segoe UI", 20f, System.Drawing.FontStyle.Bold);
            lblWelcome.ForeColor = System.Drawing.Color.FromArgb(28, 35, 49);
            lblWelcome.AutoSize = false;
            lblWelcome.Size = new System.Drawing.Size(460, 48);
            lblWelcome.Location = new System.Drawing.Point(40, 180);
            lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            lblWelcomeSub.Text = "Выберите инструмент в меню слева, чтобы начать работу.";
            lblWelcomeSub.Font = new System.Drawing.Font("Segoe UI", 10.5f);
            lblWelcomeSub.ForeColor = System.Drawing.Color.FromArgb(100, 110, 130);
            lblWelcomeSub.AutoSize = false;
            lblWelcomeSub.Size = new System.Drawing.Size(520, 30);
            lblWelcomeSub.Location = new System.Drawing.Point(40, 236);
            lblWelcomeSub.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── Form ──────────────────────────────────────────────────────
            AutoScaleDimensions = new System.Drawing.SizeF(7f, 15f);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(860, 560);
            MinimumSize = new System.Drawing.Size(700, 460);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "STO Workbench";
            Name = "MainPageForm";
            BackColor = System.Drawing.Color.FromArgb(242, 244, 248);

            Controls.Add(panelContent);
            Controls.Add(panelLeft);

            Load += Form1_Load;

            panelLeft.ResumeLayout(false);
            panelHeader.ResumeLayout(false);
            panelContent.ResumeLayout(false);
            ResumeLayout(false);
        }

        private static void StyleNavButton(System.Windows.Forms.Button btn, string text)
        {
            btn.Text = text;
            btn.Font = new System.Drawing.Font("Segoe UI", 10f);
            btn.ForeColor = System.Drawing.Color.FromArgb(190, 200, 220);
            btn.BackColor = System.Drawing.Color.FromArgb(28, 35, 49);
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(45, 55, 75);
            btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(55, 68, 95);
            btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btn.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            btn.UseVisualStyleBackColor = false;
        }

        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Label lblAppTitle;
        private System.Windows.Forms.Label lblAppSubtitle;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblWelcomeSub;
        private System.Windows.Forms.Button buttonSort;
        private System.Windows.Forms.Button buttonSL;
        private System.Windows.Forms.Button buttonAutoSL;
        private System.Windows.Forms.Button buttonZayavki;
        private System.Windows.Forms.Button buttonMultiburner;
    }
}
