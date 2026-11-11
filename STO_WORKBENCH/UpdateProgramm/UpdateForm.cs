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
    // Форма автообновления программы: показывает информацию о найденном релизе
    // (текущая версия, тег новой версии, описание изменений) и предлагает либо
    // скачать и установить обновление с индикацией прогресса, либо пропустить его.
    public partial class UpdateForm : Form
    {
        // Сведения о доступном релизе (тег версии, заметки "Что нового").
        private readonly ReleaseInfo _release;
        // Сервис загрузки и установки обновлений (определён в STO_WORKBENCH.Core).
        private readonly UpdateService _service;

        // Конструктор: запоминает релиз и сервис, создаёт контролы формы
        // и заполняет надписи версией, тегом релиза и заметками.
        public UpdateForm(ReleaseInfo release, UpdateService service)
        {
            _release = release;
            _service = service;

            InitializeComponent();

            // Заполняем данные релиза после InitializeComponent
            // Текущая версия программы берётся из сервиса обновлений.
            lblCurrentVersion.Text = $"Установлена версия: {UpdateService.GetCurrentVersion()}";
            // Тег нового релиза (например "v1.2.3") — в бейдж версии.
            lblVersionBadge.Text = _release.TagName;
            // Заметки релиза; если они пустые — показываем заглушку.
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
