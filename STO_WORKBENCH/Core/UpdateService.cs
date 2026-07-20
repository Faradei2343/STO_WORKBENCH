using System.Diagnostics;
using System.IO.Compression;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;

namespace STO_WORKBENCH.Core
{
    public class UpdateService
    {
        private const string RepoOwner = "Faradei2343";
        private const string RepoName = "STO_WORKBENCH";
        private const string ApiUrl = $"https://api.github.com/repos/{RepoOwner}/{RepoName}/releases/latest";

        private static readonly HttpClient _http = new HttpClient();

        static UpdateService()
        {
            _http.DefaultRequestHeaders.UserAgent.ParseAdd("STO_WORKBENCH-Updater/2.0");
        }

        /// <summary>
        /// Проверяет наличие обновления.
        /// Ищет ZIP-архив в последнем GitHub Release.
        /// </summary>
        public async Task<ReleaseInfo?> CheckForUpdateAsync()
        {
            try
            {
                GitHubRelease? release = await _http.GetFromJsonAsync<GitHubRelease>(ApiUrl);

                if (release == null)
                    return null;

                if (string.IsNullOrWhiteSpace(release.tag_name))
                    return null;

                string tagVersion = release.tag_name.TrimStart('v', 'V');

                if (!Version.TryParse(tagVersion, out Version? remoteVersion))
                    return null;

                Version currentVersion = GetCurrentVersion();

                if (remoteVersion <= currentVersion)
                    return null;

                GitHubAsset? zipAsset = release.assets
                    .FirstOrDefault(x =>
                        x.name.EndsWith(".zip", StringComparison.OrdinalIgnoreCase));

                if (zipAsset == null)
                    return new ReleaseInfo
                    {
                        Version = remoteVersion,
                        DownloadUrl = null,
                        AssetName = "",
                        Notes = release.body ?? "",
                        TagName = release.tag_name
                    };

                return new ReleaseInfo
                {
                    Version = remoteVersion,
                    DownloadUrl = zipAsset.browser_download_url,
                    AssetName = zipAsset.name,
                    Notes = release.body ?? "",
                    TagName = release.tag_name
                };
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Скачивает ZIP-архив обновления, распаковывает его во временную папку
        /// и запускает внешний update.cmd, который заменяет все файлы приложения.
        /// </summary>
        public async Task DownloadAndInstallAsync(ReleaseInfo release, IProgress<int>? progress)
        {
            if (release == null)
                throw new ArgumentNullException(nameof(release));

            if (string.IsNullOrWhiteSpace(release.DownloadUrl))
                throw new InvalidOperationException("В релизе не найден ZIP-архив обновления.");

            string appExePath = Application.ExecutablePath;
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string exeName = Path.GetFileName(appExePath);

            string updateRoot = Path.Combine(
                Path.GetTempPath(),
                "STO_WORKBENCH_Update_" + Guid.NewGuid().ToString("N"));

            string archivePath = Path.Combine(updateRoot, "update.zip");
            string extractPath = Path.Combine(updateRoot, "extracted");
            string updaterCmdPath = Path.Combine(updateRoot, "update.cmd");

            Directory.CreateDirectory(updateRoot);
            Directory.CreateDirectory(extractPath);

            await DownloadFileAsync(release.DownloadUrl, archivePath, progress);

            ZipFile.ExtractToDirectory(archivePath, extractPath);

            string sourceDirectory = DetectSourceDirectory(extractPath);

            CreateUpdaterCmd(
                updaterCmdPath,
                sourceDirectory,
                appDirectory,
                exeName);

            Process currentProcess = Process.GetCurrentProcess();

            Process.Start(new ProcessStartInfo
            {
                FileName = updaterCmdPath,
                Arguments =
                    "\"" + currentProcess.Id + "\" " +
                    "\"" + appDirectory.TrimEnd('\\') + "\" " +
                    "\"" + sourceDirectory.TrimEnd('\\') + "\" " +
                    "\"" + exeName + "\"",
                UseShellExecute = true,
                WorkingDirectory = updateRoot,
                WindowStyle = ProcessWindowStyle.Hidden
            });

            Application.Exit();
        }

        /// <summary>
        /// Скачивает файл с отображением прогресса.
        /// </summary>
        private async Task DownloadFileAsync(
            string url,
            string destinationPath,
            IProgress<int>? progress)
        {
            using HttpResponseMessage response = await _http.GetAsync(
                url,
                HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();

            long totalBytes = response.Content.Headers.ContentLength ?? -1L;
            long downloadedBytes = 0;

            using Stream inputStream = await response.Content.ReadAsStreamAsync();
            using FileStream outputStream = new FileStream(
                destinationPath,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                81920,
                true);

            byte[] buffer = new byte[81920];

            while (true)
            {
                int read = await inputStream.ReadAsync(buffer, 0, buffer.Length);

                if (read <= 0)
                    break;

                await outputStream.WriteAsync(buffer, 0, read);

                downloadedBytes += read;

                if (totalBytes > 0)
                {
                    int percent = (int)(downloadedBytes * 100L / totalBytes);
                    progress?.Report(percent);
                }
            }

            progress?.Report(100);
        }

        /// <summary>
        /// Определяет, где внутри архива лежат файлы приложения.
        /// 
        /// Поддерживает оба варианта:
        /// 
        /// Вариант 1:
        /// archive.zip
        ///   STO_WORKBENCH.exe
        ///   dll...
        /// 
        /// Вариант 2:
        /// archive.zip
        ///   STO_WORKBENCH/
        ///     STO_WORKBENCH.exe
        ///     dll...
        /// </summary>
        private string DetectSourceDirectory(string extractPath)
        {
            string[] files = Directory.GetFiles(extractPath);
            string[] directories = Directory.GetDirectories(extractPath);

            bool exeInRoot = Directory
                .GetFiles(extractPath, "*.exe", SearchOption.TopDirectoryOnly)
                .Any();

            if (exeInRoot)
                return extractPath;

            if (files.Length == 0 && directories.Length == 1)
                return directories[0];

            return extractPath;
        }

        /// <summary>
        /// Создает update.cmd, который после закрытия приложения
        /// копирует все файлы из новой версии в папку приложения.
        /// </summary>
        private void CreateUpdaterCmd(
            string updaterCmdPath,
            string sourceDirectory,
            string appDirectory,
            string exeName)
        {
            string script = @"
@echo off
setlocal

set PID=%~1
set APP_DIR=%~2
set SRC_DIR=%~3
set EXE_NAME=%~4

echo Waiting for application to close...

:wait_loop
tasklist /FI ""PID eq %PID%"" | find ""%PID%"" >nul
if not errorlevel 1 (
    timeout /t 1 /nobreak >nul
    goto wait_loop
)

echo Updating files...

robocopy ""%SRC_DIR%"" ""%APP_DIR%"" /E /COPY:DAT /R:3 /W:1 /NFL /NDL /NJH /NJS /NP

set ROBOCOPY_CODE=%ERRORLEVEL%

REM Robocopy codes 0-7 are success/warnings, 8+ are errors
if %ROBOCOPY_CODE% GEQ 8 (
    echo Update failed. Robocopy code: %ROBOCOPY_CODE%
    start """" ""%APP_DIR%\%EXE_NAME%""
    exit /b %ROBOCOPY_CODE%
)

echo Starting application...

start """" ""%APP_DIR%\%EXE_NAME%""

timeout /t 2 /nobreak >nul

exit /b 0
";

            File.WriteAllText(updaterCmdPath, script, Encoding.ASCII);
        }

        /// <summary>
        /// Удаляет старые временные папки обновлений.
        /// Вызывать при старте приложения.
        /// </summary>
        public static void CleanupOldFiles()
        {
            try
            {
                string tempPath = Path.GetTempPath();

                string[] updateDirectories = Directory.GetDirectories(
                    tempPath,
                    "STO_WORKBENCH_Update_*",
                    SearchOption.TopDirectoryOnly);

                foreach (string dir in updateDirectories)
                {
                    try
                    {
                        DirectoryInfo di = new DirectoryInfo(dir);

                        if (di.CreationTime < DateTime.Now.AddDays(-3))
                        {
                            di.Delete(true);
                        }
                    }
                    catch
                    {
                        // Не критично
                    }
                }
            }
            catch
            {
                // Не критично
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

        public string AssetName { get; set; } = "";

        public string Notes { get; set; } = "";

        public string TagName { get; set; } = "";
    }
}