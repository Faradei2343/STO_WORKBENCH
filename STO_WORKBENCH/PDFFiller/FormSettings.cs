using System;
using System.Windows.Forms;
using STO_WORKBENCH.Core;
using STO_WORKBENCH.PDFFiller.Services;


namespace STO_WORKBENCH.PDFFiller
{
        public partial class FormSettings : Form
        {
            private readonly IMessageService _messageService;
            private readonly ISettingsService _settingsService;

            public FormSettings(IMessageService messageService, ISettingsService settingsService)
            {
                InitializeComponent();
                _messageService = messageService;
                _settingsService = settingsService;
            }

            private void FormSettings_Load(object sender, EventArgs e)
            {
                LoadExistingSettings();
            }

            private void LoadExistingSettings()
            {
                if (_settingsService.SettingsFileExists())
                {
                    var settings = _settingsService.LoadSettings();
                    if (settings != null)
                    {
                        textBoxInputPath.Text = settings.InputPath;
                        textBoxOutputPath.Text = settings.OutputPath;
                        textBoxLastName.Text = settings.LastName;
                        textBoxFirstName.Text = settings.FirstName;
                        textBoxPatronymic.Text = settings.Patronymic;
                    }
                }
            }

            private void buttonInputPath_Click(object sender, EventArgs e)
            {
                using var fbd = new FolderBrowserDialog
                {
                    Description = "Выберите папку с исходными файлами"
                };

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    textBoxInputPath.Text = fbd.SelectedPath;
                }
            }

            private void buttonOutputPath_Click(object sender, EventArgs e)
            {
                using var fbd = new FolderBrowserDialog
                {
                    Description = "Выберите папку для сохранения результатов"
                };

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    textBoxOutputPath.Text = fbd.SelectedPath;
                }
            }

            private void buttonSave_Click(object sender, EventArgs e)
            {
                // Валидация
                if (string.IsNullOrWhiteSpace(textBoxInputPath.Text))
                {
                    _messageService.ShowWarning("Укажите путь к исходным файлам!");
                    textBoxInputPath.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBoxOutputPath.Text))
                {
                    _messageService.ShowWarning("Укажите путь для сохранения!");
                    textBoxOutputPath.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBoxLastName.Text))
                {
                    _messageService.ShowWarning("Укажите фамилию!");
                    textBoxLastName.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBoxFirstName.Text))
                {
                    _messageService.ShowWarning("Укажите имя!");
                    textBoxFirstName.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBoxPatronymic.Text))
                {
                    _messageService.ShowWarning("Укажите отчество!");
                    textBoxPatronymic.Focus();
                    return;
                }

                var settings = new AppSettings
                {
                    InputPath = textBoxInputPath.Text.Trim(),
                    OutputPath = textBoxOutputPath.Text.Trim(),
                    LastName = textBoxLastName.Text.Trim(),
                    FirstName = textBoxFirstName.Text.Trim(),
                    Patronymic = textBoxPatronymic.Text.Trim()
                };

                _settingsService.SaveSettings(settings);
                _messageService.ShowSuccess("Настройки сохранены!");

                DialogResult = DialogResult.OK;
                Close();
            }

            private void buttonCancel_Click(object sender, EventArgs e)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }
    }
