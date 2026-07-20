using STO_WORKBENCH.Core;
using STO_WORKBENCH.PDFFiller;
using STO_WORKBENCH.SLRegistry;
using STO_WORKBENCH.UpdateProgramm;
using STO_WORKBENCH.ZayavkoDrochitel3000;
using STO_WORKBENCH.MultiBurner;
using System.Reflection;

namespace STO_WORKBENCH
{
    public partial class MainPageForm : Form
    {
        private readonly UpdateService _updateService = new UpdateService();
        
        public MainPageForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblWelcomeSub.Text=lblWelcomeSub.Text+" Версия: "+ UpdateService.GetCurrentVersion();
            // Чистим .old от прошлого обновления
            UpdateService.CleanupOldFiles();

            // Проверка обновлений в фоне — не блокирует загрузку формы
            _ = CheckForUpdatesAsync();
        }

        private async Task CheckForUpdatesAsync()
        {
            var release = await _updateService.CheckForUpdateAsync();
            if (release == null) return;

            // Показываем диалог только после полной загрузки формы
            await Task.Delay(500);

            using var form = new UpdateForm(release, _updateService);
            form.ShowDialog(this);
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            Form FormSort = new FormSort();
            FormSort.ShowDialog();
        }

        private void buttonSL_Click(object sender, EventArgs e)
        {
            Form SLRegistry = new FormSLRegistry();
            SLRegistry.ShowDialog();
        }

        private void buttonAutoSL_Click(object sender, EventArgs e)
        {
            Form FormPDFFiller = new FormPDFFiller();
            FormPDFFiller.ShowDialog();
        }

        private void buttonZayavki_Click(object sender, EventArgs e)
        {
            Form form = new FormZayavkodrochitel3000();
            form.ShowDialog();
        }

        private void buttonMultiburner_Click(object sender, EventArgs e)
        {
            {
                Form FormMultiburner = new BurnerForm();
                FormMultiburner.ShowDialog();
            }
        }
    }
}
