using System;
using System.IO;

namespace STO_WORKBENCH.MultiBurner
{
    // Контейнер моделей данных модуля MultiBurner:
    // перечисление статусов задания на запись, модель самого задания
    // и модель пишущего привода (CD/DVD/BD-рекордера).
    internal class Models
    {
        // Статусная модель задания на запись диска.
        // Задание переходит по цепочке: Waiting -> Burning -> Done | Error.
        public enum BurnJobStatus
        {
            Waiting,
            Burning,
            Done,
            Error
        }

        // Задание на запись: одна выбранная пользователем папка,
        // которую модуль должен записать на диск на одном из приводов.
        public class BurnJob
        {
            // Полный путь к папке с данными для записи (источник).
            public string FolderPath { get; set; }

            // Имя папки (без пути) — отображается в таблице заданий
            // и используется как имя тома записываемого диска.
            public string FolderName
            {
                get { return Path.GetFileName(FolderPath); }
            }

            // Текущий статус задания (Waiting/Burning/Done/Error).
            public BurnJobStatus Status { get; set; }

            // Отображаемое имя привода, которому назначено задание.
            // Заполняется рабочим потоком непосредственно перед записью.
            public string AssignedDrive { get; set; }

            // Текст ошибки записи, если Status == Error.
            public string ErrorMessage { get; set; }
        }

        // Модель пишущего привода, найденного через IMAPI2.
        // RecorderId — уникальный идентификатор рекордера,
        // который нужен всем вызовам IMAPI2.
        public class DriveInfoModel
        {
            // Уникальный идентификатор рекордера IMAPI2.
            // Его передают в InitializeDiscRecorder и в BurnWorker/BurnService.
            public string RecorderId { get; set; }

            // Человекочитаемое имя привода: "Vendor Product Revision (D:, E:)".
            // Именно оно показывается пользователю в списке приводов.
            public string DisplayName { get; set; }

            // При приведении модели к строке возвращаем отображаемое имя —
            // так CheckedListBox показывает привод без лишнего кода.
            public override string ToString()
            {
                return DisplayName;
            }
        }

    }
}
