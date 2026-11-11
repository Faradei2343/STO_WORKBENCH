namespace STO_WORKBENCH.Web.Services;

/// <summary>
/// Выполнение кода с COM-объектами в выделенном STA-потоке.
/// Blazor Server работает в MTA-потоках, а Excel/Outlook требуют STA.
/// </summary>
public static class StaComRunner
{
    public static Task RunAsync(Action action, CancellationToken ct = default)
    {
        var tcs = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);

        var thread = new Thread(() =>
        {
            try
            {
                action();
                tcs.TrySetResult();
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested)
            {
                tcs.TrySetCanceled(ct);
            }
            catch (Exception ex)
            {
                tcs.TrySetException(ex);
            }
        });

        thread.SetApartmentState(ApartmentState.STA);
        thread.IsBackground = true;
        thread.Start();

        return tcs.Task;
    }
}
