namespace STO_WORKBENCH.ZayavkoDrochitel3000
{
    // Набор простых моделей настроек (DTO) для модуля «Заявки МФ»:
    // объекты этих классов собираются на форме из полей ввода
    // и передаются в сервисы ExcelService / OutlookService.

    // Параметры генерации файлов заявок из Excel-шаблона.
    public class GenerationOptions
    {
        // Полный путь к исходному файлу Excel-шаблона (на его основе создаются все заявки).
        public string ExcelFilePath { get; set; }

        // Начальный номер заявки (включительно), например 100 → «МФ100».
        public int StartCount { get; set; }

        // Конечный номер заявки (включительно), например 105 → «МФ105».
        public int EndCount { get; set; }
    }

    // Параметры отправки готовых заявок по e-mail через Outlook.
    public class SendOptions
    {
        public string FilesDir { get; set; }      // папка с готовыми PDF/XLSX

        // Начальный номер заявки (включительно).
        public int StartCount { get; set; }

        // Конечный номер заявки (включительно).
        public int EndCount { get; set; }

        // Адрес основного получателя (поле «Кому» письма).
        public string RecipientEmail { get; set; }

        // Адрес для копии (поле «Копия»); может быть пустым — копия не добавляется.
        public string CcEmail { get; set; }
    }
}
