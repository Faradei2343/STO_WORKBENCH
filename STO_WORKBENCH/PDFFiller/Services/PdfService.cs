using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using STO_WORKBENCH.Core;
using Spire.Pdf;
using Spire.Pdf.Texts;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.util;

using iTextFont = iTextSharp.text.Font;
using iTextRectangle = iTextSharp.text.Rectangle;
using SpirePdfDocument = Spire.Pdf.PdfDocument;
using iTextPdfTextExtractor = iTextSharp.text.pdf.parser.PdfTextExtractor;

namespace STO_WORKBENCH.PDFFiller.Services
{
    public class PdfService : IPdfService
    {
        private readonly IMessageService _messageService;
        private const string MarkerText = "(ФИО)";
        private const int DefaultMark = 5;

        public PdfService(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public PointF? GetMarkerCoordinates(string filePath)
        {
            try
            {
                using var doc = new SpirePdfDocument();
                doc.LoadFromFile(filePath);

                foreach (PdfPageBase page in doc.Pages)
                {
                    var finder = new PdfTextFinder(page)
                    {
                        Options = new PdfTextFindOptions
                        {
                            Parameter = TextFindParameter.IgnoreCase
                        }
                    };

                    var fragments = finder.Find(MarkerText);
                    if (fragments.Count > 0)
                    {
                        return fragments[0].Positions[0];
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                _messageService.ShowError($"Ошибка поиска маркера в PDF: {ex.Message}");
                return null;
            }
        }

        public DateTime? GetControlDate(string filePath)
        {
            return ExtractDateFromRegion(filePath, new RectangleJ(115, 620, 70, 25));
        }

        public DateTime? GetReceiptDate(string filePath)
        {
            return ExtractDateFromRegion(filePath, new RectangleJ(170, 650, 40, 25));
        }

        private DateTime? ExtractDateFromRegion(string filePath, RectangleJ rect)
        {
            try
            {
                using var reader = new PdfReader(filePath);
                var filter = new RenderFilter[] { new RegionTextRenderFilter(rect) };
                var strategy = new FilteredTextRenderListener(new LocationTextExtractionStrategy(), filter);
                var text = iTextPdfTextExtractor.GetTextFromPage(reader, 1, strategy);

                if (DateTime.TryParse(text, out var result))
                    return result;

                return null;
            }
            catch (Exception ex)
            {
                _messageService.ShowError($"Не удалось прочитать дату из файла.\n" +
                    $"Возможно, выбран не тот документ или документ уже был отсканирован.\n" +
                    $"Системное сообщение: {ex.Message}");
                return null;
            }
        }

        private ApplicantInfo? ExtractApplicantFromPdf(string filePath)
        {
            try
            {
                using var reader = new PdfReader(filePath);
                var rect = new RectangleJ(90, 720, 50, 65);
                var filter = new RenderFilter[] { new RegionTextRenderFilter(rect) };
                var strategy = new FilteredTextRenderListener(new LocationTextExtractionStrategy(), filter);
                var text = iTextPdfTextExtractor.GetTextFromPage(reader, 1, strategy);

                var fioString = text.Replace("\n", " ");
                var parts = fioString.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length >= 3)
                {
                    return new ApplicantInfo
                    {
                        LastName = parts[0],
                        FirstName = parts[1],
                        Patronymic = parts[2]
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                _messageService.ShowError($"Не удалось прочитать ФИО из файла: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> ProcessPdfAsync(string fileName, AppSettings settings, PdfProcessingOptions options)
        {
            return await Task.Run(() => ProcessPdf(fileName, settings, options));
        }

        private bool ProcessPdf(string fileName, AppSettings settings, PdfProcessingOptions options)
        {
            var inputPath = System.IO.Path.Combine(settings.InputPath, fileName);
            var outputPath = System.IO.Path.Combine(settings.OutputPath, fileName);

            var coordinates = GetMarkerCoordinates(inputPath);
            if (!coordinates.HasValue)
            {
                _messageService.ShowError($"Не найден маркер в файле: {fileName}");
                return false;
            }

            var point = Point.Round(coordinates.Value);

            try
            {
                using var reader = new PdfReader(inputPath);
                using var outputStream = new FileStream(outputPath, FileMode.Create);
                using var stamper = new PdfStamper(reader, outputStream);

                reader.SelectPages("1-2");
                var pageSize = reader.GetPageSize(1);
                var contentByte = stamper.GetOverContent(1);

                var baseFont = CreateBaseFont();

                // Добавление ФИО исполнителя
                AddExecutorName(point, contentByte, pageSize, settings, baseFont);

                // Добавление оценки
                AddMark(point, contentByte, pageSize, baseFont);

                // Обработка даты
                if (!ProcessDate(point, contentByte, pageSize, inputPath, options, baseFont, fileName))
                {
                    return false;
                }

                // Обработка заявителя
                ProcessApplicant(point, contentByte, pageSize, inputPath, options, baseFont);

                return true;
            }
            catch (Exception ex)
            {
                _messageService.ShowError($"Ошибка обработки файла {fileName}: {ex.Message}");
                return false;
            }
        }

        private BaseFont CreateBaseFont()
        {
            var fontPath = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Fonts),
                "ARIAL.TTF");
            return BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        }

        private void AddExecutorName(Point coordinates, PdfContentByte content,
            iTextRectangle pageSize, AppSettings settings, BaseFont baseFont)
        {
            var font = new iTextFont(baseFont, 9, iTextFont.NORMAL);
            int y = (int)(pageSize.Height - coordinates.Y);
            var text = settings.GetExecutorShortName();

            ColumnText.ShowTextAligned(content, Element.ALIGN_CENTER,
                new Phrase(text, font), coordinates.X - 8, y + 7, 0);
        }

        private void AddMark(Point coordinates, PdfContentByte content,
            iTextRectangle pageSize, BaseFont baseFont)
        {
            var font = new iTextFont(baseFont, 12, iTextFont.NORMAL);
            int y = (int)(pageSize.Height - coordinates.Y);

            ColumnText.ShowTextAligned(content, Element.ALIGN_CENTER,
                new Phrase(DefaultMark.ToString(), font), coordinates.X + 215, y, 0);
        }

        private bool ProcessDate(Point coordinates, PdfContentByte content,
            iTextRectangle pageSize, string inputPath,
            PdfProcessingOptions options, BaseFont baseFont, string fileName)
        {
            var font = new iTextFont(baseFont, 20, iTextFont.NORMAL);
            int y = (int)(pageSize.Height - coordinates.Y);

            if (options.FillOnlyMonthYear)
            {
                AddMonthYearOnly(coordinates, content, y, font);
                return true;
            }

            DateTime dateToUse;

            if (options.UseCustomDate && options.CustomDateTime.HasValue)
            {
                dateToUse = options.CustomDateTime.Value;
                var controlDate = GetControlDate(inputPath);

                if (controlDate.HasValue && dateToUse > controlDate.Value)
                {
                    var receiptDate = GetReceiptDate(inputPath);
                    var message = $"Выбранная дата превышает контрольную!\n" +
                        $"Имя СЛ: {fileName}\n" +
                        $"Контрольная дата: {controlDate.Value}\n" +
                        $"Дата поступления заявки: {receiptDate}\n" +
                        $"Да - Продолжить, Нет - Заполнить задним числом";

                    if (_messageService.ShowYesNo(message) == System.Windows.Forms.DialogResult.No)
                    {
                        dateToUse = controlDate.Value.AddHours(-1);
                    }
                }
            }
            else
            {
                var receiptDate = GetReceiptDate(inputPath);
                var controlDate = GetControlDate(inputPath);

                if (!receiptDate.HasValue || !controlDate.HasValue)
                    return false;

                dateToUse = DateTime.Now;
                if (dateToUse > controlDate.Value)
                {
                    dateToUse = controlDate.Value.AddHours(-1);
                }
            }

            AddFullDateTime(coordinates, content, y, font, dateToUse);
            return true;
        }

        private void AddMonthYearOnly(Point coordinates, PdfContentByte content, int y, iTextFont font)
        {
            var now = DateTime.Now;
            var month = now.ToString("MM");
            var year = now.ToString("yyyy");

            float x = coordinates.X;
            float yPos = y + 52;

            ColumnText.ShowTextAligned(content, Element.ALIGN_CENTER,
                new Phrase(month[0].ToString(), font), x + 259, yPos, 0);
            ColumnText.ShowTextAligned(content, Element.ALIGN_CENTER,
                new Phrase(month[1].ToString(), font), x + 279, yPos, 0);
            ColumnText.ShowTextAligned(content, Element.ALIGN_CENTER,
                new Phrase(year[3].ToString(), font), x + 368, yPos, 0);
        }

        private void AddFullDateTime(Point coordinates, PdfContentByte content,
            int y, iTextFont font, DateTime dateTime)
        {
            var day = dateTime.ToString("dd");
            var month = dateTime.ToString("MM");
            var year = dateTime.ToString("yyyy");
            var hours = dateTime.ToString("HH");
            var minutes = dateTime.ToString("mm");

            float x = coordinates.X;
            float yPos = y + 52;

            // День
            ColumnText.ShowTextAligned(content, Element.ALIGN_CENTER,
                new Phrase(day[0].ToString(), font), x + 211, yPos, 0);
            ColumnText.ShowTextAligned(content, Element.ALIGN_CENTER,
                new Phrase(day[1].ToString(), font), x + 231, yPos, 0);

            // Месяц
            ColumnText.ShowTextAligned(content, Element.ALIGN_CENTER,
                new Phrase(month[0].ToString(), font), x + 259, yPos, 0);
            ColumnText.ShowTextAligned(content, Element.ALIGN_CENTER,
                new Phrase(month[1].ToString(), font), x + 279, yPos, 0);

            // Год (только последняя цифра)
            ColumnText.ShowTextAligned(content, Element.ALIGN_CENTER,
                new Phrase(year[3].ToString(), font), x + 368, yPos, 0);

            // Часы
            ColumnText.ShowTextAligned(content, Element.ALIGN_CENTER,
                new Phrase(hours[0].ToString(), font), x + 398, yPos, 0);
            ColumnText.ShowTextAligned(content, Element.ALIGN_CENTER,
                new Phrase(hours[1].ToString(), font), x + 416, yPos, 0);

            // Минуты
            ColumnText.ShowTextAligned(content, Element.ALIGN_CENTER,
                new Phrase(minutes[0].ToString(), font), x + 448, yPos, 0);
            ColumnText.ShowTextAligned(content, Element.ALIGN_CENTER,
                new Phrase(minutes[1].ToString(), font), x + 465, yPos, 0);
        }

        private void ProcessApplicant(Point coordinates, PdfContentByte content,
            iTextRectangle pageSize, string inputPath,
            PdfProcessingOptions options, BaseFont baseFont)
        {
            var font = new iTextFont(baseFont, 9, iTextFont.NORMAL);
            int y = (int)(pageSize.Height - coordinates.Y);

            ApplicantInfo? applicant;

            if (options.UseCustomApplicant && options.CustomApplicant?.IsValid() == true)
            {
                applicant = options.CustomApplicant;
            }
            else
            {
                applicant = ExtractApplicantFromPdf(inputPath);
            }

            if (applicant?.IsValid() == true)
            {
                ColumnText.ShowTextAligned(content, Element.ALIGN_CENTER,
                    new Phrase(applicant.GetShortName(), font), coordinates.X + 285, y + 7, 0);
            }
        }
    }
}
