using System.Collections.Concurrent;

namespace STO_WORKBENCH.Web.Services;

/// <summary>
/// Реестр сгенерированных файлов для скачивания через /download/{id}.
/// Заменяет SaveFileDialog WinForms-версии: файл отдаётся в браузер как вложение.
/// </summary>
public class DownloadRegistry
{
    private readonly ConcurrentDictionary<string, DownloadEntry> _entries = new();
    private readonly TimeSpan _ttl = TimeSpan.FromMinutes(30);

    public string Add(byte[] bytes, string fileName, string contentType)
    {
        var id = Guid.NewGuid().ToString("N");
        _entries[id] = new DownloadEntry(bytes, fileName, contentType, DateTime.UtcNow);
        Cleanup();
        return id;
    }

    public DownloadEntry? Get(string id)
    {
        if (_entries.TryGetValue(id, out var entry))
            return entry;
        return null;
    }

    private void Cleanup()
    {
        var cutoff = DateTime.UtcNow - _ttl;
        foreach (var kv in _entries)
        {
            if (kv.Value.CreatedUtc < cutoff)
                _entries.TryRemove(kv.Key, out _);
        }
    }
}

public class DownloadEntry
{
    public DownloadEntry(byte[] bytes, string fileName, string contentType, DateTime createdUtc)
    {
        Bytes = bytes;
        FileName = fileName;
        ContentType = contentType;
        CreatedUtc = createdUtc;
    }

    public byte[] Bytes { get; }
    public string FileName { get; }
    public string ContentType { get; }
    public DateTime CreatedUtc { get; }
}
