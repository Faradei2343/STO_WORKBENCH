namespace STO_WORKBENCH.FileNameExporter
{
    partial class FormFileNameExporter
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblFolder = new System.Windows.Forms.Label();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.lblSearchPattern = new System.Windows.Forms.Label();
            this.txtSearchPattern = new System.Windows.Forms.TextBox();
            this.lblSeparator = new System.Windows.Forms.Label();
            this.txtSeparator = new System.Windows.Forms.TextBox();
            this.chkIncludeSubfolders = new System.Windows.Forms.CheckBox();
            this.chkIncludeOriginal = new System.Windows.Forms.CheckBox();
            this.lblTemplate = new System.Windows.Forms.Label();
            this.txtTemplate = new System.Windows.Forms.TextBox();
            this.lblHelp = new System.Windows.Forms.Label();
            this.chkRemoveDuplicates = new System.Windows.Forms.CheckBox();
            this.chkIgnoreCaseDuplicates = new System.Windows.Forms.CheckBox();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.gridPreview = new System.Windows.Forms.DataGridView();
            this.groupMain = new System.Windows.Forms.GroupBox();
            this.groupDuplicates = new System.Windows.Forms.GroupBox();
            this.groupOrganize = new System.Windows.Forms.GroupBox();
            this.chkOrganizeFiles = new System.Windows.Forms.CheckBox();
            this.lblOutputRootFolder = new System.Windows.Forms.Label();
            this.txtOutputRootFolder = new System.Windows.Forms.TextBox();
            this.btnSelectOutputRootFolder = new System.Windows.Forms.Button();
            this.chkCopyFiles = new System.Windows.Forms.CheckBox();
            this.btnOrganizeFiles = new System.Windows.Forms.Button();
            this.groupGrouping = new System.Windows.Forms.GroupBox();
            this.chkEnableGrouping = new System.Windows.Forms.CheckBox();
            this.lblGroupKeyTemplate = new System.Windows.Forms.Label();
            this.txtGroupKeyTemplate = new System.Windows.Forms.TextBox();
            this.lblGroupFolderTemplate = new System.Windows.Forms.Label();
            this.txtGroupFolderTemplate = new System.Windows.Forms.TextBox();
            this.groupSortRules = new System.Windows.Forms.GroupBox();
            this.gridSortRules = new System.Windows.Forms.DataGridView();
            this.groupPreview = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridPreview)).BeginInit();
            this.groupMain.SuspendLayout();
            this.groupDuplicates.SuspendLayout();
            this.groupOrganize.SuspendLayout();
            this.groupGrouping.SuspendLayout();
            this.groupSortRules.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSortRules)).BeginInit();
            this.groupPreview.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Location = new System.Drawing.Point(15, 28);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(43, 16);
            this.lblFolder.TabIndex = 0;
            this.lblFolder.Text = "Папка:";
            // 
            // txtFolder
            // 
            this.txtFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolder.Location = new System.Drawing.Point(160, 25);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(660, 22);
            this.txtFolder.TabIndex = 1;
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectFolder.Location = new System.Drawing.Point(835, 22);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(120, 28);
            this.btnSelectFolder.TabIndex = 2;
            this.btnSelectFolder.Text = "Выбрать...";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            // 
            // lblSearchPattern
            // 
            this.lblSearchPattern.AutoSize = true;
            this.lblSearchPattern.Location = new System.Drawing.Point(15, 62);
            this.lblSearchPattern.Name = "lblSearchPattern";
            this.lblSearchPattern.Size = new System.Drawing.Size(105, 16);
            this.lblSearchPattern.TabIndex = 3;
            this.lblSearchPattern.Text = "Фильтр файлов:";
            // 
            // txtSearchPattern
            // 
            this.txtSearchPattern.Location = new System.Drawing.Point(160, 59);
            this.txtSearchPattern.Name = "txtSearchPattern";
            this.txtSearchPattern.Size = new System.Drawing.Size(180, 22);
            this.txtSearchPattern.TabIndex = 4;
            this.txtSearchPattern.Text = "*.*";
            // 
            // lblSeparator
            // 
            this.lblSeparator.AutoSize = true;
            this.lblSeparator.Location = new System.Drawing.Point(370, 62);
            this.lblSeparator.Name = "lblSeparator";
            this.lblSeparator.Size = new System.Drawing.Size(91, 16);
            this.lblSeparator.TabIndex = 5;
            this.lblSeparator.Text = "Разделитель:";
            // 
            // txtSeparator
            // 
            this.txtSeparator.Location = new System.Drawing.Point(470, 59);
            this.txtSeparator.Name = "txtSeparator";
            this.txtSeparator.Size = new System.Drawing.Size(70, 22);
            this.txtSeparator.TabIndex = 6;
            this.txtSeparator.Text = "_";
            // 
            // chkIncludeSubfolders
            // 
            this.chkIncludeSubfolders.AutoSize = true;
            this.chkIncludeSubfolders.Location = new System.Drawing.Point(570, 61);
            this.chkIncludeSubfolders.Name = "chkIncludeSubfolders";
            this.chkIncludeSubfolders.Size = new System.Drawing.Size(143, 20);
            this.chkIncludeSubfolders.TabIndex = 7;
            this.chkIncludeSubfolders.Text = "Включая подпапки";
            this.chkIncludeSubfolders.UseVisualStyleBackColor = true;
            // 
            // chkIncludeOriginal
            // 
            this.chkIncludeOriginal.AutoSize = true;
            this.chkIncludeOriginal.Checked = true;
            this.chkIncludeOriginal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeOriginal.Location = new System.Drawing.Point(730, 61);
            this.chkIncludeOriginal.Name = "chkIncludeOriginal";
            this.chkIncludeOriginal.Size = new System.Drawing.Size(203, 20);
            this.chkIncludeOriginal.TabIndex = 8;
            this.chkIncludeOriginal.Text = "Исходное имя файла в Excel";
            this.chkIncludeOriginal.UseVisualStyleBackColor = true;
            // 
            // lblTemplate
            // 
            this.lblTemplate.AutoSize = true;
            this.lblTemplate.Location = new System.Drawing.Point(15, 98);
            this.lblTemplate.Name = "lblTemplate";
            this.lblTemplate.Size = new System.Drawing.Size(60, 16);
            this.lblTemplate.TabIndex = 9;
            this.lblTemplate.Text = "Шаблон:";
            // 
            // txtTemplate
            // 
            this.txtTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTemplate.Location = new System.Drawing.Point(160, 95);
            this.txtTemplate.Name = "txtTemplate";
            this.txtTemplate.Size = new System.Drawing.Size(795, 22);
            this.txtTemplate.TabIndex = 10;
            this.txtTemplate.Text = "{name}";
            // 
            // lblHelp
            // 
            this.lblHelp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHelp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHelp.Location = new System.Drawing.Point(18, 130);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Padding = new System.Windows.Forms.Padding(7);
            this.lblHelp.Size = new System.Drawing.Size(937, 105);
            this.lblHelp.TabIndex = 11;
            this.lblHelp.Text = "Примеры шаблонов:\r\n" +
                "{3}_{4} → 863_m1131694\r\n" +
                "{3}_{4:0:4} → 863_m113\r\n" +
                "{match:m\\d+} → найти m1131694 в любой части имени\r\n" +
                "{match:№\\s*(\\d+):1} → взять номер после №";
            // 
            // chkRemoveDuplicates
            // 
            this.chkRemoveDuplicates.AutoSize = true;
            this.chkRemoveDuplicates.Location = new System.Drawing.Point(18, 28);
            this.chkRemoveDuplicates.Name = "chkRemoveDuplicates";
            this.chkRemoveDuplicates.Size = new System.Drawing.Size(123, 20);
            this.chkRemoveDuplicates.TabIndex = 0;
            this.chkRemoveDuplicates.Text = "Удалять дубли";
            this.chkRemoveDuplicates.UseVisualStyleBackColor = true;
            // 
            // chkIgnoreCaseDuplicates
            // 
            this.chkIgnoreCaseDuplicates.AutoSize = true;
            this.chkIgnoreCaseDuplicates.Checked = true;
            this.chkIgnoreCaseDuplicates.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIgnoreCaseDuplicates.Location = new System.Drawing.Point(180, 28);
            this.chkIgnoreCaseDuplicates.Name = "chkIgnoreCaseDuplicates";
            this.chkIgnoreCaseDuplicates.Size = new System.Drawing.Size(201, 20);
            this.chkIgnoreCaseDuplicates.TabIndex = 1;
            this.chkIgnoreCaseDuplicates.Text = "Дубли без учёта регистра";
            this.chkIgnoreCaseDuplicates.UseVisualStyleBackColor = true;
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(18, 55);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(145, 33);
            this.btnPreview.TabIndex = 2;
            this.btnPreview.Text = "Предпросмотр";
            this.btnPreview.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(180, 55);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(150, 33);
            this.btnExport.TabIndex = 3;
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
            this.gridPreview.Location = new System.Drawing.Point(15, 25);
            this.gridPreview.MultiSelect = false;
            this.gridPreview.Name = "gridPreview";
            this.gridPreview.ReadOnly = true;
            this.gridPreview.RowHeadersWidth = 51;
            this.gridPreview.RowTemplate.Height = 24;
            this.gridPreview.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridPreview.Size = new System.Drawing.Size(940, 230);
            this.gridPreview.TabIndex = 0;
            // 
            // groupMain
            // 
            this.groupMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.groupMain.Controls.Add(this.lblFolder);
            this.groupMain.Controls.Add(this.txtFolder);
            this.groupMain.Controls.Add(this.btnSelectFolder);
            this.groupMain.Controls.Add(this.lblSearchPattern);
            this.groupMain.Controls.Add(this.txtSearchPattern);
            this.groupMain.Controls.Add(this.lblSeparator);
            this.groupMain.Controls.Add(this.txtSeparator);
            this.groupMain.Controls.Add(this.chkIncludeSubfolders);
            this.groupMain.Controls.Add(this.chkIncludeOriginal);
            this.groupMain.Controls.Add(this.lblTemplate);
            this.groupMain.Controls.Add(this.txtTemplate);
            this.groupMain.Controls.Add(this.lblHelp);
            this.groupMain.Location = new System.Drawing.Point(12, 12);
            this.groupMain.Name = "groupMain";
            this.groupMain.Size = new System.Drawing.Size(970, 250);
            this.groupMain.TabIndex = 0;
            this.groupMain.TabStop = false;
            this.groupMain.Text = "Основные настройки";
            // 
            // groupDuplicates
            // 
            this.groupDuplicates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.groupDuplicates.Controls.Add(this.chkRemoveDuplicates);
            this.groupDuplicates.Controls.Add(this.chkIgnoreCaseDuplicates);
            this.groupDuplicates.Controls.Add(this.btnPreview);
            this.groupDuplicates.Controls.Add(this.btnExport);
            this.groupDuplicates.Location = new System.Drawing.Point(12, 268);
            this.groupDuplicates.Name = "groupDuplicates";
            this.groupDuplicates.Size = new System.Drawing.Size(970, 100);
            this.groupDuplicates.TabIndex = 1;
            this.groupDuplicates.TabStop = false;
            this.groupDuplicates.Text = "Excel и дубли";
            // 
            // groupOrganize
            // 
            this.groupOrganize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.groupOrganize.Controls.Add(this.chkOrganizeFiles);
            this.groupOrganize.Controls.Add(this.lblOutputRootFolder);
            this.groupOrganize.Controls.Add(this.txtOutputRootFolder);
            this.groupOrganize.Controls.Add(this.btnSelectOutputRootFolder);
            this.groupOrganize.Controls.Add(this.chkCopyFiles);
            this.groupOrganize.Controls.Add(this.btnOrganizeFiles);
            this.groupOrganize.Location = new System.Drawing.Point(12, 374);
            this.groupOrganize.Name = "groupOrganize";
            this.groupOrganize.Size = new System.Drawing.Size(970, 105);
            this.groupOrganize.TabIndex = 2;
            this.groupOrganize.TabStop = false;
            this.groupOrganize.Text = "Сортировка файлов по папкам";
            // 
            // chkOrganizeFiles
            // 
            this.chkOrganizeFiles.AutoSize = true;
            this.chkOrganizeFiles.Location = new System.Drawing.Point(18, 28);
            this.chkOrganizeFiles.Name = "chkOrganizeFiles";
            this.chkOrganizeFiles.Size = new System.Drawing.Size(203, 20);
            this.chkOrganizeFiles.TabIndex = 0;
            this.chkOrganizeFiles.Text = "Включить сортировку файлов";
            this.chkOrganizeFiles.UseVisualStyleBackColor = true;
            // 
            // lblOutputRootFolder
            // 
            this.lblOutputRootFolder.AutoSize = true;
            this.lblOutputRootFolder.Location = new System.Drawing.Point(15, 65);
            this.lblOutputRootFolder.Name = "lblOutputRootFolder";
            this.lblOutputRootFolder.Size = new System.Drawing.Size(143, 16);
            this.lblOutputRootFolder.TabIndex = 1;
            this.lblOutputRootFolder.Text = "Папка назначения:";
            // 
            // txtOutputRootFolder
            // 
            this.txtOutputRootFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutputRootFolder.Location = new System.Drawing.Point(160, 62);
            this.txtOutputRootFolder.Name = "txtOutputRootFolder";
            this.txtOutputRootFolder.Size = new System.Drawing.Size(600, 22);
            this.txtOutputRootFolder.TabIndex = 2;
            // 
            // btnSelectOutputRootFolder
            // 
            this.btnSelectOutputRootFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectOutputRootFolder.Location = new System.Drawing.Point(775, 58);
            this.btnSelectOutputRootFolder.Name = "btnSelectOutputRootFolder";
            this.btnSelectOutputRootFolder.Size = new System.Drawing.Size(115, 30);
            this.btnSelectOutputRootFolder.TabIndex = 3;
            this.btnSelectOutputRootFolder.Text = "Выбрать...";
            this.btnSelectOutputRootFolder.UseVisualStyleBackColor = true;
            // 
            // chkCopyFiles
            // 
            this.chkCopyFiles.AutoSize = true;
            this.chkCopyFiles.Checked = true;
            this.chkCopyFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCopyFiles.Location = new System.Drawing.Point(250, 28);
            this.chkCopyFiles.Name = "chkCopyFiles";
            this.chkCopyFiles.Size = new System.Drawing.Size(242, 20);
            this.chkCopyFiles.TabIndex = 4;
            this.chkCopyFiles.Text = "Копировать, не перемещать файлы";
            this.chkCopyFiles.UseVisualStyleBackColor = true;
            // 
            // btnOrganizeFiles
            // 
            this.btnOrganizeFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOrganizeFiles.Location = new System.Drawing.Point(775, 22);
            this.btnOrganizeFiles.Name = "btnOrganizeFiles";
            this.btnOrganizeFiles.Size = new System.Drawing.Size(180, 30);
            this.btnOrganizeFiles.TabIndex = 5;
            this.btnOrganizeFiles.Text = "Разложить файлы";
            this.btnOrganizeFiles.UseVisualStyleBackColor = true;
            // 
            // groupGrouping
            // 
            this.groupGrouping.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.groupGrouping.Controls.Add(this.chkEnableGrouping);
            this.groupGrouping.Controls.Add(this.lblGroupKeyTemplate);
            this.groupGrouping.Controls.Add(this.txtGroupKeyTemplate);
            this.groupGrouping.Controls.Add(this.lblGroupFolderTemplate);
            this.groupGrouping.Controls.Add(this.txtGroupFolderTemplate);
            this.groupGrouping.Location = new System.Drawing.Point(12, 485);
            this.groupGrouping.Name = "groupGrouping";
            this.groupGrouping.Size = new System.Drawing.Size(970, 115);
            this.groupGrouping.TabIndex = 3;
            this.groupGrouping.TabStop = false;
            this.groupGrouping.Text = "Группировка файлов";
            // 
            // chkEnableGrouping
            // 
            this.chkEnableGrouping.AutoSize = true;
            this.chkEnableGrouping.Location = new System.Drawing.Point(18, 27);
            this.chkEnableGrouping.Name = "chkEnableGrouping";
            this.chkEnableGrouping.Size = new System.Drawing.Size(168, 20);
            this.chkEnableGrouping.TabIndex = 0;
            this.chkEnableGrouping.Text = "Включить группировку";
            this.chkEnableGrouping.UseVisualStyleBackColor = true;
            // 
            // lblGroupKeyTemplate
            // 
            this.lblGroupKeyTemplate.AutoSize = true;
            this.lblGroupKeyTemplate.Location = new System.Drawing.Point(15, 58);
            this.lblGroupKeyTemplate.Name = "lblGroupKeyTemplate";
            this.lblGroupKeyTemplate.Size = new System.Drawing.Size(119, 16);
            this.lblGroupKeyTemplate.TabIndex = 1;
            this.lblGroupKeyTemplate.Text = "Шаблон группы:";
            // 
            // txtGroupKeyTemplate
            // 
            this.txtGroupKeyTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGroupKeyTemplate.Location = new System.Drawing.Point(160, 55);
            this.txtGroupKeyTemplate.Name = "txtGroupKeyTemplate";
            this.txtGroupKeyTemplate.Size = new System.Drawing.Size(795, 22);
            this.txtGroupKeyTemplate.TabIndex = 2;
            this.txtGroupKeyTemplate.Text = "{match:№\\s*(\\d+):1}";
            // 
            // lblGroupFolderTemplate
            // 
            this.lblGroupFolderTemplate.AutoSize = true;
            this.lblGroupFolderTemplate.Location = new System.Drawing.Point(15, 88);
            this.lblGroupFolderTemplate.Name = "lblGroupFolderTemplate";
            this.lblGroupFolderTemplate.Size = new System.Drawing.Size(127, 16);
            this.lblGroupFolderTemplate.TabIndex = 3;
            this.lblGroupFolderTemplate.Text = "Папка для группы:";
            // 
            // txtGroupFolderTemplate
            // 
            this.txtGroupFolderTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGroupFolderTemplate.Location = new System.Drawing.Point(160, 85);
            this.txtGroupFolderTemplate.Name = "txtGroupFolderTemplate";
            this.txtGroupFolderTemplate.Size = new System.Drawing.Size(795, 22);
            this.txtGroupFolderTemplate.TabIndex = 4;
            this.txtGroupFolderTemplate.Text = "{group}";
            // 
            // groupSortRules
            // 
            this.groupSortRules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.groupSortRules.Controls.Add(this.gridSortRules);
            this.groupSortRules.Location = new System.Drawing.Point(12, 606);
            this.groupSortRules.Name = "groupSortRules";
            this.groupSortRules.Size = new System.Drawing.Size(970, 170);
            this.groupSortRules.TabIndex = 4;
            this.groupSortRules.TabStop = false;
            this.groupSortRules.Text = "Правила сортировки по папкам";
            // 
            // gridSortRules
            // 
            this.gridSortRules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.gridSortRules.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gridSortRules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSortRules.Location = new System.Drawing.Point(15, 25);
            this.gridSortRules.Name = "gridSortRules";
            this.gridSortRules.RowHeadersWidth = 51;
            this.gridSortRules.RowTemplate.Height = 24;
            this.gridSortRules.Size = new System.Drawing.Size(940, 130);
            this.gridSortRules.TabIndex = 0;
            // 
            // groupPreview
            // 
            this.groupPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.groupPreview.Controls.Add(this.gridPreview);
            this.groupPreview.Location = new System.Drawing.Point(12, 782);
            this.groupPreview.Name = "groupPreview";
            this.groupPreview.Size = new System.Drawing.Size(970, 270);
            this.groupPreview.TabIndex = 5;
            this.groupPreview.TabStop = false;
            this.groupPreview.Text = "Предпросмотр";
            // 
            // FormFileNameExporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 1064);
            this.Controls.Add(this.groupPreview);
            this.Controls.Add(this.groupSortRules);
            this.Controls.Add(this.groupGrouping);
            this.Controls.Add(this.groupOrganize);
            this.Controls.Add(this.groupDuplicates);
            this.Controls.Add(this.groupMain);
            this.MinimumSize = new System.Drawing.Size(1010, 900);
            this.Name = "FormFileNameExporter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Экспорт имён файлов и сортировка";
            ((System.ComponentModel.ISupportInitialize)(this.gridPreview)).EndInit();
            this.groupMain.ResumeLayout(false);
            this.groupMain.PerformLayout();
            this.groupDuplicates.ResumeLayout(false);
            this.groupDuplicates.PerformLayout();
            this.groupOrganize.ResumeLayout(false);
            this.groupOrganize.PerformLayout();
            this.groupGrouping.ResumeLayout(false);
            this.groupGrouping.PerformLayout();
            this.groupSortRules.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSortRules)).EndInit();
            this.groupPreview.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblFolder;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.Button btnSelectFolder;

        private System.Windows.Forms.Label lblSearchPattern;
        private System.Windows.Forms.TextBox txtSearchPattern;

        private System.Windows.Forms.Label lblSeparator;
        private System.Windows.Forms.TextBox txtSeparator;

        private System.Windows.Forms.CheckBox chkIncludeSubfolders;
        private System.Windows.Forms.CheckBox chkIncludeOriginal;

        private System.Windows.Forms.Label lblTemplate;
        private System.Windows.Forms.TextBox txtTemplate;

        private System.Windows.Forms.Label lblHelp;

        private System.Windows.Forms.CheckBox chkRemoveDuplicates;
        private System.Windows.Forms.CheckBox chkIgnoreCaseDuplicates;

        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnExport;

        private System.Windows.Forms.DataGridView gridPreview;

        private System.Windows.Forms.GroupBox groupMain;
        private System.Windows.Forms.GroupBox groupDuplicates;

        private System.Windows.Forms.GroupBox groupOrganize;
        private System.Windows.Forms.CheckBox chkOrganizeFiles;
        private System.Windows.Forms.Label lblOutputRootFolder;
        private System.Windows.Forms.TextBox txtOutputRootFolder;
        private System.Windows.Forms.Button btnSelectOutputRootFolder;
        private System.Windows.Forms.CheckBox chkCopyFiles;
        private System.Windows.Forms.Button btnOrganizeFiles;

        private System.Windows.Forms.GroupBox groupGrouping;
        private System.Windows.Forms.CheckBox chkEnableGrouping;
        private System.Windows.Forms.Label lblGroupKeyTemplate;
        private System.Windows.Forms.TextBox txtGroupKeyTemplate;
        private System.Windows.Forms.Label lblGroupFolderTemplate;
        private System.Windows.Forms.TextBox txtGroupFolderTemplate;

        private System.Windows.Forms.GroupBox groupSortRules;
        private System.Windows.Forms.DataGridView gridSortRules;

        private System.Windows.Forms.GroupBox groupPreview;
    }
}