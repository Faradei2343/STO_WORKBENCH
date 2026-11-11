using System;
using IMAPI2;

namespace STO_WORKBENCH.MultiBurner
{
    // Проверка состояния носителя в приводе через IMAPI2.
    // Нужен, чтобы фоновый worker мог определить момент,
    // когда пользователь вставил чистый диск.
    internal class ImapiMediaChecker
    {
        // Проверяет, что в приводе recorderId находится чистый (пустой) диск,
        // пригодный для записи.
        // Возвращает true, если диск вставлен, поддерживается и пуст;
        // false — в любой другой ситуации (нет диска, диск уже записан,
        // привод занят и т.п.).
        // Все проверки "мягкие": любое исключение IMAPI2 трактуется как false.
        public bool IsBlankMediaReady(string recorderId)
        {
            MsftDiscRecorder2 recorder = new MsftDiscRecorder2();
            recorder.InitializeDiscRecorder(recorderId);

            MsftDiscFormat2Data dataWriter = new MsftDiscFormat2Data();

            // Привод должен быть поддерживаемым рекордером.
            if (!dataWriter.IsRecorderSupported(recorder))
                return false;

            dataWriter.Recorder = recorder;

            // В приводе должен находиться носитель, с которым умеет работать IMAPI2.
            if (!dataWriter.IsCurrentMediaSupported(recorder))
                return false;

            // Носитель должен выглядеть пустым (эвристическая проверка IMAPI2).
            return dataWriter.MediaHeuristicallyBlank;
        }

    }
}
