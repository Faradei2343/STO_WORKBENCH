using System;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace STO_WORKBENCH.FileNameExporter
{
    public partial class FormFileNameExporter : Form
    {
        private readonly FileNameExporter _exporter = new FileNameExporter();

        public FormFileNameExporter()
        {
            InitializeComponent();

            btnSelectFolder.Click += BtnSelectFolder_Click;
            btnPreview.Click += BtnPreview_Click;
            btnExport.Click += BtnExport_Click;
        }

        private void BtnSelectFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Выберите папку с файлами";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtFolder.Text = dialog.SelectedPath;
                }
            }
        }

        private void BtnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                FileNameExporterOptions options = CreateOptions();

                List<FileNameExportItem> items = _exporter.GetFileNames(options);

                gridPreview.DataSource = null;
                gridPreview.DataSource = items;

                if (gridPreview.Columns["FullPath"] != null)
                    gridPreview.Columns["FullPath"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                FileNameExporterOptions options = CreateOptions();

                using (SaveFileDialog dialog = new SaveFileDialog())
                {
                    dialog.Title = "Сохранить Excel-файл";
                    dialog.Filter = "Excel файл (*.xlsx)|*.xlsx";
                    dialog.FileName = "Список файлов.xlsx";

                    if (dialog.ShowDialog() != DialogResult.OK)
                        return;

                    _exporter.ExportToExcel(options, dialog.FileName);

                    MessageBox.Show(
                        "Excel-файл успешно создан.",
                        "Готово",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private FileNameExporterOptions CreateOptions()
        {
            return new FileNameExporterOptions
            {
                FolderPath = txtFolder.Text.Trim(),
                SearchPattern = txtSearchPattern.Text.Trim(),
                Template = txtTemplate.Text.Trim(),
                Separator = txtSeparator.Text,
                IncludeSubfolders = chkIncludeSubfolders.Checked,
                IncludeOriginalFileName = chkIncludeOriginal.Checked
            };
        }
    }
}
