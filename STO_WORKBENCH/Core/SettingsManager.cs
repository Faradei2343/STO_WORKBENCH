using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STO_WORKBENCH.Core
{
    /// <summary>
    /// Обёртка для доступа к настройкам приложения
    /// </summary>
    // Статическая обёртка над автогенерированным Properties.Settings.Default:
    // даёт типизированные свойства для каждой пользовательской настройки
    // (значения сохраняются в user.config рядом с профилем пользователя)
    // и централизованные методы Save/Reload. Значения в свойствах хранятся
    // только в памяти до явного вызова Save().
    public static class SettingsManager
    {
        // ---------- Геттеры и сеттеры для каждой настройки ----------
       
        // Sorter
        // Последняя папка, выбранная в модуле сортировщика детализаций
        public static string SorterLastFolder
        {
            get => Properties.Settings.Default.SorterLastFolder;
            set => Properties.Settings.Default.SorterLastFolder = value;
        }

        // Registry
        // Последняя папка с исходными det-файлами в модуле построения реестров
        public static string RegistryLastFolder
        {
            get => Properties.Settings.Default.RegistryLastFolder;
            set => Properties.Settings.Default.RegistryLastFolder = value;
        }
        // Последний введённый отчётный период в модуле построения реестров
        public static string RegistryLastPeriod
        {
            get => Properties.Settings.Default.RegistryLastPeriod;
            set => Properties.Settings.Default.RegistryLastPeriod = value;
        }
        // Последнее введённое название проекта в модуле построения реестров
        public static string RegistryLastProject
        {
            get => Properties.Settings.Default.RegistryLastProject;
            set => Properties.Settings.Default.RegistryLastProject = value;
        }

        // PDF Filler
        // Путь к папке с исходными PDF-документами
        public static string PDFInputPath
        {
            get => Properties.Settings.Default.PDFInputPath;
            set => Properties.Settings.Default.PDFInputPath = value;
        }
        // Путь к папке для сохранения заполненных PDF-документов
        public static string PDFOutputPath
        {
            get => Properties.Settings.Default.PDFOutputPath;
            set => Properties.Settings.Default.PDFOutputPath = value;
        }
        // Имя исполнителя для PDF-модуля
        public static string PDFFirstName
        {
            get => Properties.Settings.Default.PDFFirstName;
            set => Properties.Settings.Default.PDFFirstName = value;
        }
        // Фамилия исполнителя для PDF-модуля
        public static string PDFLastName
        {
            get => Properties.Settings.Default.PDFLastName;
            set => Properties.Settings.Default.PDFLastName = value;
        }
        // Отчество исполнителя для PDF-модуля
        public static string PDFPatronymic
        {
            get => Properties.Settings.Default.PDFPatronymic;
            set => Properties.Settings.Default.PDFPatronymic = value;
        }

        // Общие
        // Индекс вкладки, открытой при последнем закрытии приложения
        // (чтобы восстановить её при следующем запуске)
        public static int LastUsedTab
        {
            get => Properties.Settings.Default.LastUsedTab;
            set => Properties.Settings.Default.LastUsedTab = value;
        }

        /// <summary>
        /// Сохранить все настройки
        /// </summary>
        // Принудительно сбрасывает все изменённые значения настроек в файл user.config
        public static void Save()
        {
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Перезагрузить настройки из файла
        /// </summary>
        // Сбрасывает все несохранённые изменения и загружает значения заново из user.config
        public static void Reload()
        {
            Properties.Settings.Default.Reload();
        }
    }
}