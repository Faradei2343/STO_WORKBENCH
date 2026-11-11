using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Spire.Pdf;
using Spire.Pdf.Texts;
using System.Drawing;
using System.Text;
using System.util;

// Псевдонимы типов: в проекте одновременно используются две PDF-библиотеки,
// и их одноимённые классы (Font, Rectangle, PdfDocument, PdfTextExtractor) конфликтуют
// с типами System.Drawing и друг с другом.
using iTextFont = iTextSharp.text.Font;
using iTextRectangle = iTextSharp.text.Rectangle;
using SpirePdfDocument = Spire.Pdf.PdfDocument;
using iTextPdfTextExtractor = iTextSharp.text.pdf.parser.PdfTextExtractor;

namespace STO_WORKBENCH.Web.Services;

// Сервис обработки PDF-документов (сервисных листов) — порт PdfService из WinForms-версии
// (STO_WORKBENCH/PDFFiller/Services/PdfService.cs). Логика наложения надписей перенесена 1:1.
// Отличия от десктопа: вместо IMessageService используются делегаты — log (строки лога,
// "ОШИБКА: ..." для ошибок, "!!...!!" для предупреждений) и status (текущий обрабатываемый файл);
// модальный диалог ShowYesNo «Выбранная дата превышает контрольную» заменён фиксированной
// политикой: пишется предупреждение в лог и обработка продолжается с выбранной датой.
//
// Используются две библиотеки: Spire.PDF — только для поиска маркера "(ФИО)" (поиск текста
// с координатами фрагментов), iTextSharp — для чтения текста из областей страницы и штамповки.
// Системы координат различаются: Spire отсчитывает Y сверху вниз, iTextSharp — снизу вверх,
// поэтому перед наложением текста выполняется пересчёт Y := pageSize.Height - y.
public class PdfService
{
    // Делегат вывода строк в лог операций (вызывается из фонового потока обработки).
    private readonly Action<string> _log;

    // Делегат индикации текущего обрабатываемого файла (аналог statusLabel десктопа).
    private readonly Action<string>? _status;

    // Текст-маркер, по которому в документе ищется якорная точка.
    private const string MarkerText = "(ФИО)";

    // Оценка по умолчанию, которая проставляется в сервисный лист ("5").
    private const int DefaultMark = 5;

    // iTextSharp при чтении TTF (ReadStandardString) запрашивает кодировку windows-1252,
    // которая в современном .NET доступна только через провайдер кодовых страниц
    // (в WinForms-версии она была в составе .NET Framework). Регистрируем до первого вызова.
    static PdfService()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    }

    // log — строки лога; status — текущий обрабатываемый файл (может быть null).
    public PdfService(Action<string> log, Action<string>? status = null)
    {
        _log = log;
        _status = status;
    }

    // Эквивалент IMessageService.ShowError: строка с префиксом "ОШИБКА:" (красная в логе).
    private void ShowError(string message) => _log($"ОШИБКА: {message}");

    // Эквивалент IMessageService.ShowWarning: строка в маркерах "!!...!!".
    private void ShowWarning(string message) => _log($"!!{message}!!");

    // Ищет маркер "(ФИО)" на всех страницах документа и возвращает координаты первого вхождения.
    // Возвращает PointF (система Spire: X вправо, Y вниз) или null, если маркер не найден.
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
            ShowError($"Ошибка поиска маркера в PDF: {ex.Message}");
            return null;
        }
    }

    // Читает контрольную дату из фиксированной области первой страницы.
    public DateTime? GetControlDate(string filePath)
    {
        return ExtractDateFromRegion(filePath, new RectangleJ(115, 620, 70, 25));
    }

    // Читает дату поступления заявки из фиксированной области первой страницы.
    public DateTime? GetReceiptDate(string filePath)
    {
        return ExtractDateFromRegion(filePath, new RectangleJ(170, 650, 40, 25));
    }

    // Универсальный метод извлечения даты из заданной прямоугольной области первой страницы.
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
            // Ошибка чтения обычно означает: выбран не тот документ либо это скан (текстового слоя нет).
            ShowError($"Не удалось прочитать дату из файла.\n" +
                $"Возможно, выбран не тот документ или документ уже был отсканирован.\n" +
                $"Системное сообщение: {ex.Message}");
            return null;
        }
    }

    // Извлекает ФИО заявителя из фиксированной области первой страницы (та же техника, что и у дат).
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
            ShowError($"Не удалось прочитать ФИО из файла: {ex.Message}");
            return null;
        }
    }

    // Основной метод обработки одного сервисного листа (порт ProcessPdf; в десктопе был
    // private с async-обёрткой ProcessPdfAsync — в веб-версии страница оборачивает вызов
    // в Task.Run сама). Возвращает true при успехе; false — при любой ошибке.
    public bool ProcessPdf(string fileName, AppSettings settings, PdfProcessingOptions options)
    {
        var inputPath = System.IO.Path.Combine(settings.InputPath, fileName);
        var outputPath = System.IO.Path.Combine(settings.OutputPath, fileName);

        _status?.Invoke($"Обработка: {fileName}");

        // Шаг 1: поиск якорной точки по маркеру.
        var coordinates = GetMarkerCoordinates(inputPath);
        if (!coordinates.HasValue)
        {
            ShowError($"Не найден маркер в файле: {fileName}");
            return false;
        }

        var point = Point.Round(coordinates.Value);

        try
        {
            // Шаг 2-3: открываем исходник, создаём выходной поток и штамповщик.
            using var reader = new PdfReader(inputPath);
            using var outputStream = new System.IO.FileStream(outputPath, System.IO.FileMode.Create);
            using var stamper = new PdfStamper(reader, outputStream);

            // Оставляем в документе только первые две страницы.
            reader.SelectPages("1-2");
            var pageSize = reader.GetPageSize(1);

            var contentByte = stamper.GetOverContent(1);

            // Шаг 4: базовый шрифт для всех надписей.
            var baseFont = CreateBaseFont();

            // Шаг 5: последовательное наложение надписей.
            AddExecutorName(point, contentByte, pageSize, settings, baseFont);
            AddMark(point, contentByte, pageSize, baseFont);

            if (!ProcessDate(point, contentByte, pageSize, inputPath, options, baseFont, fileName))
            {
                return false;
            }

            ProcessApplicant(point, contentByte, pageSize, inputPath, options, baseFont);

            return true;
        }
        catch (Exception ex)
        {
            ShowError($"Ошибка обработки файла {fileName}: {ex.Message}");
            return false;
        }
    }

    // Создаёт базовый шрифт для надписей: системный ARIAL.TTF, IDENTITY_H (кириллица),
    // NOT_EMBEDDED (шрифт в PDF не встраивается).
    private BaseFont CreateBaseFont()
    {
        var fontPath = System.IO.Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Fonts),
            "ARIAL.TTF");
        return BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
    }

    // Наносит ФИО исполнителя в сокращённом виде. Сдвиги X-8 и Y+7 подогнаны под макет формы.
    private void AddExecutorName(Point coordinates, PdfContentByte content,
        iTextRectangle pageSize, AppSettings settings, BaseFont baseFont)
    {
        var font = new iTextFont(baseFont, 9, iTextFont.NORMAL);
        int y = (int)(pageSize.Height - coordinates.Y);
        var text = settings.GetExecutorShortName();

        ColumnText.ShowTextAligned(content, Element.ALIGN_CENTER,
            new Phrase(text, font), coordinates.X - 8, y + 7, 0);
    }

    // Наносит оценку (DefaultMark = "5"): на одной строке с маркером, смещение +215 по X.
    private void AddMark(Point coordinates, PdfContentByte content,
        iTextRectangle pageSize, BaseFont baseFont)
    {
        var font = new iTextFont(baseFont, 12, iTextFont.NORMAL);
        int y = (int)(pageSize.Height - coordinates.Y);

        ColumnText.ShowTextAligned(content, Element.ALIGN_CENTER,
            new Phrase(DefaultMark.ToString(), font), coordinates.X + 215, y, 0);
    }

    // Определяет и наносит дату/время закрытия заявки.
    // Логика выбора даты:
    //   - опция "только месяц и год" — наносятся только цифры месяца и последняя цифра года;
    //   - "своя дата" — берётся выбранная пользователем дата; если она больше контрольной,
    //     в лог пишется предупреждение и обработка продолжается с выбранной датой
    //     (в десктопе спрашивался вопрос Yes/No, где "Нет" означало "задним числом");
    //   - иначе — берётся текущее время, но если оно больше контрольной даты,
    //     автоматически проставляется контрольная минус 1 час;
    //   - если поступившая или контрольная дата прочитать не удалось — false.
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
                // Вместо модального диалога ShowYesNo — фиксированная политика: продолжаем
                // с выбранной датой (соответствует ответу "Да" в десктопной версии).
                ShowWarning("Выбранная дата превышает контрольную. Используется выбранная дата");
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

    // Наносит только цифры месяца и последнюю цифру текущего года (режим "только месяц и год").
    // Смещения +259/+279/+368 по X подогнаны под макет формы.
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

    // Наносит полную дату и время: день, месяц, последнюю цифру года, часы и минуты.
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

    // Наносит ФИО заявителя (в сокращённом виде): пользовательский вариант приоритетнее,
    // иначе ФИО извлекается из самого документа. Некорректное ФИО не наносится.
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
