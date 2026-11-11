using System.Text.Json.Serialization;

namespace STO_WORKBENCH.Core
{
    // Модель настроек приложения: пути к папкам обработки, ФИО исполнителя, версия.
    // Сериализуется в JSON-файл настроек; имена полей в файле задаются атрибутами JsonPropertyName.
    public class AppSettings
    {
        // Путь к папке с исходными файлами для обработки
        [JsonPropertyName("inputPath")]
        public string InputPath { get; set; } = string.Empty;

        // Путь к папке, куда складываются результаты обработки
        [JsonPropertyName("outputPath")]
        public string OutputPath { get; set; } = string.Empty;

        // Имя исполнителя (используется для подписей в документах)
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; } = string.Empty;

        // Фамилия исполнителя
        [JsonPropertyName("lastName")]
        public string LastName { get; set; } = string.Empty;

        // Отчество исполнителя
        [JsonPropertyName("patronymic")]
        public string Patronymic { get; set; } = string.Empty;

        // Версия приложения, записанная в настройках (для сверки при обновлении)
        [JsonPropertyName("version")]
        public string Version { get; set; } = string.Empty;

        // Проверка, что обязательные настройки (пути и ФИО) заполнены.
        // Возвращает true, только если ни одно из полей не пустое и не состоит из пробелов.
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(InputPath) &&
                   !string.IsNullOrWhiteSpace(OutputPath) &&
                   !string.IsNullOrWhiteSpace(FirstName) &&
                   !string.IsNullOrWhiteSpace(LastName) &&
                   !string.IsNullOrWhiteSpace(Patronymic);
        }

        // Формирует сокращённое ФИО исполнителя вида "Фамилия И. О." для подписей в документах.
        // Если какая-то часть ФИО не заполнена, возвращает пустую строку.
        public string GetExecutorShortName()
        {
            if (string.IsNullOrWhiteSpace(LastName) ||
                string.IsNullOrWhiteSpace(FirstName) ||
                string.IsNullOrWhiteSpace(Patronymic))
                return string.Empty;

            return $"{LastName} {FirstName[0]}. {Patronymic[0]}.";
        }
    }

    // Модель данных заявителя (ФИО) для подстановки в PDF-документы.
    // Отдельный класс нужен, чтобы хранить пользовательский вариант ФИО
    // независимо от основных настроек исполнителя.
    public class ApplicantInfo
    {
        // Фамилия заявителя
        public string LastName { get; set; } = string.Empty;

        // Имя заявителя
        public string FirstName { get; set; } = string.Empty;

        // Отчество заявителя
        public string Patronymic { get; set; } = string.Empty;

        // Проверка, что ФИО заявителя полностью заполнено
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(LastName) &&
                   !string.IsNullOrWhiteSpace(FirstName) &&
                   !string.IsNullOrWhiteSpace(Patronymic);
        }

        // Формирует сокращённое ФИО вида "Фамилия И. О.".
        // При неполном ФИО возвращает пустую строку.
        public string GetShortName()
        {
            if (!IsValid()) return string.Empty;
            return $"{LastName} {FirstName[0]}. {Patronymic[0]}.";
        }
    }

    // Параметры обработки PDF-документов: какие поля подставлять и какие значения использовать.
    // Настройки позволяют заменить дату и ФИО заявителя в шаблонах на пользовательские.
    public class PdfProcessingOptions
    {
        // Использовать пользовательскую дату вместо текущей
        public bool UseCustomDate { get; set; }

        // Заполнять в документе только месяц и год (без числа)
        public bool FillOnlyMonthYear { get; set; }

        // Использовать пользовательское ФИО заявителя вместо ФИО исполнителя
        public bool UseCustomApplicant { get; set; }

        // Пользовательская дата для подстановки в документ (null — брать текущую)
        public DateTime? CustomDateTime { get; set; }

        // Пользовательское ФИО заявителя (null — брать из основных настроек)
        public ApplicantInfo? CustomApplicant { get; set; }
    }
}
