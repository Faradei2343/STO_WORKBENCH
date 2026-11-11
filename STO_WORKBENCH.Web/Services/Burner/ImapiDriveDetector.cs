using System.Runtime.InteropServices;
using static STO_WORKBENCH.Web.Services.Models;

namespace STO_WORKBENCH.Web.Services;

// Детектор пишущих приводов через IMAPI2.
// Перебирает все зарегистрированные в системе дисководы-рекордеры
// (CD-R/RW, DVD±R, BD-R и т.д.) и формирует человекочитаемые
// описания для показа пользователю.
// COM-вызовы — только внутри STA-потока (StaComRunner).
public static class ImapiDriveDetector
{
    // IMAPI2.MsftDiscMaster2 перечисляет рекордеры через IEnumVARIANT,
    // поэтому перебор делаем вручную (late binding не умеет For Each).
    [ComImport]
    [Guid("00020404-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IEnumVARIANT
    {
        [PreserveSig]
        int Next(
            uint celt,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0, ArraySubType = UnmanagedType.Struct)]
            object[] rgelt,
            IntPtr pceltFetched);

        void Skip(uint celt);
        void Reset();

        [return: MarshalAs(UnmanagedType.Interface)]
        IEnumVARIANT Clone();
    }

    // Возвращает список всех найденных пишущих приводов.
    // Выбрасывает исключение, если среда IMAPI2 недоступна.
    public static List<DriveInfoModel> GetRecorders()
    {
        var result = new List<DriveInfoModel>();

        // Корневой объект IMAPI2 — перечислитель дисководов.
        dynamic discMaster = ComInterop.Create(ComInterop.ImapiDiscMaster2);

        // Без поддерживаемой среды IMAPI2 (старая ОС, отключенный сервис)
        // дальше работать нет смысла — сразу сообщаем об ошибке.
        if (!(bool)discMaster.IsSupportedEnvironment)
            throw new Exception("IMAPI2 не поддерживается на этой системе.");

        var enumerator = (IEnumVARIANT)(object)discMaster;
        var items = new object[1];

        // Перебираем идентификаторы всех пишущих приводов системы.
        while (enumerator.Next(1, items, IntPtr.Zero) == 0)
        {
            string recorderId = items[0]?.ToString() ?? string.Empty;
            if (string.IsNullOrEmpty(recorderId))
                continue;

            // Создаем рекордер и привязываем его к найденному приводу.
            dynamic recorder = ComInterop.Create(ComInterop.ImapiDiscRecorder2);
            recorder.InitializeDiscRecorder(recorderId);

            // Собираем описание привода: производитель, модель, ревизия прошивки.
            string vendor = SafeTrim(recorder.VendorId);
            string product = SafeTrim(recorder.ProductId);
            string revision = SafeTrim(recorder.ProductRevision);

            // Буквы дисководов (например "D:\") — COM-свойство может
            // не существовать или выбросить исключение, поэтому в try/catch.
            string volumePaths = string.Empty;

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
                volumePaths = string.Empty;
            }

            // Склеиваем все части в одну строку.
            string display = string.Format(
                "{0} {1} {2} {3}",
                vendor,
                product,
                revision,
                volumePaths).Trim();

            // Если ничего осмысленного не получилось,
            // показываем хотя бы сырой идентификатор.
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

    // Безопасная обрезка строки: null превращается в пустую строку.
    private static string SafeTrim(string? value)
    {
        return value == null ? string.Empty : value.Trim();
    }
}
