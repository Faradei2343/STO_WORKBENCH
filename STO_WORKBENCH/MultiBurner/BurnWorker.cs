using System;
using System.Collections.Concurrent;
using System.Threading;
using static STO_WORKBENCH.MultiBurner.Models;

namespace STO_WORKBENCH.MultiBurner
{
    internal class BurnWorker
    {

        private readonly DriveInfoModel _drive;
        private readonly ConcurrentQueue<BurnJob> _queue;
        private readonly CancellationToken _token;

        private Thread _thread;

        public event Action<string> Log;
        public event Action<BurnJob> JobUpdated;
        public event Action<string, string> NeedDisc;
        public event Action<string, string> JobCompleted;
        public event Action<string, string> JobError;

        public BurnWorker(
            DriveInfoModel drive,
            ConcurrentQueue<BurnJob> queue,
            CancellationToken token)
        {
            _drive = drive;
            _queue = queue;
            _token = token;
        }

        public void Start()
        {
            _thread = new Thread(Run);
            _thread.IsBackground = true;
            _thread.SetApartmentState(ApartmentState.STA);
            _thread.Start();
        }

        private void Run()
        {
            ImapiBurnService burnService = new ImapiBurnService();

            while (!_token.IsCancellationRequested)
            {
                if (!_queue.TryDequeue(out BurnJob job))
                {
                    WriteLog(_drive.DisplayName + ": очередь пуста, работа завершена.");
                    break;
                }

                try
                {
                    job.Status = BurnJobStatus.Burning;
                    job.AssignedDrive = _drive.DisplayName;
                    job.ErrorMessage = null;
                    JobUpdated?.Invoke(job);

                    WriteLog(_drive.DisplayName + ": подготовка к записи " + job.FolderName);

                    TryEject(burnService);

                    NeedDisc?.Invoke(_drive.DisplayName, job.FolderName);

                    WaitForBlankDisc(job);

                    if (_token.IsCancellationRequested)
                        break;

                    WriteLog(_drive.DisplayName + ": запись началась: " + job.FolderName);

                    burnService.BurnFolderToDisc(
                        _drive.RecorderId,
                        job.FolderPath,
                        job.FolderName,
                        includeRootFolder: true);

                    job.Status = BurnJobStatus.Done;
                    JobUpdated?.Invoke(job);

                    WriteLog(_drive.DisplayName + ": запись завершена: " + job.FolderName);

                    TryEject(burnService);

                    JobCompleted?.Invoke(_drive.DisplayName, job.FolderName);
                }
                catch (OperationCanceledException)
                {
                    WriteLog(_drive.DisplayName + ": операция остановлена.");
                    break;
                }
                catch (Exception ex)
                {
                    job.Status = BurnJobStatus.Error;
                    job.ErrorMessage = ex.Message;
                    JobUpdated?.Invoke(job);

                    WriteLog(_drive.DisplayName + ": ошибка записи " + job.FolderName + ": " + ex.Message);

                    TryEject(burnService);

                    JobError?.Invoke(job.FolderName, ex.Message);
                }
            }
        }

        private void WaitForBlankDisc(BurnJob job)
        {
            ImapiMediaChecker checker = new ImapiMediaChecker();

            while (!_token.IsCancellationRequested)
            {
                try
                {
                    if (checker.IsBlankMediaReady(_drive.RecorderId))
                    {
                        WriteLog(_drive.DisplayName + ": чистый диск обнаружен для " + job.FolderName);
                        return;
                    }
                }
                catch
                {
                    // диск не вставлен, лоток открыт или диск не читается
                }

                WriteLog(_drive.DisplayName + ": вставьте чистый диск для " + job.FolderName);
                Thread.Sleep(3000);
            }

            throw new OperationCanceledException();
        }

        private void TryEject(ImapiBurnService burnService)
        {
            try
            {
                burnService.Eject(_drive.RecorderId);
            }
            catch (Exception ex)
            {
                WriteLog(_drive.DisplayName + ": не удалось открыть лоток: " + ex.Message);
            }
        }

        private void WriteLog(string message)
        {
            Log?.Invoke(message);
            LogService.Write(message);
        }

    }
}
