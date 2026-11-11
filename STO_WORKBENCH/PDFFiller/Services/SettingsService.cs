using STO_WORKBENCH.Core;
using System.Reflection;
using System.Text.Json;

namespace STO_WORKBENCH.PDFFiller.Services
{
    // Сервис хранения настроек приложения в JSON-файле.
    // Файл settings.json лежит в подкаталоге Settings рабочего каталога программы
    // (Environment.CurrentDirectory). Все публичные операции обёрнуты в try/catch:
    // при ошибках пользователь получает сообщение через IMessageService, а метод
    // возвращает безопасное значение по умолчанию (false / null).
    public class SettingsService : ISettingsService
    {
        // Каталог, в котором хранится файл настроек (создаётся автоматически при необходимости).
        private readonly string _settingsDirectory;

        // Полный путь к файлу настроек: <рабочий каталог>\Settings\settings.json.
        private readonly string _settingsFilePath;

        // Сервис сообщений для показа пользователю диалогов об ошибках.
        private readonly IMessageService _messageService;

        // Параметры сериализации JSON: с отступами — чтобы файл было удобно читать
        // и при необходимости править вручную.
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true
        };

        // Конструктор: запоминает сервис сообщений и вычисляет пути к каталогу и файлу настроек.
        // messageService — сервис для показа диалогов об ошибках.
        public SettingsService(IMessageService messageService)
        {
            _messageService = messageService;
            _settingsDirectory = Path.Combine(Environment.CurrentDirectory, "Settings");
            _settingsFilePath = Path.Combine(_settingsDirectory, "settings.json");
        }

        // Возвращает путь к каталогу настроек (используется внешним кодом).
        public string GetSettingsDirectory() => _settingsDirectory;

        // Проверяет наличие файла настроек на диске.
        // Перед проверкой гарантирует существование каталога (создаёт его при отсутствии).
        // Возвращает true, если файл существует; при исключении показывает ошибку и возвращает false —
        // для вызывающего кода это выглядит как "настроек нет", и будет показано окно первоначальной настройки.
        public bool SettingsFileExists()
        {
            try
            {
                // Гарантируем существование каталога, чтобы проверка не упала на отсутствующем пути.
                EnsureDirectoryExists();
                return File.Exists(_settingsFilePath);
            }
            catch (Exception ex)
            {
                _messageService.ShowError($"Ошибка проверки файла настроек: {ex.Message}");
                return false;
            }
        }

        // Загружает настройки из JSON-файла.
        // Возвращает десериализованный AppSettings или null, если файла нет, он пуст или повреждён
        // (в последнем случае пользователю уже показано сообщение об ошибке).
        public AppSettings? LoadSettings()
        {
            try
            {
                // Отсутствие файла — штатная ситуация первого запуска, ошибкой не считается.
                if (!File.Exists(_settingsFilePath))
                    return null;

                // Читаем весь файл и десериализуем его в модель настроек.
                var json = File.ReadAllText(_settingsFilePath);
                return JsonSerializer.Deserialize<AppSettings>(json, JsonOptions);
            }
            catch (Exception ex)
            {
                _messageService.ShowError($"Ошибка загрузки настроек: {ex.Message}");
                return null;
            }
        }

        // Сохраняет настройки в JSON-файл.
        // settings — объект настроек; перед записью в него проставляется версия приложения
        // (чтобы можно было определить, какой версией программы он был записан).
        // При ошибке записи пользователь получает сообщение через IMessageService.
        public void SaveSettings(AppSettings settings)
        {
            try
            {
                EnsureDirectoryExists();

                // Фиксируем версию сборки в файле настроек для диагностики совместимости.
                settings.Version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "1.0.0";
                var json = JsonSerializer.Serialize(settings, JsonOptions);
                File.WriteAllText(_settingsFilePath, json);
            }
            catch (Exception ex)
            {
                _messageService.ShowError($"Ошибка сохранения настроек: {ex.Message}");
            }
        }

        // Служебный метод: создаёт каталог настроек, если он ещё не существует.
        private void EnsureDirectoryExists()
        {
            if (!Directory.Exists(_settingsDirectory))
            {
                Directory.CreateDirectory(_settingsDirectory);
            }
        }
    }
}
