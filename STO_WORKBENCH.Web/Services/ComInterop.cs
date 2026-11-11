namespace STO_WORKBENCH.Web.Services;

/// <summary>
/// Создание COM-объектов по ProgID без interop-сборок (late binding).
/// Используется для Excel, Outlook и IMAPI2.
/// </summary>
public static class ComInterop
{
    public const string ExcelApplication = "Excel.Application";
    public const string OutlookApplication = "Outlook.Application";
    public const string ImapiDiscMaster2 = "IMAPI2.MsftDiscMaster2";
    public const string ImapiDiscRecorder2 = "IMAPI2.MsftDiscRecorder2";
    public const string ImapiDiscFormat2Data = "IMAPI2.MsftDiscFormat2Data";
    public const string ImapiFileSystemImage = "IMAPI2FS.MsftFileSystemImage";

    public static dynamic Create(string progId)
    {
        Type? type = Type.GetTypeFromProgID(progId);
        if (type is null)
            throw new InvalidOperationException($"COM-класс '{progId}' не зарегистрирован на этом компьютере.");

        return Activator.CreateInstance(type)
            ?? throw new InvalidOperationException($"Не удалось создать COM-объект '{progId}'.");
    }
}
