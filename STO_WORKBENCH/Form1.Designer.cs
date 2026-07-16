namespace STO_WORKBENCH
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelLeft = new Panel();
            buttonAutoSL = new Button();
            buttonSL = new Button();
            buttonSort = new Button();
            panelLeft.SuspendLayout();
            SuspendLayout();
            // 
            // panelLeft
            // 
            panelLeft.BorderStyle = BorderStyle.FixedSingle;
            panelLeft.Controls.Add(buttonAutoSL);
            panelLeft.Controls.Add(buttonSL);
            panelLeft.Controls.Add(buttonSort);
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Location = new Point(0, 0);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(175, 450);
            panelLeft.TabIndex = 0;
            // 
            // buttonAutoSL
            // 
            buttonAutoSL.Dock = DockStyle.Top;
            buttonAutoSL.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            buttonAutoSL.Location = new Point(0, 118);
            buttonAutoSL.Name = "buttonAutoSL";
            buttonAutoSL.Size = new Size(173, 59);
            buttonAutoSL.TabIndex = 3;
            buttonAutoSL.Text = "Авто подпись СЛ";
            buttonAutoSL.UseVisualStyleBackColor = true;
            buttonAutoSL.Click += buttonAutoSL_Click;
            // 
            // buttonSL
            // 
            buttonSL.Dock = DockStyle.Top;
            buttonSL.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            buttonSL.Location = new Point(0, 59);
            buttonSL.Name = "buttonSL";
            buttonSL.Size = new Size(173, 59);
            buttonSL.TabIndex = 2;
            buttonSL.Text = "Сервисные листы";
            buttonSL.UseVisualStyleBackColor = true;
            buttonSL.Click += buttonSL_Click;
            // 
            // buttonSort
            // 
            buttonSort.Dock = DockStyle.Top;
            buttonSort.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            buttonSort.Location = new Point(0, 0);
            buttonSort.Name = "buttonSort";
            buttonSort.Size = new Size(173, 59);
            buttonSort.TabIndex = 1;
            buttonSort.Text = "Сортировка детализаций";
            buttonSort.UseVisualStyleBackColor = true;
            buttonSort.Click += buttonSort_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panelLeft);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            panelLeft.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelLeft;
        private Button buttonSort;
        private Button buttonAutoSL;
        private Button buttonSL;
    }
}
