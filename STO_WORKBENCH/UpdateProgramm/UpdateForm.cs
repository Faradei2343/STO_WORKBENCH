using STO_WORKBENCH.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace STO_WORKBENCH.UpdateProgramm
{
    public partial class UpdateForm : Form
    {
        private readonly ReleaseInfo _release;
        private readonly UpdateService _service;

        public UpdateForm(ReleaseInfo release, UpdateService service)
        {
            _release = release;
            _service = service;

            InitializeComponent();

            // Заполняем данные релиза после InitializeComponent
            lblCurrentVersion.Text = $"Установлена версия: {UpdateService.GetCurrentVersion()}";
            lblVersionBadge.Text = _release.TagName;
            rtbNotes.Text = string.IsNullOrWhiteSpace(_release.Notes)
                                         ? "Описание для этого релиза не указано."
                                         : _release.Notes;
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            SetDownloadingState(true);

            var progress = new Progress<int>(pct =>
            {
                pgDownload.Value = pct;
                lblPercent.Text = $"{pct}%";
            });

            try
            {
                await _service.DownloadAndInstallAsync(_release, progress);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ошибка при загрузке обновления:\n{ex.Message}",
                    "Ошибка обновления",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                SetDownloadingState(false);
            }
        }

        private void btnSkip_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SetDownloadingState(bool isDownloading)
        {
            btnUpdate.Enabled = !isDownloading;
            btnSkip.Enabled = !isDownloading;

            if (isDownloading)
            {
                panelProgress.Visible = true;
                pgDownload.Value = 0;
                lblPercent.Text = "0%";
                // Расширяем форму под панель прогресса
                ClientSize = new System.Drawing.Size(
                    ClientSize.Width,
                    ClientSize.Height + panelProgress.Height);
            }
            else
            {
                panelProgress.Visible = false;
                ClientSize = new System.Drawing.Size(
                    ClientSize.Width,
                    ClientSize.Height - panelProgress.Height);
            }
        }
    }
}
