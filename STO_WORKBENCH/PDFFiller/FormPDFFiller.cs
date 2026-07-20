using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using STO_WORKBENCH.Core;
using STO_WORKBENCH.PDFFiller.Services;

namespace STO_WORKBENCH.PDFFiller
{
    public partial class FormPDFFiller : Form
    {
        private readonly IMessageService _messageService;
        private readonly ISettingsService _settingsService;
        private readonly IPdfService _pdfService;
        private AppSettings? _settings;

        public FormPDFFiller()
        {
            InitializeComponent();

            _messageService = new MessageService();
            _settingsService = new SettingsService(_messageService);
            _pdfService = new PdfService(_messageService);
        }

        private void FormPDFFiller_Load(object sender, EventArgs e)
        {
            InitializeApplication();
        }

        private void InitializeApplication()
        {
            if (_settingsService.SettingsFileExists())
            {
                _settings = _settingsService.LoadSettings();
                if (_settings != null)
                {
                    RefreshFileList();
                    timerRefresh.Start();
                    UpdateStatusBar();
                }
            }
            else
            {
                _messageService.ShowWarning("Первый запуск. Откроется окно настроек.");
                ShowSettingsForm();
            }
        }

        private void RefreshFileList()
        {
            if (_settings == null || string.IsNullOrEmpty(_settings.InputPath))
                return;

            dataGridViewFiles.Rows.Clear();

            if (!Directory.Exists(_settings.InputPath))
            {
                _messageService.ShowWarning($"Папка не найдена: {_settings.InputPath}");
                return;
            }

            var directory = new DirectoryInfo(_settings.InputPath);
            foreach (var file in directory.GetFiles("*.pdf"))
            {
                dataGridViewFiles.Rows.Add(file.Name, true);
            }

            UpdateStatusBar();
        }

        private void UpdateStatusBar()
        {
            var totalCount = dataGridViewFiles.RowCount;
            var selectedCount = GetSelectedFilesCount();
            statusLabelCount.Text = $"Файлов: {totalCount} | Выбрано: {selectedCount}";
        }

        private void MoveProcessedFile(string fileName)
        {
            if (_settings == null) return;

            try
            {
                var archiveDir = Path.Combine(
                    _settings.InputPath,
                    $"Заполненные {DateTime.Now:dd-MM-yyyy}");

                if (!Directory.Exists(archiveDir))
                {
                    Directory.CreateDirectory(archiveDir);
                }

                var sourcePath = Path.Combine(_settings.InputPath, fileName);
                var destPath = Path.Combine(archiveDir, fileName);

                File.Move(sourcePath, destPath);
            }
            catch (Exception ex)
            {
                _messageService.ShowError($"Ошибка перемещения файла: {ex.Message}");
            }
        }

        private PdfProcessingOptions BuildProcessingOptions()
        {
            var options = new PdfProcessingOptions
            {
                UseCustomDate = checkBoxCustomDate.Checked,
                FillOnlyMonthYear = checkBoxMonthYearOnly.Checked,
                UseCustomApplicant = checkBoxCustomApplicant.Checked
            };

            if (options.UseCustomDate)
            {
                var date = dateTimePickerDate.Value.Date;
                if (TimeSpan.TryParse(maskedTextBoxTime.Text, out var time))
                {
                    options.CustomDateTime = date.Add(time);
                }
                else
                {
                    options.CustomDateTime = date;
                }
            }

            if (options.UseCustomApplicant)
            {
                options.CustomApplicant = new ApplicantInfo
                {
                    LastName = textBoxApplicantLastName.Text.Trim(),
                    FirstName = textBoxApplicantFirstName.Text.Trim(),
                    Patronymic = textBoxApplicantPatronymic.Text.Trim()
                };
            }

            return options;
        }

        private async void buttonProcess_Click(object sender, EventArgs e)
        {
            if (_settings == null || !_settings.IsValid())
            {
                _messageService.ShowWarning("Настройки не заданы!");
                return;
            }

            if (dataGridViewFiles.RowCount == 0)
            {
                _messageService.ShowWarning("Список файлов пуст!");
                return;
            }

            var options = BuildProcessingOptions();
            var processedCount = 0;
            var errorCount = 0;

            // Блокируем UI на время обработки
            SetProcessingState(true);

            try
            {
                if (radioButtonProcessAll.Checked)
                {
                    var selectedCount = GetSelectedFilesCount();
                    progressBar.Maximum = selectedCount > 0 ? selectedCount : 1;

                    for (int i = 0; i < dataGridViewFiles.RowCount; i++)
                    {
                        var isSelected = Convert.ToBoolean(dataGridViewFiles.Rows[i].Cells[columnSelected.Index].Value);
                        if (!isSelected) continue;

                        var fileName = dataGridViewFiles.Rows[i].Cells[columnFileName.Index].Value?.ToString();
                        if (string.IsNullOrEmpty(fileName)) continue;

                        statusLabel.Text = $"Обработка: {fileName}";

                        if (await _pdfService.ProcessPdfAsync(fileName, _settings, options))
                        {
                            MoveProcessedFile(fileName);
                            processedCount++;
                        }
                        else
                        {
                            errorCount++;
                        }

                        progressBar.Value = Math.Min(progressBar.Value + 1, progressBar.Maximum);
                    }
                }
                else if (dataGridViewFiles.CurrentRow != null)
                {
                    progressBar.Maximum = 1;
                    var fileName = dataGridViewFiles.CurrentRow.Cells[columnFileName.Index].Value?.ToString();

                    if (!string.IsNullOrEmpty(fileName))
                    {
                        statusLabel.Text = $"Обработка: {fileName}";

                        if (await _pdfService.ProcessPdfAsync(fileName, _settings, options))
                        {
                            MoveProcessedFile(fileName);
                            processedCount++;
                        }
                        else
                        {
                            errorCount++;
                        }
                    }
                    progressBar.Value = 1;
                }

                RefreshFileList();

                if (errorCount == 0 && processedCount > 0)
                {
                    _messageService.ShowSuccess($"Обработано файлов: {processedCount}");
                    Process.Start("explorer.exe", _settings.OutputPath);
                }
                else if (errorCount > 0)
                {
                    _messageService.ShowWarning($"Обработано: {processedCount}, с ошибками: {errorCount}");
                }
                else
                {
                    _messageService.ShowWarning("Не выбрано ни одного файла для обработки.");
                }
            }
            catch (Exception ex)
            {
                _messageService.ShowError($"Ошибка обработки: {ex.Message}");
            }
            finally
            {
                SetProcessingState(false);
            }
        }

        private void SetProcessingState(bool isProcessing)
        {
            buttonProcess.Enabled = !isProcessing;
            groupBoxMode.Enabled = !isProcessing;
            dataGridViewFiles.Enabled = !isProcessing;
            menuStrip.Enabled = !isProcessing;
            progressBar.Visible = isProcessing;
            progressBar.Value = 0;

            if (!isProcessing)
            {
                statusLabel.Text = "Готов";
            }
        }

        private int GetSelectedFilesCount()
        {
            int count = 0;
            for (int i = 0; i < dataGridViewFiles.RowCount; i++)
            {
                if (Convert.ToBoolean(dataGridViewFiles.Rows[i].Cells[columnSelected.Index].Value))
                    count++;
            }
            return count;
        }

        private void ShowSettingsForm()
        {
            using var settingsForm = new FormSettings(_messageService, _settingsService);
            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                _settings = _settingsService.LoadSettings();
                RefreshFileList();
            }
        }

        #region Event Handlers

        private void menuItemSettingsOpen_Click(object sender, EventArgs e)
        {
            ShowSettingsForm();
        }

        private void menuItemRefresh_Click(object sender, EventArgs e)
        {
            RefreshFileList();
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            RefreshFileList();
        }

        private void dataGridViewFiles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Обработка клика по чекбоксу
            if (e.RowIndex >= 0 && e.ColumnIndex == columnSelected.Index)
            {
                // Завершаем редактирование, чтобы получить актуальное значение
                dataGridViewFiles.EndEdit();
                UpdateStatusBar();
            }
        }

        private void dataGridViewFiles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Двойной клик по строке - переключаем выбор
            if (e.RowIndex >= 0)
            {
                var currentValue = Convert.ToBoolean(dataGridViewFiles.Rows[e.RowIndex].Cells[columnSelected.Index].Value);
                dataGridViewFiles.Rows[e.RowIndex].Cells[columnSelected.Index].Value = !currentValue;
                UpdateStatusBar();
            }
        }

        private void checkBoxCustomDate_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCustomDate.Checked && checkBoxMonthYearOnly.Checked)
            {
                checkBoxMonthYearOnly.Checked = false;
            }
            panelDateTime.Visible = checkBoxCustomDate.Checked;
        }

        private void checkBoxMonthYearOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMonthYearOnly.Checked && checkBoxCustomDate.Checked)
            {
                checkBoxCustomDate.Checked = false;
            }
        }

        private void checkBoxCustomApplicant_CheckedChanged(object sender, EventArgs e)
        {
            panelApplicant.Visible = checkBoxCustomApplicant.Checked;
        }

        #endregion

        #region Context Menu & Additional Features

        /// <summary>
        /// Выбрать все файлы
        /// </summary>
        public void SelectAllFiles()
        {
            for (int i = 0; i < dataGridViewFiles.RowCount; i++)
            {
                dataGridViewFiles.Rows[i].Cells[columnSelected.Index].Value = true;
            }
            UpdateStatusBar();
        }

        /// <summary>
        /// Снять выделение со всех файлов
        /// </summary>
        public void DeselectAllFiles()
        {
            for (int i = 0; i < dataGridViewFiles.RowCount; i++)
            {
                dataGridViewFiles.Rows[i].Cells[columnSelected.Index].Value = false;
            }
            UpdateStatusBar();
        }

        /// <summary>
        /// Инвертировать выделение
        /// </summary>
        public void InvertSelection()
        {
            for (int i = 0; i < dataGridViewFiles.RowCount; i++)
            {
                var currentValue = Convert.ToBoolean(dataGridViewFiles.Rows[i].Cells[columnSelected.Index].Value);
                dataGridViewFiles.Rows[i].Cells[columnSelected.Index].Value = !currentValue;
            }
            UpdateStatusBar();
        }

        #endregion
    }
}
