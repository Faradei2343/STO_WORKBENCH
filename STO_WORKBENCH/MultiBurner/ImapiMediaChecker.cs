using System;
using IMAPI2;

namespace STO_WORKBENCH.MultiBurner
{
    internal class ImapiMediaChecker
    {
        public bool IsBlankMediaReady(string recorderId)
        {
            MsftDiscRecorder2 recorder = new MsftDiscRecorder2();
            recorder.InitializeDiscRecorder(recorderId);

            MsftDiscFormat2Data dataWriter = new MsftDiscFormat2Data();

            if (!dataWriter.IsRecorderSupported(recorder))
                return false;

            dataWriter.Recorder = recorder;

            if (!dataWriter.IsCurrentMediaSupported(recorder))
                return false;

            return dataWriter.MediaHeuristicallyBlank;
        }

    }
}
