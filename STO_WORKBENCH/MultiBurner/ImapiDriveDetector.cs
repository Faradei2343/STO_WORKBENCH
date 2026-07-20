using IMAPI2;
using System;
using System.Collections.Generic;
using static STO_WORKBENCH.MultiBurner.Models;

namespace STO_WORKBENCH.MultiBurner
{
    internal class ImapiDriveDetector
    {
        public static List<DriveInfoModel> GetRecorders()
        {
            var result = new List<DriveInfoModel>();

            MsftDiscMaster2 discMaster = new MsftDiscMaster2();

            if (!discMaster.IsSupportedEnvironment)
                throw new Exception("IMAPI2 не поддерживается на этой системе.");

            foreach (string recorderId in discMaster)
            {
                MsftDiscRecorder2 recorder = new MsftDiscRecorder2();
                recorder.InitializeDiscRecorder(recorderId);

                string vendor = SafeTrim(recorder.VendorId);
                string product = SafeTrim(recorder.ProductId);
                string revision = SafeTrim(recorder.ProductRevision);

                string volumePaths = "";

                try
                {
                    object pathsObj = recorder.VolumePathNames;

                    if (pathsObj is string[] paths && paths.Length > 0)
                    {
                        volumePaths = string.Join(", ", paths);
                    }
                }
                catch
                {
                    volumePaths = "";
                }

                string display = string.Format(
                    "{0} {1} {2} {3}",
                    vendor,
                    product,
                    revision,
                    volumePaths).Trim();

                if (string.IsNullOrWhiteSpace(display))
                    display = recorderId;

                result.Add(new DriveInfoModel
                {
                    RecorderId = recorderId,
                    DisplayName = display
                });
            }

            return result;
        }

        private static string SafeTrim(string value)
        {
            return value == null ? "" : value.Trim();
        }

    }
}
