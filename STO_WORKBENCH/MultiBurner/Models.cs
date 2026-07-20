using System;
using System.IO;

namespace STO_WORKBENCH.MultiBurner
{
    internal class Models
    {
        public enum BurnJobStatus
        {
            Waiting,
            Burning,
            Done,
            Error
        }

        public class BurnJob
        {
            public string FolderPath { get; set; }

            public string FolderName
            {
                get { return Path.GetFileName(FolderPath); }
            }

            public BurnJobStatus Status { get; set; }

            public string AssignedDrive { get; set; }

            public string ErrorMessage { get; set; }
        }

        public class DriveInfoModel
        {
            public string RecorderId { get; set; }

            public string DisplayName { get; set; }

            public override string ToString()
            {
                return DisplayName;
            }
        }

    }
}
