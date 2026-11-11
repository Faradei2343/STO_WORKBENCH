namespace STO_WORKBENCH
{
    // Точка входа приложения STO WORKBENCH (WinForms).

    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            // ApplicationConfiguration.Initialize() применяет настройки из ApplicationConfiguration
            // (высокий DPI и т.п.) и создаёт ApplicationContext по умолчанию.
            ApplicationConfiguration.Initialize();

            // Запуск главной формы-меню приложения; Message Loop живёт, пока открыта эта форма.
            Application.Run(new MainPageForm());
        }
    }
}