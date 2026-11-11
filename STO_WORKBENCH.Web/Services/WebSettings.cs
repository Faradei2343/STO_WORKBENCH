using System.Text.Json;

namespace STO_WORKBENCH.Web.Services;

/// <summary>
/// Общие настройки веб-приложения. Заменяет Properties.Settings WinForms-версии.
/// Хранятся в JSON-файле рядом с приложением: Settings/websettings.json
/// </summary>
public class WebSettings
{
    // Сортировка детализаций
    public string SorterLastFolder { get; set; } = string.Empty;

    // Сервисные листы
    public string RegistryLastFolder { get; set; } = string.Empty;
    public string RegistryLastPeriod { get; set; } = string.Empty;
    public string RegistryLastProject { get; set; } = string.Empty;

    // Авто подпись СЛ
    public string PdfInputPath { get; set; } = string.Empty;
    public string PdfOutputPath { get; set; } = string.Empty;
    public string PdfLastName { get; set; } = string.Empty;
    public string PdfFirstName { get; set; } = string.Empty;
    public string PdfPatronymic { get; set; } = string.Empty;

    // Заявки МФ
    public string ZayavkiExcelFilePath { get; set; } = string.Empty;
    public string ZayavkiFilesDir { get; set; } = string.Empty;
    public string ZayavkiRecipientEmail { get; set; } = string.Empty;
    public string ZayavkiCcEmail { get; set; } = string.Empty;

    // Мультизапись
    public string BurnerRootFolder { get; set; } = string.Empty;

    // Экспорт имён файлов
    public string ExporterFolder { get; set; } = string.Empty;
    public string ExporterOutputRoot { get; set; } = string.Empty;
}

public class WebSettingsStore
{
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    private readonly string _settingsDirectory;
    private readonly string _settingsFilePath;
    private readonly object _lock = new();

    public WebSettingsStore()
    {
        _settingsDirectory = Path.Combine(AppContext.BaseDirectory, "Settings");
        _settingsFilePath = Path.Combine(_settingsDirectory, "websettings.json");
    }

    public WebSettings Load()
    {
        lock (_lock)
        {
            try
            {
                if (!File.Exists(_settingsFilePath))
                    return new WebSettings();

                var json = File.ReadAllText(_settingsFilePath);
                return JsonSerializer.Deserialize<WebSettings>(json, JsonOptions) ?? new WebSettings();
            }
            catch
            {
                return new WebSettings();
            }
        }
    }

    public void Save(WebSettings settings)
    {
        lock (_lock)
        {
            Directory.CreateDirectory(_settingsDirectory);
            var json = JsonSerializer.Serialize(settings, JsonOptions);
            File.WriteAllText(_settingsFilePath, json);
        }
    }
}
