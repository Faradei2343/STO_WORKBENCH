namespace STO_WORKBENCH
{
    partial class FormSort
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
            textBox1 = new TextBox();
            buttonPath = new Button();
            label1 = new Label();
            richTextBox1 = new RichTextBox();
            folderBrowserDialog1 = new FolderBrowserDialog();
            buttonSort = new Button();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 27);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(658, 23);
            textBox1.TabIndex = 0;
            // 
            // buttonPath
            // 
            buttonPath.Location = new Point(674, 27);
            buttonPath.Name = "buttonPath";
            buttonPath.Size = new Size(114, 23);
            buttonPath.TabIndex = 1;
            buttonPath.Text = "Путь к папке";
            buttonPath.UseVisualStyleBackColor = true;
            buttonPath.Click += buttonPath_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(234, 15);
            label1.TabIndex = 2;
            label1.Text = "Выберите путь к файлам для сортировки";
            // 
            // richTextBox1
            // 
            richTextBox1.Dock = DockStyle.Bottom;
            richTextBox1.Location = new Point(0, 111);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(800, 339);
            richTextBox1.TabIndex = 3;
            richTextBox1.Text = "";
            // 
            // buttonSort
            // 
            buttonSort.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            buttonSort.Location = new Point(674, 56);
            buttonSort.Name = "buttonSort";
            buttonSort.Size = new Size(116, 50);
            buttonSort.TabIndex = 4;
            buttonSort.Text = "Сортировать";
            buttonSort.UseVisualStyleBackColor = true;
            buttonSort.Click += buttonSort_Click;
            // 
            // FormSort
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonSort);
            Controls.Add(richTextBox1);
            Controls.Add(label1);
            Controls.Add(buttonPath);
            Controls.Add(textBox1);
            Name = "FormSort";
            Text = "FormSort";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Button buttonPath;
        private Label label1;
        private RichTextBox richTextBox1;
        private FolderBrowserDialog folderBrowserDialog1;
        private Button buttonSort;
    }
}