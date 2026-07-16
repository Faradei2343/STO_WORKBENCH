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
    public static class SettingsManager
    {
        // ---------- Геттеры и сеттеры для каждой настройки ----------

        // Sorter
        public static string SorterLastFolder
        {
            get => Properties.Settings.Default.SorterLastFolder;
            set => Properties.Settings.Default.SorterLastFolder = value;
        }

        // Registry
        public static string RegistryLastFolder
        {
            get => Properties.Settings.Default.RegistryLastFolder;
            set => Properties.Settings.Default.RegistryLastFolder = value;
        }
        public static string RegistryLastPeriod
        {
            get => Properties.Settings.Default.RegistryLastPeriod;
            set => Properties.Settings.Default.RegistryLastPeriod = value;
        }
        public static string RegistryLastProject
        {
            get => Properties.Settings.Default.RegistryLastProject;
            set => Properties.Settings.Default.RegistryLastProject = value;
        }

        // PDF Filler
        public static string PDFInputPath
        {
            get => Properties.Settings.Default.PDFInputPath;
            set => Properties.Settings.Default.PDFInputPath = value;
        }
        public static string PDFOutputPath
        {
            get => Properties.Settings.Default.PDFOutputPath;
            set => Properties.Settings.Default.PDFOutputPath = value;
        }
        public static string PDFFirstName
        {
            get => Properties.Settings.Default.PDFFirstName;
            set => Properties.Settings.Default.PDFFirstName = value;
        }
        public static string PDFLastName
        {
            get => Properties.Settings.Default.PDFLastName;
            set => Properties.Settings.Default.PDFLastName = value;
        }
        public static string PDFPatronymic
        {
            get => Properties.Settings.Default.PDFPatronymic;
            set => Properties.Settings.Default.PDFPatronymic = value;
        }

        // Общие
        public static int LastUsedTab
        {
            get => Properties.Settings.Default.LastUsedTab;
            set => Properties.Settings.Default.LastUsedTab = value;
        }

        /// <summary>
        /// Сохранить все настройки
        /// </summary>
        public static void Save()
        {
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Перезагрузить настройки из файла
        /// </summary>
        public static void Reload()
        {
            Properties.Settings.Default.Reload();
        }
    }
}