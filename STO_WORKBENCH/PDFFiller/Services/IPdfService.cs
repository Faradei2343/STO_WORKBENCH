using STO_WORKBENCH.Core;

namespace STO_WORKBENCH.PDFFiller.Services
{
    public interface IPdfService
    {
        Task<bool> ProcessPdfAsync(string fileName, AppSettings settings, PdfProcessingOptions options);
        DateTime? GetReceiptDate(string filePath);
        DateTime? GetControlDate(string filePath);
        PointF? GetMarkerCoordinates(string filePath);
    }
}
