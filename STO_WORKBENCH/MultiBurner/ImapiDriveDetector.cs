using IMAPI2;
using System;
using System.Collections.Generic;
using static STO_WORKBENCH.MultiBurner.Models;

namespace STO_WORKBENCH.MultiBurner
{
    // Детектор пишущих приводов через IMAPI2.
    // Перебирает все зарегистрированные в системе дисководы-рекордеры
    // (CD-R/RW, DVD±R, BD-R и т.д.) и формирует человекочитаемые
    // описания для показа пользователю.
    internal class ImapiDriveDetector
    {
        // Возвращает список всех найденных пишущих приводов.
        // Каждый элемент — DriveInfoModel с RecorderId (для вызовов IMAPI2)
        // и DisplayName (для отображения пользователю).
        // Выбрасывает исключение, если среда IMAPI2 недоступна.
        public static List<DriveInfoModel> GetRecorders()
        {
            var result = new List<DriveInfoModel>();

            // Корневой объект IMAPI2 — перечислитель дисководов.
            MsftDiscMaster2 discMaster = new MsftDiscMaster2();

            // Без поддерживаемой среды IMAPI2 (старая ОС, отключенный сервис)
            // дальше работать нет смысла — сразу сообщаем об ошибке.
            if (!discMaster.IsSupportedEnvironment)
                throw new Exception("IMAPI2 не поддерживается на этой системе.");

            // Перебираем идентификаторы всех пишущих приводов системы.
            foreach (string recorderId in discMaster)
            {
                // Создаем рекордер и привязываем его к найденному приводу.
                MsftDiscRecorder2 recorder = new MsftDiscRecorder2();
                recorder.InitializeDiscRecorder(recorderId);

                // Собираем описание привода: производитель, модель, ревизия прошивки.
                string vendor = SafeTrim(recorder.VendorId);
                string product = SafeTrim(recorder.ProductId);
                string revision = SafeTrim(recorder.ProductRevision);

                // Буквы дисководов (например "D:\") — COM-свойство может
                // не существовать или выбросить исключение, поэтому в try/catch.
                string volumePaths = "";

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
                    volumePaths = "";
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

        // Безопасная обрезка строки: null превращается в пустую строку,
        // иначе — обычный Trim(). Защита от null из COM-свойств IMAPI2.
        private static string SafeTrim(string value)
        {
            return value == null ? "" : value.Trim();
        }

    }
}
