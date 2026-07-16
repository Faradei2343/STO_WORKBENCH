using System.Windows.Forms;

namespace STO_WORKBENCH.Core
{
    public static class DialogHelper
    {
        public static string SelectFolder(string description = "Выберите папку")
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.Description = description;
                fbd.ShowNewFolderButton = true;
                return fbd.ShowDialog() == DialogResult.OK ? fbd.SelectedPath : null;
            }
        }

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