using System;

namespace STO_WORKBENCH.Core
{
    // Простой статический логгер: сам ничего никуда не записывает, а лишь рассылает
    // сообщения всем подписчикам через событие OnMessage. Формы подписываются на это
    // событие и выводят текст в свои RichTextBox-логи.
    public static class Logger
    {
        // Событие, вызываемое при каждом логировании; аргумент — текст сообщения
        public static event Action<string> OnMessage;

        // Отправляет сообщение всем подписчикам события OnMessage.
        // Оператор ?. защищает от NullReferenceException, когда подписчиков нет.
        public static void Log(string message)
        {
            OnMessage?.Invoke(message);
        }

        // Логирует сообщение с пометкой об ошибке: "[ОШИБКА] ..."
        public static void LogError(string message)
        {
            Log($"[ОШИБКА] {message}");
        }

        // Логирует сообщение об успешном завершении операции: "[УСПЕХ] ..."
        public static void LogSuccess(string message)
        {
            Log($"[УСПЕХ] {message}");
        }

        // Логирует информационное сообщение: "[INFO] ..."
        public static void LogInfo(string message)
        {
            Log($"[INFO] {message}");
        }
    }
}