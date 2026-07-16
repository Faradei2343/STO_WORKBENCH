using STO_WORKBENCH.Core;

namespace STO_WORKBENCH.PDFFiller.Services
{
    public interface ISettingsService
    {
        bool SettingsFileExists();
        AppSettings? LoadSettings();
        void SaveSettings(AppSettings settings);
        string GetSettingsDirectory();
    }
}
