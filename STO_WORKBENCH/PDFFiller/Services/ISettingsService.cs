using STO_WORKBENCH.Core;

namespace STO_WORKBENCH.PDFFiller.Services
{
    // Интерфейс сервиса хранения настроек приложения (пути к папкам, ФИО исполнителя).
    // Настройки сериализуются в JSON-файл settings.json рядом с программой (подкаталог Settings).
    // Реализация — SettingsService.
    public interface ISettingsService
    {
        // Проверяет, существует ли файл настроек settings.json
        // (при необходимости заранее создаёт каталог настроек).
        // Возвращает true, если файл есть и его можно загружать.
        bool SettingsFileExists();

        // Загружает и десериализует настройки из JSON-файла.
        // Возвращает объект AppSettings или null, если файла нет либо он повреждён
        // (об ошибке пользователь уже уведомлён через IMessageService).
        AppSettings? LoadSettings();

        // Сериализует настройки в JSON и записывает их в файл settings.json.
        // settings — объект настроек для сохранения.
        void SaveSettings(AppSettings settings);

        // Возвращает полный путь к каталогу, в котором лежит файл настроек.
        string GetSettingsDirectory();
    }
}
