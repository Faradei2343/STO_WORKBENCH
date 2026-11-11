
namespace STO_WORKBENCH.PDFFiller.Services
{
    // Реализация IMessageService: сервис показа стандартных модальных окон MessageBox
    // с русскоязычными заголовками ("Ошибка", "Внимание", "Успех").
    // Все методы блокирующие и должны вызываться из UI-потока.
    public class MessageService : IMessageService
    {
        // Показывает окно ошибки: заголовок "Ошибка", иконка Error, одна кнопка OK.
        // message — текст ошибки.
        public void ShowError(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Показывает окно предупреждения: заголовок "Внимание", иконка Warning, одна кнопка OK.
        // message — текст предупреждения.
        public void ShowWarning(string message)
        {
            MessageBox.Show(message, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // Показывает окно успеха: заголовок "Успех", иконка Information, одна кнопка OK.
        // message — текст подтверждения.
        public void ShowSuccess(string message)
        {
            MessageBox.Show(message, "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Показывает диалог подтверждения: заголовок "Внимание", иконка Warning, кнопки Yes/No.
        // message — текст вопроса.
        // Возвращает DialogResult.Yes или DialogResult.No по выбору пользователя.
        public DialogResult ShowYesNo(string message)
        {
            return MessageBox.Show(message, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }
    }
}
