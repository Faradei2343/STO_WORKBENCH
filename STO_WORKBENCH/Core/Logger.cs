using System;

namespace STO_WORKBENCH.Core
{
    public static class Logger
    {
        public static event Action<string> OnMessage;

        public static void Log(string message)
        {
            OnMessage?.Invoke(message);
        }

        public static void LogError(string message)
        {
            Log($"[ОШИБКА] {message}");
        }

        public static void LogSuccess(string message)
        {
            Log($"[УСПЕХ] {message}");
        }

        public static void LogInfo(string message)
        {
            Log($"[INFO] {message}");
        }
    }
}