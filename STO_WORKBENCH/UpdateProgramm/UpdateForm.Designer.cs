namespace STO_WORKBENCH.UpdateProgramm
{
    partial class UpdateForm
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
            lblCurrentVersion = new System.Windows.Forms.Label();
            panelBody = new System.Windows.Forms.Panel();
            lblVersionBadgeCaption = new System.Windows.Forms.Label();
            lblVersionBadge = new System.Windows.Forms.Label();
            lblNotesCaption = new System.Windows.Forms.Label();
            rtbNotes = new System.Windows.Forms.RichTextBox();
            panelProgress = new System.Windows.Forms.Panel();
            lblProgressCaption = new System.Windows.Forms.Label();
            pgDownload = new System.Windows.Forms.ProgressBar();
            lblPercent = new System.Windows.Forms.Label();
            separatorLine = new System.Windows.Forms.Panel();
            panelButtons = new System.Windows.Forms.Panel();
            btnUpdate = new System.Windows.Forms.Button();
            btnSkip = new System.Windows.Forms.Button();

            panelTop.SuspendLayout();
            panelBody.SuspendLayout();
            panelProgress.SuspendLayout();
            panelButtons.SuspendLayout();
            SuspendLayout();

            // ── panelTop ──────────────────────────────────────────────────
            panelTop.BackColor = System.Drawing.Color.FromArgb(22, 28, 40);
            panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            panelTop.Height = 80;
            panelTop.Controls.AddRange(new System.Windows.Forms.Control[]
                { lblTitle, lblCurrentVersion });

            lblTitle.Text = "🔄  Доступно обновление";
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 13f, System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.White;
            lblTitle.Location = new System.Drawing.Point(20, 16);
            lblTitle.Size = new System.Drawing.Size(420, 26);

            lblCurrentVersion.Text = "Установлена версия: ...";
            lblCurrentVersion.Font = new System.Drawing.Font("Segoe UI", 8.5f);
            lblCurrentVersion.ForeColor = System.Drawing.Color.FromArgb(120, 140, 170);
            lblCurrentVersion.Location = new System.Drawing.Point(20, 48);
            lblCurrentVersion.Size = new System.Drawing.Size(420, 18);

            // ── panelBody ─────────────────────────────────────────────────
            panelBody.BackColor = System.Drawing.Color.FromArgb(242, 244, 248);
            panelBody.Dock = System.Windows.Forms.DockStyle.Top;
            panelBody.Height = 200;
            panelBody.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                lblVersionBadgeCaption, lblVersionBadge,
                lblNotesCaption, rtbNotes
            });

            lblVersionBadgeCaption.Text = "Новая версия:";
            lblVersionBadgeCaption.Font = new System.Drawing.Font("Segoe UI", 8.5f);
            lblVersionBadgeCaption.ForeColor = System.Drawing.Color.FromArgb(100, 110, 130);
            lblVersionBadgeCaption.Location = new System.Drawing.Point(20, 18);
            lblVersionBadgeCaption.Size = new System.Drawing.Size(100, 18);

            lblVersionBadge.Text = "";
            lblVersionBadge.Font = new System.Drawing.Font("Segoe UI", 11f, System.Drawing.FontStyle.Bold);
            lblVersionBadge.ForeColor = System.Drawing.Color.FromArgb(0, 122, 204);
            lblVersionBadge.Location = new System.Drawing.Point(124, 15);
            lblVersionBadge.Size = new System.Drawing.Size(200, 24);

            lblNotesCaption.Text = "Что нового:";
            lblNotesCaption.Font = new System.Drawing.Font("Segoe UI", 8.5f, System.Drawing.FontStyle.Bold);
            lblNotesCaption.ForeColor = System.Drawing.Color.FromArgb(80, 90, 110);
            lblNotesCaption.Location = new System.Drawing.Point(20, 50);
            lblNotesCaption.Size = new System.Drawing.Size(420, 18);

            rtbNotes.Location = new System.Drawing.Point(20, 72);
            rtbNotes.Size = new System.Drawing.Size(420, 112);
            rtbNotes.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            rtbNotes.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
            rtbNotes.BackColor = System.Drawing.Color.White;
            rtbNotes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            rtbNotes.ReadOnly = true;
            rtbNotes.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            rtbNotes.TabStop = false;

            // ── panelProgress ─────────────────────────────────────────────
            panelProgress.BackColor = System.Drawing.Color.FromArgb(235, 237, 242);
            panelProgress.Dock = System.Windows.Forms.DockStyle.Top;
            panelProgress.Height = 60;
            panelProgress.Visible = false;
            panelProgress.Controls.AddRange(new System.Windows.Forms.Control[]
                { lblProgressCaption, pgDownload, lblPercent });

            lblProgressCaption.Text = "Загрузка обновления...";
            lblProgressCaption.Font = new System.Drawing.Font("Segoe UI", 8.5f);
            lblProgressCaption.ForeColor = System.Drawing.Color.FromArgb(80, 90, 110);
            lblProgressCaption.Location = new System.Drawing.Point(20, 10);
            lblProgressCaption.Size = new System.Drawing.Size(300, 16);

            pgDownload.Location = new System.Drawing.Point(20, 32);
            pgDownload.Size = new System.Drawing.Size(374, 8);
            pgDownload.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            pgDownload.BackColor = System.Drawing.Color.FromArgb(210, 215, 225);
            pgDownload.ForeColor = System.Drawing.Color.FromArgb(0, 122, 204);

            lblPercent.Text = "0%";
            lblPercent.Font = new System.Drawing.Font("Segoe UI", 8.5f, System.Drawing.FontStyle.Bold);
            lblPercent.ForeColor = System.Drawing.Color.FromArgb(0, 122, 204);
            lblPercent.Location = new System.Drawing.Point(400, 28);
            lblPercent.Size = new System.Drawing.Size(40, 18);
            lblPercent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // ── separatorLine ─────────────────────────────────────────────
            separatorLine.BackColor = System.Drawing.Color.FromArgb(210, 215, 225);
            separatorLine.Dock = System.Windows.Forms.DockStyle.Bottom;
            separatorLine.Height = 1;

            // ── panelButtons ──────────────────────────────────────────────
            panelButtons.BackColor = System.Drawing.Color.FromArgb(242, 244, 248);
            panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            panelButtons.Height = 60;
            panelButtons.Controls.AddRange(new System.Windows.Forms.Control[]
                { btnUpdate, btnSkip });

            btnUpdate.Text = "▶  Обновить сейчас";
            btnUpdate.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            btnUpdate.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            btnUpdate.ForeColor = System.Drawing.Color.White;
            btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnUpdate.FlatAppearance.BorderSize = 0;
            btnUpdate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(0, 102, 180);
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Size = new System.Drawing.Size(170, 32);
            btnUpdate.Location = new System.Drawing.Point(268, 14);
            btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);

            btnSkip.Text = "Пропустить";
            btnSkip.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            btnSkip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSkip.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(180, 190, 210);
            btnSkip.BackColor = System.Drawing.Color.White;
            btnSkip.ForeColor = System.Drawing.Color.FromArgb(60, 70, 90);
            btnSkip.Size = new System.Drawing.Size(130, 32);
            btnSkip.Location = new System.Drawing.Point(128, 14);
            btnSkip.Click += new System.EventHandler(this.btnSkip_Click);

            // ── Form ──────────────────────────────────────────────────────
            Text = "Обновление STO Workbench";
            ClientSize = new System.Drawing.Size(460, 340);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            BackColor = System.Drawing.Color.FromArgb(242, 244, 248);
            Name = "UpdateForm";

            // Bottom/Top — порядок важен
            Controls.Add(panelButtons);
            Controls.Add(separatorLine);
            Controls.Add(panelProgress);
            Controls.Add(panelBody);
            Controls.Add(panelTop);

            panelTop.ResumeLayout(false);
            panelBody.ResumeLayout(false);
            panelProgress.ResumeLayout(false);
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCurrentVersion;
        private System.Windows.Forms.Panel panelBody;
        private System.Windows.Forms.Label lblVersionBadgeCaption;
        private System.Windows.Forms.Label lblVersionBadge;
        private System.Windows.Forms.Label lblNotesCaption;
        private System.Windows.Forms.RichTextBox rtbNotes;
        private System.Windows.Forms.Panel panelProgress;
        private System.Windows.Forms.Label lblProgressCaption;
        private System.Windows.Forms.ProgressBar pgDownload;
        private System.Windows.Forms.Label lblPercent;
        private System.Windows.Forms.Panel separatorLine;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnSkip;
    }
}