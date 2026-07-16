namespace STO_WORKBENCH.PDFFiller
{
    partial class FormSettings
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
            this.labelInputPath = new System.Windows.Forms.Label();
            this.textBoxInputPath = new System.Windows.Forms.TextBox();
            this.buttonInputPath = new System.Windows.Forms.Button();
            this.labelOutputPath = new System.Windows.Forms.Label();
            this.textBoxOutputPath = new System.Windows.Forms.TextBox();
            this.buttonOutputPath = new System.Windows.Forms.Button();
            this.groupBoxExecutor = new System.Windows.Forms.GroupBox();
            this.labelPatronymic = new System.Windows.Forms.Label();
            this.textBoxPatronymic = new System.Windows.Forms.TextBox();
            this.labelFirstName = new System.Windows.Forms.Label();
            this.textBoxFirstName = new System.Windows.Forms.TextBox();
            this.labelLastName = new System.Windows.Forms.Label();
            this.textBoxLastName = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxPaths = new System.Windows.Forms.GroupBox();
            this.groupBoxExecutor.SuspendLayout();
            this.groupBoxPaths.SuspendLayout();
            this.SuspendLayout();

            // 
            // groupBoxPaths
            // 
            this.groupBoxPaths.Controls.Add(this.labelInputPath);
            this.groupBoxPaths.Controls.Add(this.textBoxInputPath);
            this.groupBoxPaths.Controls.Add(this.buttonInputPath);
            this.groupBoxPaths.Controls.Add(this.labelOutputPath);
            this.groupBoxPaths.Controls.Add(this.textBoxOutputPath);
            this.groupBoxPaths.Controls.Add(this.buttonOutputPath);
            this.groupBoxPaths.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.groupBoxPaths.Location = new System.Drawing.Point(12, 12);
            this.groupBoxPaths.Name = "groupBoxPaths";
            this.groupBoxPaths.Size = new System.Drawing.Size(460, 130);
            this.groupBoxPaths.TabIndex = 0;
            this.groupBoxPaths.TabStop = false;
            this.groupBoxPaths.Text = "Пути к файлам";

            // 
            // labelInputPath
            // 
            this.labelInputPath.AutoSize = true;
            this.labelInputPath.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelInputPath.Location = new System.Drawing.Point(10, 28);
            this.labelInputPath.Name = "labelInputPath";
            this.labelInputPath.Size = new System.Drawing.Size(157, 15);
            this.labelInputPath.TabIndex = 0;
            this.labelInputPath.Text = "Путь к исходным файлам:";

            // 
            // textBoxInputPath
            // 
            this.textBoxInputPath.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxInputPath.Location = new System.Drawing.Point(13, 46);
            this.textBoxInputPath.Name = "textBoxInputPath";
            this.textBoxInputPath.ReadOnly = true;
            this.textBoxInputPath.Size = new System.Drawing.Size(400, 23);
            this.textBoxInputPath.TabIndex = 1;

            // 
            // buttonInputPath
            // 
            this.buttonInputPath.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonInputPath.Location = new System.Drawing.Point(419, 45);
            this.buttonInputPath.Name = "buttonInputPath";
            this.buttonInputPath.Size = new System.Drawing.Size(32, 25);
            this.buttonInputPath.TabIndex = 2;
            this.buttonInputPath.Text = "...";
            this.buttonInputPath.UseVisualStyleBackColor = true;
            this.buttonInputPath.Click += new System.EventHandler(this.buttonInputPath_Click);

            // 
            // labelOutputPath
            // 
            this.labelOutputPath.AutoSize = true;
            this.labelOutputPath.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelOutputPath.Location = new System.Drawing.Point(10, 76);
            this.labelOutputPath.Name = "labelOutputPath";
            this.labelOutputPath.Size = new System.Drawing.Size(171, 15);
            this.labelOutputPath.TabIndex = 3;
            this.labelOutputPath.Text = "Путь для сохранения файлов:";

            // 
            // textBoxOutputPath
            // 
            this.textBoxOutputPath.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxOutputPath.Location = new System.Drawing.Point(13, 94);
            this.textBoxOutputPath.Name = "textBoxOutputPath";
            this.textBoxOutputPath.ReadOnly = true;
            this.textBoxOutputPath.Size = new System.Drawing.Size(400, 23);
            this.textBoxOutputPath.TabIndex = 4;

            // 
            // buttonOutputPath
            // 
            this.buttonOutputPath.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonOutputPath.Location = new System.Drawing.Point(419, 93);
            this.buttonOutputPath.Name = "buttonOutputPath";
            this.buttonOutputPath.Size = new System.Drawing.Size(32, 25);
            this.buttonOutputPath.TabIndex = 5;
            this.buttonOutputPath.Text = "...";
            this.buttonOutputPath.UseVisualStyleBackColor = true;
            this.buttonOutputPath.Click += new System.EventHandler(this.buttonOutputPath_Click);

            // 
            // groupBoxExecutor
            // 
            this.groupBoxExecutor.Controls.Add(this.labelLastName);
            this.groupBoxExecutor.Controls.Add(this.textBoxLastName);
            this.groupBoxExecutor.Controls.Add(this.labelFirstName);
            this.groupBoxExecutor.Controls.Add(this.textBoxFirstName);
            this.groupBoxExecutor.Controls.Add(this.labelPatronymic);
            this.groupBoxExecutor.Controls.Add(this.textBoxPatronymic);
            this.groupBoxExecutor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.groupBoxExecutor.Location = new System.Drawing.Point(12, 148);
            this.groupBoxExecutor.Name = "groupBoxExecutor";
            this.groupBoxExecutor.Size = new System.Drawing.Size(460, 145);
            this.groupBoxExecutor.TabIndex = 1;
            this.groupBoxExecutor.TabStop = false;
            this.groupBoxExecutor.Text = "Данные исполнителя";

            // 
            // labelLastName
            // 
            this.labelLastName.AutoSize = true;
            this.labelLastName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelLastName.Location = new System.Drawing.Point(10, 28);
            this.labelLastName.Name = "labelLastName";
            this.labelLastName.Size = new System.Drawing.Size(59, 15);
            this.labelLastName.TabIndex = 0;
            this.labelLastName.Text = "Фамилия";

            // 
            // textBoxLastName
            // 
            this.textBoxLastName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxLastName.Location = new System.Drawing.Point(100, 25);
            this.textBoxLastName.Name = "textBoxLastName";
            this.textBoxLastName.Size = new System.Drawing.Size(350, 23);
            this.textBoxLastName.TabIndex = 1;

            // 
            // labelFirstName
            // 
            this.labelFirstName.AutoSize = true;
            this.labelFirstName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelFirstName.Location = new System.Drawing.Point(10, 65);
            this.labelFirstName.Name = "labelFirstName";
            this.labelFirstName.Size = new System.Drawing.Size(31, 15);
            this.labelFirstName.TabIndex = 2;
            this.labelFirstName.Text = "Имя";

            // 
            // textBoxFirstName
            // 
            this.textBoxFirstName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxFirstName.Location = new System.Drawing.Point(100, 62);
            this.textBoxFirstName.Name = "textBoxFirstName";
            this.textBoxFirstName.Size = new System.Drawing.Size(350, 23);
            this.textBoxFirstName.TabIndex = 3;

            // 
            // labelPatronymic
            // 
            this.labelPatronymic.AutoSize = true;
            this.labelPatronymic.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelPatronymic.Location = new System.Drawing.Point(10, 102);
            this.labelPatronymic.Name = "labelPatronymic";
            this.labelPatronymic.Size = new System.Drawing.Size(58, 15);
            this.labelPatronymic.TabIndex = 4;
            this.labelPatronymic.Text = "Отчество";

            // 
            // textBoxPatronymic
            // 
            this.textBoxPatronymic.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxPatronymic.Location = new System.Drawing.Point(100, 99);
            this.textBoxPatronymic.Name = "textBoxPatronymic";
            this.textBoxPatronymic.Size = new System.Drawing.Size(350, 23);
            this.textBoxPatronymic.TabIndex = 5;

            // 
            // buttonSave
            // 
            this.buttonSave.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.buttonSave.Location = new System.Drawing.Point(266, 305);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(100, 35);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);

            // 
            // buttonCancel
            // 
            this.buttonCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.buttonCancel.Location = new System.Drawing.Point(372, 305);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 35);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);

            // 
            // FormSettings
            // 
            this.AcceptButton = this.buttonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(484, 352);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.groupBoxExecutor);
            this.Controls.Add(this.groupBoxPaths);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.FormSettings_Load);
            this.groupBoxExecutor.ResumeLayout(false);
            this.groupBoxExecutor.PerformLayout();
            this.groupBoxPaths.ResumeLayout(false);
            this.groupBoxPaths.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxPaths;
        private System.Windows.Forms.Label labelInputPath;
        private System.Windows.Forms.TextBox textBoxInputPath;
        private System.Windows.Forms.Button buttonInputPath;
        private System.Windows.Forms.Label labelOutputPath;
        private System.Windows.Forms.TextBox textBoxOutputPath;
        private System.Windows.Forms.Button buttonOutputPath;
        private System.Windows.Forms.GroupBox groupBoxExecutor;
        private System.Windows.Forms.Label labelLastName;
        private System.Windows.Forms.TextBox textBoxLastName;
        private System.Windows.Forms.Label labelFirstName;
        private System.Windows.Forms.TextBox textBoxFirstName;
        private System.Windows.Forms.Label labelPatronymic;
        private System.Windows.Forms.TextBox textBoxPatronymic;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
    }
}