namespace STO_WORKBENCH.Web.Services.Zayavki;

// DTO модуля «Заявки МФ»: собираются на странице и передаются в сервисы
// ExcelService / OutlookService (порт WinForms-модуля ZayavkoDrochitel3000).

// Параметры генерации файлов заявок из Excel-шаблона.
public class GenerationOptions
{
    // Полный путь к исходному файлу Excel-шаблона (на его основе создаются все заявки).
    public string ExcelFilePath { get; set; } = string.Empty;

    // Начальный номер заявки (включительно), например 100 → «МФ100».
    public int StartCount { get; set; }

    // Конечный номер заявки (включительно), например 105 → «МФ105».
    public int EndCount { get; set; }
}

// Параметры отправки готовых заявок по e-mail через Outlook.
public class SendOptions
{
    // Папка с готовыми PDF/XLSX.
    public string FilesDir { get; set; } = string.Empty;

    // Начальный номер заявки (включительно).
    public int StartCount { get; set; }

    // Конечный номер заявки (включительно).
    public int EndCount { get; set; }

    // Адрес основного получателя (поле «Кому» письма).
    public string RecipientEmail { get; set; } = string.Empty;

    // Адрес для копии (поле «Копия»); может быть пустым — копия не добавляется.
    public string CcEmail { get; set; } = string.Empty;
}
