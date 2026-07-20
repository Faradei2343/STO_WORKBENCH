using System.Text.Json.Serialization;

namespace STO_WORKBENCH.Core
{
    public class AppSettings
    {
        [JsonPropertyName("inputPath")]
        public string InputPath { get; set; } = string.Empty;

        [JsonPropertyName("outputPath")]
        public string OutputPath { get; set; } = string.Empty;

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; } = string.Empty;

        [JsonPropertyName("lastName")]
        public string LastName { get; set; } = string.Empty;

        [JsonPropertyName("patronymic")]
        public string Patronymic { get; set; } = string.Empty;

        [JsonPropertyName("version")]
        public string Version { get; set; } = string.Empty;

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(InputPath) &&
                   !string.IsNullOrWhiteSpace(OutputPath) &&
                   !string.IsNullOrWhiteSpace(FirstName) &&
                   !string.IsNullOrWhiteSpace(LastName) &&
                   !string.IsNullOrWhiteSpace(Patronymic);
        }

        public string GetExecutorShortName()
        {
            if (string.IsNullOrWhiteSpace(LastName) ||
                string.IsNullOrWhiteSpace(FirstName) ||
                string.IsNullOrWhiteSpace(Patronymic))
                return string.Empty;

            return $"{LastName} {FirstName[0]}. {Patronymic[0]}.";
        }
    }

    public class ApplicantInfo
    {
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Patronymic { get; set; } = string.Empty;

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(LastName) &&
                   !string.IsNullOrWhiteSpace(FirstName) &&
                   !string.IsNullOrWhiteSpace(Patronymic);
        }

        public string GetShortName()
        {
            if (!IsValid()) return string.Empty;
            return $"{LastName} {FirstName[0]}. {Patronymic[0]}.";
        }
    }

    public class PdfProcessingOptions
    {
        public bool UseCustomDate { get; set; }
        public bool FillOnlyMonthYear { get; set; }
        public bool UseCustomApplicant { get; set; }
        public DateTime? CustomDateTime { get; set; }
        public ApplicantInfo? CustomApplicant { get; set; }
    }
}
