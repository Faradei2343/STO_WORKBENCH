using System;
using System.IO;
using IMAPI2;
using IMAPI2FS;

namespace STO_WORKBENCH.MultiBurner
{
    internal class ImapiBurnService
    {
        public void Eject(string recorderId)
        {
            MsftDiscRecorder2 recorder = new MsftDiscRecorder2();
            recorder.InitializeDiscRecorder(recorderId);
            recorder.EjectMedia();
        }

        public void CloseTray(string recorderId)
        {
            MsftDiscRecorder2 recorder = new MsftDiscRecorder2();
            recorder.InitializeDiscRecorder(recorderId);
            recorder.CloseTray();
        }

        public void BurnFolderToDisc(
            string recorderId,
            string folderPath,
            string volumeName,
            bool includeRootFolder)
        {
            if (!Directory.Exists(folderPath))
                throw new DirectoryNotFoundException(folderPath);

            MsftDiscRecorder2 recorder = null;
            MsftDiscFormat2Data dataWriter = null;
            MsftFileSystemImage fileSystemImage = null;

            try
            {
                recorder = new MsftDiscRecorder2();
                recorder.InitializeDiscRecorder(recorderId);

                dataWriter = new MsftDiscFormat2Data();

                if (!dataWriter.IsRecorderSupported(recorder))
                    throw new Exception("Привод не поддерживается IMAPI2.");

                dataWriter.Recorder = recorder;
                dataWriter.ClientName = "STO_WORKBENCH MultiDriveBurner";

                if (!dataWriter.IsCurrentMediaSupported(recorder))
                    throw new Exception("Диск не вставлен или не поддерживается.");

                if (!dataWriter.MediaHeuristicallyBlank)
                    throw new Exception("Диск не пустой.");

                fileSystemImage = new MsftFileSystemImage();

                /*
                 * ВАЖНО:
                 * Здесь используется dynamic, потому что COM interop-типы
                 * IMAPI2.MsftDiscRecorder2 и IMAPI2FS.IDiscRecorder2
                 * в C# могут считаться несовместимыми.
                 */
                dynamic dynFileSystemImage = fileSystemImage;
                dynFileSystemImage.ChooseImageDefaults(recorder);

                fileSystemImage.FileSystemsToCreate =
                    FsiFileSystems.FsiFileSystemISO9660 |
                    FsiFileSystems.FsiFileSystemJoliet |
                    FsiFileSystems.FsiFileSystemUDF;

                fileSystemImage.VolumeName = MakeSafeVolumeName(volumeName);

                IFsiDirectoryItem root = fileSystemImage.Root;

                /*
                 * AddTree умеет сам добавлять дерево папки.
                 *
                 * includeRootFolder = true:
                 * Диск:
                 *   ПАПКА1\
                 *      file1.txt
                 *
                 * includeRootFolder = false:
                 * Диск:
                 *   file1.txt
                 *   subfolder\
                 */
                root.AddTree(folderPath, includeRootFolder);

                IFileSystemImageResult image = fileSystemImage.CreateResultImage();

                dataWriter.ForceMediaToBeClosed = true;

                /*
                 * Здесь тоже dynamic из-за несовместимости:
                 * IMAPI2FS.FsiStream и IMAPI2.IStream.
                 */
                dynamic dynDataWriter = dataWriter;
                dynDataWriter.Write(image.ImageStream);
            }
            catch
            {
                throw;
            }
        }

        private string MakeSafeVolumeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                name = "DISC";

            foreach (char c in Path.GetInvalidFileNameChars())
            {
                name = name.Replace(c, '_');
            }

            name = name.Replace(' ', '_');

            /*
             * ISO9660/Joliet/UDF имеют ограничения по имени тома.
             * 32 символа — безопасный минимум.
             */
            if (name.Length > 32)
                name = name.Substring(0, 32);

            return name;
        }

        public static long GetDirectorySize(string path)
        {
            long size = 0;

            if (!Directory.Exists(path))
                return 0;

            foreach (string file in Directory.GetFiles(path, "*", SearchOption.AllDirectories))
            {
                try
                {
                    FileInfo fi = new FileInfo(file);
                    size += fi.Length;
                }
                catch
                {
                    // файл может быть недоступен, пропускаем
                }
            }

            return size;
        }

    }
}
