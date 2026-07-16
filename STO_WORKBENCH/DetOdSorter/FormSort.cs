using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace STO_WORKBENCH
{
    public partial class FormSort : Form
    {
        private string selectedPath;
        public FormSort()
        {
            InitializeComponent();
            textBox1.ReadOnly = true;
            buttonSort.Enabled = false;
        }

        private void buttonPath_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = "Выберите папку источник";
            folderBrowserDialog1.ShowNewFolderButton = true;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                selectedPath = folderBrowserDialog1.SelectedPath;
                textBox1.Text = selectedPath;
                richTextBox1.Clear();
                buttonSort.Enabled = true;
                LogMessage($"Выбрана папка: {selectedPath}");
            }
        }

        private void LogMessage(string message)
        {
            // Безопасный вызов из любого потока (если потребуется)
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(new Action(() => LogMessage(message)));
                return;
            }
            richTextBox1.AppendText(message + Environment.NewLine);
            richTextBox1.ScrollToCaret();
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedPath))
            {
                MessageBox.Show("Сначала выберите папку!", "Предупреждение");
                return;
            }

            // Создаём экземпляр сортировщика и передаём метод логирования
            Sorter sorter= new Sorter(LogMessage);

            // Запускаем
            bool success = sorter.Sort(selectedPath);

            if (success)
                MessageBox.Show("Сортировка успешно завершена!", "Готово");
            else
                MessageBox.Show("Сортировка завершена с ошибками. Смотрите лог.", "Внимание");
        }
    }
}
