using STO_WORKBENCH.Core;
using System.Reflection;
using System.Text.Json;

namespace STO_WORKBENCH.PDFFiller.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly string _settingsDirectory;
        private readonly string _settingsFilePath;
        private readonly IMessageService _messageService;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true
        };

        public SettingsService(IMessageService messageService)
        {
            _messageService = messageService;
            _settingsDirectory = Path.Combine(Environment.CurrentDirectory, "Settings");
            _settingsFilePath = Path.Combine(_settingsDirectory, "settings.json");
        }

        public string GetSettingsDirectory() => _settingsDirectory;

        public bool SettingsFileExists()
        {
            try
            {
                EnsureDirectoryExists();
                return File.Exists(_settingsFilePath);
            }
            catch (Exception ex)
            {
                _messageService.ShowError($"Ошибка проверки файла настроек: {ex.Message}");
                return false;
            }
        }

        public AppSettings? LoadSettings()
        {
            try
            {
                if (!File.Exists(_settingsFilePath))
                    return null;

                var json = File.ReadAllText(_settingsFilePath);
                return JsonSerializer.Deserialize<AppSettings>(json, JsonOptions);
            }
            catch (Exception ex)
            {
                _messageService.ShowError($"Ошибка загрузки настроек: {ex.Message}");
                return null;
            }
        }

        public void SaveSettings(AppSettings settings)
        {
            try
            {
                EnsureDirectoryExists();
                settings.Version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "1.0.0";
                var json = JsonSerializer.Serialize(settings, JsonOptions);
                File.WriteAllText(_settingsFilePath, json);
            }
            catch (Exception ex)
            {
                _messageService.ShowError($"Ошибка сохранения настроек: {ex.Message}");
            }
        }

        private void EnsureDirectoryExists()
        {
            if (!Directory.Exists(_settingsDirectory))
            {
                Directory.CreateDirectory(_settingsDirectory);
            }
        }
    }
}
