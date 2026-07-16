namespace STO_WORKBENCH.SLRegistry
{
    partial class FormSLRegistry
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
            folderBrowserDialogSLRegistry = new FolderBrowserDialog();
            panelLeft = new Panel();
            buttonBuildAll = new Button();
            panel1 = new Panel();
            buttonPath = new Button();
            textBoxPath = new TextBox();
            labelPath = new Label();
            textBoxProject = new TextBox();
            labelProject = new Label();
            textBoxPeriod = new TextBox();
            labelPeriod = new Label();
            richTextBoxLog = new RichTextBox();
            panelLeft.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panelLeft
            // 
            panelLeft.BorderStyle = BorderStyle.FixedSingle;
            panelLeft.Controls.Add(buttonBuildAll);
            panelLeft.Controls.Add(panel1);
            panelLeft.Controls.Add(labelPath);
            panelLeft.Controls.Add(textBoxProject);
            panelLeft.Controls.Add(labelProject);
            panelLeft.Controls.Add(textBoxPeriod);
            panelLeft.Controls.Add(labelPeriod);
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Location = new Point(0, 0);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(303, 450);
            panelLeft.TabIndex = 4;
            // 
            // buttonBuildAll
            // 
            buttonBuildAll.Dock = DockStyle.Top;
            buttonBuildAll.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            buttonBuildAll.Location = new Point(0, 153);
            buttonBuildAll.Name = "buttonBuildAll";
            buttonBuildAll.Size = new Size(301, 38);
            buttonBuildAll.TabIndex = 11;
            buttonBuildAll.Text = "Создать реестры";
            buttonBuildAll.UseVisualStyleBackColor = true;
            buttonBuildAll.Click += buttonBuildAll_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(buttonPath);
            panel1.Controls.Add(textBoxPath);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 121);
            panel1.Name = "panel1";
            panel1.Size = new Size(301, 32);
            panel1.TabIndex = 9;
            // 
            // buttonPath
            // 
            buttonPath.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            buttonPath.Location = new Point(273, 0);
            buttonPath.Name = "buttonPath";
            buttonPath.Size = new Size(28, 29);
            buttonPath.TabIndex = 5;
            buttonPath.Text = "...";
            buttonPath.UseVisualStyleBackColor = true;
            buttonPath.Click += buttonPath_Click;
            // 
            // textBoxPath
            // 
            textBoxPath.Font = new Font("Segoe UI", 12F);
            textBoxPath.Location = new Point(-1, 0);
            textBoxPath.Name = "textBoxPath";
            textBoxPath.Size = new Size(278, 29);
            textBoxPath.TabIndex = 10;
            // 
            // labelPath
            // 
            labelPath.AutoSize = true;
            labelPath.Dock = DockStyle.Top;
            labelPath.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelPath.Location = new Point(0, 100);
            labelPath.Name = "labelPath";
            labelPath.Size = new Size(140, 21);
            labelPath.TabIndex = 8;
            labelPath.Text = "Название проекта";
            // 
            // textBoxProject
            // 
            textBoxProject.Dock = DockStyle.Top;
            textBoxProject.Font = new Font("Segoe UI", 12F);
            textBoxProject.Location = new Point(0, 71);
            textBoxProject.Name = "textBoxProject";
            textBoxProject.Size = new Size(301, 29);
            textBoxProject.TabIndex = 7;
            // 
            // labelProject
            // 
            labelProject.AutoSize = true;
            labelProject.Dock = DockStyle.Top;
            labelProject.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelProject.Location = new Point(0, 50);
            labelProject.Name = "labelProject";
            labelProject.Size = new Size(140, 21);
            labelProject.TabIndex = 6;
            labelProject.Text = "Название проекта";
            // 
            // textBoxPeriod
            // 
            textBoxPeriod.Dock = DockStyle.Top;
            textBoxPeriod.Font = new Font("Segoe UI", 12F);
            textBoxPeriod.Location = new Point(0, 21);
            textBoxPeriod.Name = "textBoxPeriod";
            textBoxPeriod.Size = new Size(301, 29);
            textBoxPeriod.TabIndex = 5;
            // 
            // labelPeriod
            // 
            labelPeriod.AutoSize = true;
            labelPeriod.Dock = DockStyle.Top;
            labelPeriod.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelPeriod.Location = new Point(0, 0);
            labelPeriod.Name = "labelPeriod";
            labelPeriod.Size = new Size(139, 21);
            labelPeriod.TabIndex = 5;
            labelPeriod.Text = "Отчетный период";
            // 
            // richTextBoxLog
            // 
            richTextBoxLog.Dock = DockStyle.Fill;
            richTextBoxLog.Location = new Point(303, 0);
            richTextBoxLog.Name = "richTextBoxLog";
            richTextBoxLog.Size = new Size(497, 450);
            richTextBoxLog.TabIndex = 5;
            richTextBoxLog.Text = "";
            // 
            // FormSLRegistry
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(richTextBoxLog);
            Controls.Add(panelLeft);
            Name = "FormSLRegistry";
            Text = "FormSLRegistry";
            panelLeft.ResumeLayout(false);
            panelLeft.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private FolderBrowserDialog folderBrowserDialogSLRegistry;
        private Panel panelLeft;
        private TextBox textBoxProject;
        private Label labelProject;
        private TextBox textBoxPeriod;
        private Label labelPeriod;
        private Panel panel1;
        private Button buttonPath;
        private TextBox textBoxPath;
        private Label labelPath;
        private RichTextBox richTextBoxLog;
        private Button buttonBuildAll;
    }
}