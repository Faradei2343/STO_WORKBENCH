using System;
using System.IO;
using System.Text;

namespace STO_WORKBENCH.MultiBurner
{
    internal class LogService
    {
        private static readonly object _lock = new object();

        private static string LogDirectory
        {
            get
            {
                string dir = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                    "MultiDriveBurner",
                    "logs");

                Directory.CreateDirectory(dir);
                return dir;
            }
        }

        public static void Write(string message)
        {
            lock (_lock)
            {
                string file = Path.Combine(LogDirectory, DateTime.Now.ToString("yyyy-MM-dd") + ".log");

                string line = string.Format(
                    "{0:yyyy-MM-dd HH:mm:ss}; {1}",
                    DateTime.Now,
                    message);

                File.AppendAllText(file, line + Environment.NewLine, Encoding.UTF8);
            }
        }

    }
}
