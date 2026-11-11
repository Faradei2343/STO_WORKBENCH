namespace STO_WORKBENCH.Web.Services;

// Проверка состояния носителя в приводе через IMAPI2.
// Нужен, чтобы фоновый worker мог определить момент,
// когда пользователь вставил чистый диск.
// COM-вызовы — только внутри STA-потока BurnWorker.
public class ImapiMediaChecker
{
    // Проверяет, что в приводе recorderId находится чистый (пустой) диск,
    // пригодный для записи. Любое исключение IMAPI2 вызывающий код
    // (WaitForBlankDisc) трактует как «диск не готов».
    public bool IsBlankMediaReady(string recorderId)
    {
        dynamic recorder = ComInterop.Create(ComInterop.ImapiDiscRecorder2);
        recorder.InitializeDiscRecorder(recorderId);

        dynamic dataWriter = ComInterop.Create(ComInterop.ImapiDiscFormat2Data);

        // Привод должен быть поддерживаемым рекордером.
        if (!(bool)dataWriter.IsRecorderSupported(recorder))
            return false;

        dataWriter.Recorder = recorder;

        // В приводе должен находиться носитель, с которым умеет работать IMAPI2.
        if (!(bool)dataWriter.IsCurrentMediaSupported(recorder))
            return false;

        // Носитель должен выглядеть пустым (эвристическая проверка IMAPI2).
        return (bool)dataWriter.MediaHeuristicallyBlank;
    }
}
