namespace STO_WORKBENCH.Web.Services;

// Модели данных модуля мультизаписи: статусы задания, само задание
// и модель пишущего привода (CD/DVD/BD-рекордера).
public class Models
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
        public string FolderPath { get; set; } = string.Empty;

        // Имя папки (без пути) — отображается в таблице заданий
        // и используется как имя тома записываемого диска.
        public string FolderName => Path.GetFileName(FolderPath);

        // Текущий статус задания (Waiting/Burning/Done/Error).
        public BurnJobStatus Status { get; set; }

        // Отображаемое имя привода, которому назначено задание.
        // Заполняется рабочим потоком непосредственно перед записью.
        public string? AssignedDrive { get; set; }

        // Текст ошибки записи, если Status == Error.
        public string? ErrorMessage { get; set; }

        // Выбрана ли строка в таблице заданий (чекбокс «Выбрать»).
        // В десктопе это было значением ячейки DataGridView.
        public bool Selected { get; set; } = true;

        // Отправлено ли задание в очередь текущего сеанса записи
        // (используется для показа статуса «В очереди»).
        public bool Queued { get; set; }

        // Отформатированный размер папки для показа в таблице.
        public string SizeText { get; set; } = "н/д";
    }

    // Модель пишущего привода, найденного через IMAPI2.
    // RecorderId — уникальный идентификатор рекордера,
    // который нужен всем вызовам IMAPI2.
    public class DriveInfoModel
    {
        // Уникальный идентификатор рекордера IMAPI2.
        // Его передают в InitializeDiscRecorder и в BurnWorker/BurnService.
        public string RecorderId { get; set; } = string.Empty;

        // Человекочитаемое имя привода: "Vendor Product Revision (D:, E:)".
        public string DisplayName { get; set; } = string.Empty;

        // Отмечен ли привод в списке выбора.
        public bool Selected { get; set; } = true;

        public override string ToString() => DisplayName;
    }
}
