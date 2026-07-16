using System;
using System.IO;
using System.Windows.Forms;
using STO_WORKBENCH;

namespace STO_WORKBENCH.SLRegistry
{
    public partial class FormSLRegistry : Form
    {
        private string selectedFolder;
        public FormSLRegistry()
        {
            InitializeComponent();
            buttonBuildAll.Enabled = false;
        }

        private void buttonPath_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Выберите папку с det_*.xls файлами";
                if(dialog.ShowDialog()==DialogResult.OK)
                {
                   selectedFolder = dialog.SelectedPath;
                    textBoxPath.Text = selectedFolder;
                    buttonBuildAll.Enabled = true;
                    richTextBoxLog.Clear();
                    LogMessage($"Выбрана папка: {selectedFolder}");
                }
            }
               
        }

        private void buttonBuildAll_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedFolder) || !Directory.Exists(selectedFolder))
            {
                MessageBox.Show("Папка не выбрана или не существует.", "Ошибка");
                return;
            }

            string period = textBoxPeriod.Text.Trim();
            string project = textBoxProject.Text.Trim();

            if (string.IsNullOrEmpty(period))
            {
                MessageBox.Show("Введите отчётный период.", "Предупреждение");
                textBoxPeriod.Focus();
                return;
            }

            if (string.IsNullOrEmpty(project))
            {
                MessageBox.Show("Введите название проекта.", "Предупреждение");
                textBoxProject.Focus();
                return;
            }

            richTextBoxLog.Clear();
            var builder = new RegistryBuilder(LogMessage);
            var created = builder.BuildAll(selectedFolder, period, project, null);

            if (created != null && created.Count > 0)
            {
                MessageBox.Show($"Создано реестров: {created.Count}", "Готово");
            }
            else
            {
                MessageBox.Show("Не удалось создать ни одного реестра.", "Ошибка");
            }
        }

        private void LogMessage(string message)
        {
            if (richTextBoxLog.InvokeRequired)
            {
                richTextBoxLog.Invoke(new Action(() => LogMessage(message)));
                return;
            }
            richTextBoxLog.AppendText(message + Environment.NewLine);
            richTextBoxLog.ScrollToCaret();
        }
    }
}
