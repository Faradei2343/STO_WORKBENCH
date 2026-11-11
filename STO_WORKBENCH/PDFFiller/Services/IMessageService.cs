using System;
using System.Collections.Generic;
using System.Text;

namespace STO_WORKBENCH.PDFFiller.Services
{
    // Интерфейс сервиса диалоговых окон с пользователем.
    // Абстрагирует вызовы MessageBox, чтобы формы не зависели от конкретной реализации
    // и чтобы показ сообщений можно было подменять (например, в тестах).
    // Реализация — MessageService.
    public interface IMessageService
    {
        // Показывает модальное окно с сообщением об ошибке (иконка Error, одна кнопка OK).
        // message — текст ошибки, выводимый пользователю.
        void ShowError(string message);

        // Показывает модальное окно с предупреждением (иконка Warning, кнопка OK).
        // Используется для невалидных настроек, отсутствующих папок/файлов и т.п.
        // message — текст предупреждения.
        void ShowWarning(string message);

        // Показывает модальное окно с сообщением об успешном завершении операции
        // (иконка Information, кнопка OK).
        // message — текст подтверждения.
        void ShowSuccess(string message);

        // Показывает модальный диалог подтверждения с кнопками "Да"/"Нет" (иконка Warning).
        // message — текст вопроса.
        // Возвращает DialogResult.Yes или DialogResult.No — выбор пользователя,
        // на основе которого вызывающий код решает, как продолжить выполнение.
        DialogResult ShowYesNo(string message);
    }
}
