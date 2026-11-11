using System.Collections.Concurrent;
using static STO_WORKBENCH.Web.Services.Models;

namespace STO_WORKBENCH.Web.Services;

// Общее состояние модуля мультизаписи (singleton).
// Живет фоново: пользователь может уйти со страницы и вернуться —
// задания, приводы и лог сохраняются. Событие Changed уведомляет
// страницу об изменениях (страница делает InvokeAsync(StateHasChanged)).
public class BurnerStateService
{
    private readonly object _sync = new();
    private readonly List<string> _log = new();

    private ConcurrentQueue<BurnJob>? _queue;
    private CancellationTokenSource? _cts;
    private bool _finishLogged;

    // Задания (строки таблицы) и приводы показываются на странице напрямую,
    // поэтому изменять их можно только под _sync.
    public List<BurnJob> Jobs { get; } = new();
    public List<DriveInfoModel> Drives { get; } = new();

    public bool IsRunning { get; private set; }

    public event Action? Changed;

    public IReadOnlyList<string> LogLines
    {
        get
        {
            lock (_sync)
            {
                return _log.ToArray();
            }
        }
    }

    public void AddLog(string text)
    {
        lock (_sync)
        {
            _log.Add(string.Format("{0:HH:mm:ss} {1}", DateTime.Now, text));

            // защита от бесконечного роста лога за длительный сеанс
            if (_log.Count > 3000)
                _log.RemoveRange(0, _log.Count - 3000);
        }

        Changed?.Invoke();
    }

    public List<BurnJob> GetJobsSnapshot()
    {
        lock (_sync)
        {
            return Jobs.ToList();
        }
    }

    public void SetJobs(IEnumerable<BurnJob> jobs)
    {
        lock (_sync)
        {
            Jobs.Clear();
            Jobs.AddRange(jobs);
        }

        Changed?.Invoke();
    }

    public void SetDrives(IEnumerable<DriveInfoModel> drives)
    {
        lock (_sync)
        {
            Drives.Clear();
            Drives.AddRange(drives);
        }

        Changed?.Invoke();
    }

    // Запускает сеанс записи: общая очередь заданий + один BurnWorker (STA) на привод.
    public void StartBurning(List<BurnJob> jobs, List<DriveInfoModel> drives)
    {
        CancellationToken token;

        lock (_sync)
        {
            _queue = new ConcurrentQueue<BurnJob>(jobs);
            _cts = new CancellationTokenSource();
            _finishLogged = false;
            IsRunning = true;
            token = _cts.Token;
        }

        foreach (BurnJob job in jobs)
        {
            job.Queued = true;
            job.Status = BurnJobStatus.Waiting;
            job.AssignedDrive = string.Empty;
            job.ErrorMessage = string.Empty;
        }

        foreach (DriveInfoModel drive in drives)
        {
            BurnWorker worker = new BurnWorker(drive, _queue, token);

            worker.Log += AddLog;
            worker.JobUpdated += OnJobUpdated;
            worker.NeedDisc += OnNeedDisc;
            worker.JobCompleted += OnJobCompleted;
            worker.JobError += OnJobError;

            worker.Start();
        }

        Changed?.Invoke();
    }

    public void Stop()
    {
        lock (_sync)
        {
            _cts?.Cancel();
            IsRunning = false;
        }

        AddLog("Запрошена остановка. Если диск уже пишется, операция может завершиться не сразу.");
    }

    private void OnJobUpdated(BurnJob job)
    {
        Changed?.Invoke();
        CheckAllJobsFinished();
    }

    private void OnNeedDisc(BurnJob job)
    {
        AddLog((job.AssignedDrive ?? string.Empty) + ": вставьте чистый диск для " + job.FolderName);
    }

    private void OnJobCompleted(BurnJob job)
    {
        // Как и в десктопе, выполненное задание снимаем с выбора,
        // чтобы его случайно не отправить в повторную запись.
        job.Selected = false;

        AddLog((job.AssignedDrive ?? string.Empty) + ": диск записан: " + job.FolderName);
    }

    private void OnJobError(BurnJob job, string error)
    {
        AddLog("Ошибка записи " + job.FolderName + ": " + error);
    }

    private void CheckAllJobsFinished()
    {
        lock (_sync)
        {
            if (!IsRunning || _finishLogged || _queue == null)
                return;

            bool hasActive = _queue.Count > 0 || Jobs.Any(j =>
                j.Status == BurnJobStatus.Waiting || j.Status == BurnJobStatus.Burning);

            if (hasActive)
                return;

            _finishLogged = true;
            IsRunning = false;
        }

        AddLog("Все доступные задания завершены или обработаны.");
    }
}
