using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STO_WORKBENCH.Core
{
    public class UpdateService
    {
        // Замени на свой репозиторий
        private const string RepoOwner = "Faradei2343";
        private const string RepoName = "STO_WORKBENCH";
        private const string ApiUrl = $"https://api.github.com/repos/{RepoOwner}/{RepoName}/releases/latest";

        private static readonly HttpClient _http = new HttpClient();

        static UpdateService()
        {
            // GitHub API требует User-Agent
            _http.DefaultRequestHeaders.UserAgent
                .ParseAdd("STO_WORKBENCH-Updater/1.0");
        }

        /// <summary>
        /// Проверяет наличие обновления.
        /// Возвращает информацию о релизе если есть новая версия, иначе null.
        /// </summary>
        public async Task<ReleaseInfo?> CheckForUpdateAsync()
        {
            try
            {
                var release = await _http.GetFromJsonAsync<GitHubRelease>(ApiUrl);
                if (release == null) return null;

                // Тег вида "v1.2.3" или "1.2.3"
                var tagVersion = release.tag_name.TrimStart('v');

                if (Version.TryParse(tagVersion, out var remoteVersion) &&
                    remoteVersion > GetCurrentVersion())
                {
                    // Ищем asset — файл .exe в релизе
                    string? downloadUrl = null;
                    foreach (var asset in release.assets)
                    {
                        if (asset.name.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
                        {
                            downloadUrl = asset.browser_download_url;
                            break;
                        }
                    }

                    return new ReleaseInfo
                    {
                        Version = remoteVersion,
                        DownloadUrl = downloadUrl,
                        Notes = release.body,
                        TagName = release.tag_name
                    };
                }

                return null;
            }
            catch
            {
                // Нет интернета или GitHub недоступен — молча пропускаем
                return null;
            }
        }

        /// <summary>
        /// Скачивает новый .exe и перезапускает приложение.
        /// </summary>
        public async Task DownloadAndInstallAsync(ReleaseInfo release, IProgress<int> progress)
        {
            if (string.IsNullOrEmpty(release.DownloadUrl))
                throw new InvalidOperationException("Ссылка на загрузку не найдена.");

            var currentExe = Application.ExecutablePath;
            var newExe = currentExe + ".new";
            var oldExe = currentExe + ".old";

            // Скачиваем во временный файл
            using (var response = await _http.GetAsync(
                release.DownloadUrl,
                HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();

                var total = response.Content.Headers.ContentLength ?? -1L;
                var downloaded = 0L;

                await using var fileStream = new FileStream(newExe, FileMode.Create);
                await using var stream = await response.Content.ReadAsStreamAsync();

                var buffer = new byte[8192];
                int read;
                while ((read = await stream.ReadAsync(buffer)) > 0)
                {
                    await fileStream.WriteAsync(buffer.AsMemory(0, read));
                    downloaded += read;

                    if (total > 0)
                        progress?.Report((int)(downloaded * 100 / total));
                }
            }

            // Переименовываем текущий .exe в .old, новый в текущий
            if (File.Exists(oldExe)) File.Delete(oldExe);
            File.Move(currentExe, oldExe);
            File.Move(newExe, currentExe);

            // Запускаем новую версию и выходим
            Process.Start(new ProcessStartInfo
            {
                FileName = currentExe,
                UseShellExecute = true
            });

            Application.Exit();
        }

        /// <summary>
        /// Удаляет .old файл от предыдущего обновления если есть.
        /// </summary>
        public static void CleanupOldFiles()
        {
            var oldExe = Application.ExecutablePath + ".old";
            if (File.Exists(oldExe))
            {
                try { File.Delete(oldExe); }
                catch { /* не критично */ }
            }
        }

        public static Version GetCurrentVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version ?? new Version(0, 0, 0);
        }

        // ── Модели GitHub API ─────────────────────────────────────────────

        private class GitHubRelease
        {
            public string tag_name { get; set; } = "";
            public string body { get; set; } = "";
            public GitHubAsset[] assets { get; set; } = Array.Empty<GitHubAsset>();
        }

        private class GitHubAsset
        {
            public string name { get; set; } = "";
            public string browser_download_url { get; set; } = "";
        }
    }

    public class ReleaseInfo
    {
        public Version Version { get; set; } = new Version(0, 0, 0);
        public string? DownloadUrl { get; set; }
        public string Notes { get; set; } = "";
        public string TagName { get; set; } = "";
    }
}