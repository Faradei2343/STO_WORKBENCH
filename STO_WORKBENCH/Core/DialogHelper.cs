using System.Windows.Forms;

namespace STO_WORKBENCH.Core
{
    // Статический помощник для вызова стандартных WinForms-диалогов выбора
    // папки и файла. Единообразно обрабатывает отмену: возвращает null.
    public static class DialogHelper
    {
        // Открывает стандартный диалог выбора папки.
        // description — текст-подсказка, отображаемый внутри диалога.
        // Кнопка создания новой папки включена, чтобы пользователь мог сразу создать целевую.
        // Возвращает путь к выбранной папке либо null, если пользователь нажал "Отмена".
        public static string SelectFolder(string description = "Выберите папку")
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.Description = description;
                fbd.ShowNewFolderButton = true;
                return fbd.ShowDialog() == DialogResult.OK ? fbd.SelectedPath : null;
            }
        }

        // Открывает стандартный диалог выбора файла.
        // filter — строка фильтра в формате WinForms ("Описание|маска|..."),
        // по умолчанию разрешены все файлы.
        // Возвращает полный путь к выбранному файлу либо null при отмене.
        public static string SelectFile(string filter = "Все файлы (*.*)|*.*")
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = filter;
                return ofd.ShowDialog() == DialogResult.OK ? ofd.FileName : null;
            }
        }
    }
}