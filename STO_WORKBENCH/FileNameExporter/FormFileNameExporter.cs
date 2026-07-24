using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace STO_WORKBENCH.FileNameExporter
{
    public partial class FormFileNameExporter : Form
    {
        private readonly FileNameExporter _exporter = new FileNameExporter();

        public enum FileOrganizeFolderStructure
        {
            /// <summary>
            /// Только папки сортировки.
            /// Пример:
            /// Root\Счет-фактуры\file.pdf
            /// </summary>
            SortOnly = 0,

            /// <summary>
            /// Только папки группировки.
            /// Пример:
            /// Root\92\file.pdf
            /// </summary>
            GroupOnly = 1,

            /// <summary>
            /// Сначала сортировка, потом группа.
            /// Пример:
            /// Root\Счет-фактуры\92\file.pdf
            /// </summary>
            SortThenGroup = 2,

            /// <summary>
            /// Сначала группа, потом сортировка.
            /// Пример:
            /// Root\92\Счет-фактуры\file.pdf
            /// </summary>
            GroupThenSort = 3
        }
        
        public FormFileNameExporter()
        {
            InitializeComponent();

            btnSelectFolder.Click += BtnSelectFolder_Click;
            btnPreview.Click += BtnPreview_Click;
            btnExport.Click += BtnExport_Click;

            btnSelectOutputRootFolder.Click += BtnSelectOutputRootFolder_Click;
            btnOrganizeFiles.Click += BtnOrganizeFiles_Click;

            InitDefaultValues();
            InitSortRulesGrid();
        }

        private void InitDefaultValues()
        {
            txtSearchPattern.Text = "*.*";
            txtTemplate.Text = "{name}";
            txtSeparator.Text = "_";

            chkIncludeOriginal.Checked = true;

            chkRemoveDuplicates.Checked = false;
            chkIgnoreCaseDuplicates.Checked = true;

            chkOrganizeFiles.Checked = false;
            chkCopyFiles.Checked = true;

            chkEnableGrouping.Checked = false;
            txtGroupKeyTemplate.Text = "{match:№\\s*(\\d+):1}";
            txtGroupFolderTemplate.Text = "{group}";
        }

        private void InitSortRulesGrid()
        {
            gridSortRules.AllowUserToAddRows = true;
            gridSortRules.AllowUserToDeleteRows = true;
            gridSortRules.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            gridSortRules.Columns.Clear();

            DataGridViewCheckBoxColumn colEnabled = new DataGridViewCheckBoxColumn
            {
                Name = "colRuleEnabled",
                HeaderText = "Включено"
            };

            DataGridViewTextBoxColumn colFolder = new DataGridViewTextBoxColumn
            {
                Name = "colRuleFolder",
                HeaderText = "Папка"
            };

            DataGridViewTextBoxColumn colPatterns = new DataGridViewTextBoxColumn
            {
                Name = "colRulePatterns",
                HeaderText = "Regex-шаблоны через ;"
            };

            DataGridViewCheckBoxColumn colIgnoreCase = new DataGridViewCheckBoxColumn
            {
                Name = "colRuleIgnoreCase",
                HeaderText = "Без учёта регистра"
            };

            gridSortRules.Columns.Add(colEnabled);
            gridSortRules.Columns.Add(colFolder);
            gridSortRules.Columns.Add(colPatterns);
            gridSortRules.Columns.Add(colIgnoreCase);

            gridSortRules.Rows.Add(
                true,
                "Новые счет-фактуры",
                @"\bсф\b;сч\.?\s*ф\.?;счф",
                true
            );

            gridSortRules.Rows.Add(
                true,
                "Товарные накладные",
                @"\bтн\b;тов\.?\s*накл\.?",
                true
            );
        }

        private void BtnSelectFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Выберите папку с файлами";

                if (dialog.ShowDialog() == DialogResult.OK)
                    txtFolder.Text = dialog.SelectedPath;
            }
        }

        private void BtnSelectOutputRootFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Выберите папку, куда сортировать файлы";

                if (dialog.ShowDialog() == DialogResult.OK)
                    txtOutputRootFolder.Text = dialog.SelectedPath;
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

        private void BtnOrganizeFiles_Click(object sender, EventArgs e)
        {
            try
            {
                FileNameExporterOptions options = CreateOptions();
                options.OrganizeFiles = true;

                DialogResult confirm = MessageBox.Show(
                    options.CopyFiles
                        ? "Файлы будут скопированы по папкам. Продолжить?"
                        : "Файлы будут перемещены по папкам. Продолжить?",
                    "Подтверждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirm != DialogResult.Yes)
                    return;

                _exporter.OrganizeFiles(options);

                MessageBox.Show(
                    "Файлы успешно обработаны.",
                    "Готово",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
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
                IncludeOriginalFileName = chkIncludeOriginal.Checked,

                RemoveDuplicates = chkRemoveDuplicates.Checked,
                IgnoreCaseDuplicates = chkIgnoreCaseDuplicates.Checked,

                OrganizeFiles = chkOrganizeFiles.Checked,
                CopyFiles = chkCopyFiles.Checked,
                OutputRootFolder = txtOutputRootFolder.Text.Trim(),

                SortRules = ReadSortRulesFromGrid(),

                EnableGrouping = chkEnableGrouping.Checked,
                GroupKeyTemplate = txtGroupKeyTemplate.Text.Trim(),
                GroupFolderTemplate = txtGroupFolderTemplate.Text.Trim()
            };
        }

        private List<FileSortRule> ReadSortRulesFromGrid()
        {
            List<FileSortRule> rules = new List<FileSortRule>();

            foreach (DataGridViewRow row in gridSortRules.Rows)
            {
                if (row.IsNewRow)
                    continue;

                bool enabled = ToBool(row.Cells["colRuleEnabled"].Value, true);
                string folder = Convert.ToString(row.Cells["colRuleFolder"].Value);
                string patternsText = Convert.ToString(row.Cells["colRulePatterns"].Value);
                bool ignoreCase = ToBool(row.Cells["colRuleIgnoreCase"].Value, true);

                if (string.IsNullOrWhiteSpace(folder))
                    continue;

                if (string.IsNullOrWhiteSpace(patternsText))
                    continue;

                List<string> patterns = patternsText
                    .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .ToList();

                if (patterns.Count == 0)
                    continue;

                rules.Add(new FileSortRule
                {
                    Enabled = enabled,
                    TargetFolderName = folder,
                    RegexPatterns = patterns,
                    IgnoreCase = ignoreCase
                });
            }

            return rules;
        }

        private static bool ToBool(object value, bool defaultValue)
        {
            if (value == null)
                return defaultValue;

            if (value is bool b)
                return b;

            if (bool.TryParse(value.ToString(), out bool result))
                return result;

            return defaultValue;
        }
    }
}
