namespace STO_WORKBENCH.FileNameExporter
{
    partial class FormFileNameExporter
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">true, если управляемый ресурс должен быть удалён; иначе false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        private void InitializeComponent()
        {
            this.lblFolder = new System.Windows.Forms.Label();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.lblSearchPattern = new System.Windows.Forms.Label();
            this.txtSearchPattern = new System.Windows.Forms.TextBox();
            this.lblSearchHint = new System.Windows.Forms.Label();
            this.lblSeparator = new System.Windows.Forms.Label();
            this.txtSeparator = new System.Windows.Forms.TextBox();
            this.chkIncludeSubfolders = new System.Windows.Forms.CheckBox();
            this.chkIncludeOriginal = new System.Windows.Forms.CheckBox();
            this.lblTemplate = new System.Windows.Forms.Label();
            this.txtTemplate = new System.Windows.Forms.TextBox();
            this.lblHelp = new System.Windows.Forms.Label();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.gridPreview = new System.Windows.Forms.DataGridView();
            this.panelTop = new System.Windows.Forms.Panel();
            this.panelButtons = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.gridPreview)).BeginInit();
            this.panelTop.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Location = new System.Drawing.Point(12, 15);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(43, 16);
            this.lblFolder.TabIndex = 0;
            this.lblFolder.Text = "Папка:";
            // 
            // txtFolder
            // 
            this.txtFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolder.Location = new System.Drawing.Point(150, 12);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(600, 22);
            this.txtFolder.TabIndex = 1;
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectFolder.Location = new System.Drawing.Point(765, 10);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(115, 27);
            this.btnSelectFolder.TabIndex = 2;
            this.btnSelectFolder.Text = "Выбрать...";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            // 
            // lblSearchPattern
            // 
            this.lblSearchPattern.AutoSize = true;
            this.lblSearchPattern.Location = new System.Drawing.Point(12, 52);
            this.lblSearchPattern.Name = "lblSearchPattern";
            this.lblSearchPattern.Size = new System.Drawing.Size(94, 16);
            this.lblSearchPattern.TabIndex = 3;
            this.lblSearchPattern.Text = "Фильтр файлов:";
            // 
            // txtSearchPattern
            // 
            this.txtSearchPattern.Location = new System.Drawing.Point(150, 49);
            this.txtSearchPattern.Name = "txtSearchPattern";
            this.txtSearchPattern.Size = new System.Drawing.Size(180, 22);
            this.txtSearchPattern.TabIndex = 4;
            this.txtSearchPattern.Text = "*.pdf";
            // 
            // lblSearchHint
            // 
            this.lblSearchHint.AutoSize = true;
            this.lblSearchHint.Location = new System.Drawing.Point(345, 52);
            this.lblSearchHint.Name = "lblSearchHint";
            this.lblSearchHint.Size = new System.Drawing.Size(328, 16);
            this.lblSearchHint.TabIndex = 5;
            this.lblSearchHint.Text = "Например: *.pdf или *.pdf;*.docx или *.*";
            // 
            // lblSeparator
            // 
            this.lblSeparator.AutoSize = true;
            this.lblSeparator.Location = new System.Drawing.Point(12, 88);
            this.lblSeparator.Name = "lblSeparator";
            this.lblSeparator.Size = new System.Drawing.Size(91, 16);
            this.lblSeparator.TabIndex = 6;
            this.lblSeparator.Text = "Разделитель:";
            // 
            // txtSeparator
            // 
            this.txtSeparator.Location = new System.Drawing.Point(150, 85);
            this.txtSeparator.Name = "txtSeparator";
            this.txtSeparator.Size = new System.Drawing.Size(80, 22);
            this.txtSeparator.TabIndex = 7;
            this.txtSeparator.Text = "_";
            // 
            // chkIncludeSubfolders
            // 
            this.chkIncludeSubfolders.AutoSize = true;
            this.chkIncludeSubfolders.Location = new System.Drawing.Point(250, 87);
            this.chkIncludeSubfolders.Name = "chkIncludeSubfolders";
            this.chkIncludeSubfolders.Size = new System.Drawing.Size(143, 20);
            this.chkIncludeSubfolders.TabIndex = 8;
            this.chkIncludeSubfolders.Text = "Включая подпапки";
            this.chkIncludeSubfolders.UseVisualStyleBackColor = true;
            // 
            // chkIncludeOriginal
            // 
            this.chkIncludeOriginal.AutoSize = true;
            this.chkIncludeOriginal.Checked = true;
            this.chkIncludeOriginal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeOriginal.Location = new System.Drawing.Point(420, 87);
            this.chkIncludeOriginal.Name = "chkIncludeOriginal";
            this.chkIncludeOriginal.Size = new System.Drawing.Size(281, 20);
            this.chkIncludeOriginal.TabIndex = 9;
            this.chkIncludeOriginal.Text = "Добавить исходное имя файла в Excel";
            this.chkIncludeOriginal.UseVisualStyleBackColor = true;
            // 
            // lblTemplate
            // 
            this.lblTemplate.AutoSize = true;
            this.lblTemplate.Location = new System.Drawing.Point(12, 125);
            this.lblTemplate.Name = "lblTemplate";
            this.lblTemplate.Size = new System.Drawing.Size(60, 16);
            this.lblTemplate.TabIndex = 10;
            this.lblTemplate.Text = "Шаблон:";
            // 
            // txtTemplate
            // 
            this.txtTemplate.Location = new System.Drawing.Point(150, 122);
            this.txtTemplate.Name = "txtTemplate";
            this.txtTemplate.Size = new System.Drawing.Size(300, 22);
            this.txtTemplate.TabIndex = 11;
            this.txtTemplate.Text = "{4}";
            // 
            // lblHelp
            // 
            this.lblHelp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHelp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHelp.Location = new System.Drawing.Point(15, 160);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Padding = new System.Windows.Forms.Padding(6);
            this.lblHelp.Size = new System.Drawing.Size(865, 105);
            this.lblHelp.TabIndex = 12;
            this.lblHelp.Text = "Пример файла: it21_sl_5op_863_m1131694.pdf\r\n"
                + "{0}=it21, {1}=sl, {2}=5op, {3}=863, {4}=m1131694\r\n"
                + "{ext}=.pdf, {name}=имя без расширения, {file}=полное имя файла\r\n"
                + "Примеры шаблонов:\r\n"
                + "{4} → m1131694     |     {0}_{1}_{2} → it21_sl_5op     |     {4}{ext} → m1131694.pdf";
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(150, 10);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(140, 32);
            this.btnPreview.TabIndex = 13;
            this.btnPreview.Text = "Предпросмотр";
            this.btnPreview.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(305, 10);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(150, 32);
            this.btnExport.TabIndex = 14;
            this.btnExport.Text = "Экспорт в Excel";
            this.btnExport.UseVisualStyleBackColor = true;
            // 
            // gridPreview
            // 
            this.gridPreview.AllowUserToAddRows = false;
            this.gridPreview.AllowUserToDeleteRows = false;
            this.gridPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.gridPreview.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridPreview.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gridPreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPreview.Location = new System.Drawing.Point(12, 335);
            this.gridPreview.MultiSelect = false;
            this.gridPreview.Name = "gridPreview";
            this.gridPreview.ReadOnly = true;
            this.gridPreview.RowHeadersWidth = 51;
            this.gridPreview.RowTemplate.Height = 24;
            this.gridPreview.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridPreview.Size = new System.Drawing.Size(868, 300);
            this.gridPreview.TabIndex = 15;
            // 
            // panelTop
            // 
            this.panelTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTop.Controls.Add(this.lblFolder);
            this.panelTop.Controls.Add(this.txtFolder);
            this.panelTop.Controls.Add(this.btnSelectFolder);
            this.panelTop.Controls.Add(this.lblSearchPattern);
            this.panelTop.Controls.Add(this.txtSearchPattern);
            this.panelTop.Controls.Add(this.lblSearchHint);
            this.panelTop.Controls.Add(this.lblSeparator);
            this.panelTop.Controls.Add(this.txtSeparator);
            this.panelTop.Controls.Add(this.chkIncludeSubfolders);
            this.panelTop.Controls.Add(this.chkIncludeOriginal);
            this.panelTop.Controls.Add(this.lblTemplate);
            this.panelTop.Controls.Add(this.txtTemplate);
            this.panelTop.Controls.Add(this.lblHelp);
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(895, 275);
            this.panelTop.TabIndex = 16;
            // 
            // panelButtons
            // 
            this.panelButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.panelButtons.Controls.Add(this.btnPreview);
            this.panelButtons.Controls.Add(this.btnExport);
            this.panelButtons.Location = new System.Drawing.Point(0, 275);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(895, 55);
            this.panelButtons.TabIndex = 17;
            // 
            // FormFileNameExporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 650);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.gridPreview);
            this.MinimumSize = new System.Drawing.Size(900, 650);
            this.Name = "FormFileNameExporter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Экспорт имён файлов в Excel";
            ((System.ComponentModel.ISupportInitialize)(this.gridPreview)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblFolder;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.Button btnSelectFolder;

        private System.Windows.Forms.Label lblSearchPattern;
        private System.Windows.Forms.TextBox txtSearchPattern;
        private System.Windows.Forms.Label lblSearchHint;

        private System.Windows.Forms.Label lblSeparator;
        private System.Windows.Forms.TextBox txtSeparator;

        private System.Windows.Forms.CheckBox chkIncludeSubfolders;
        private System.Windows.Forms.CheckBox chkIncludeOriginal;

        private System.Windows.Forms.Label lblTemplate;
        private System.Windows.Forms.TextBox txtTemplate;

        private System.Windows.Forms.Label lblHelp;

        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnExport;

        private System.Windows.Forms.DataGridView gridPreview;

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelButtons;
    }
}