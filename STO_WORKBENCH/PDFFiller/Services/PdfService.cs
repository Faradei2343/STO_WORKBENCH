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

// Псевдонимы типов: в проекте одновременно используются две PDF-библиотеки,
// и их одноимённые классы (Font, Rectangle, PdfDocument, PdfTextExtractor) конфликтуют
// с типами System.Drawing и друг с другом. Через using-алиасы явно указано,
// какой именно тип имеется в виду в каждом месте.
using iTextFont = iTextSharp.text.Font;
using iTextRectangle = iTextSharp.text.Rectangle;
using SpirePdfDocument = Spire.Pdf.PdfDocument;
using iTextPdfTextExtractor = iTextSharp.text.pdf.parser.PdfTextExtractor;

namespace STO_WORKBENCH.PDFFiller.Services
{
    // Сервис обработки PDF-документов (сервисных листов) — ядро модуля PDFFiller.
    // Отвечает за: поиск координат текстового маркера "(ФИО)", чтение дат и ФИО заявителя
    // из фиксированных областей первой страницы, а также наложение на документ новых надписей
    // (ФИО исполнителя, оценка, дата и время, заявитель) с сохранением результата в выходную папку.
    //
    // Используются две библиотеки с разным назначением:
    //   - Spire.PDF — только для поиска маркера (удобный поиск текста с координатами фрагментов);
    //   - iTextSharp — для чтения текста из областей страницы и для штамповки (PdfStamper).
    // Важный нюанс: системы координат библиотек различаются — Spire отсчитывает Y сверху вниз,
    // а iTextSharp — снизу вверх, поэтому перед наложением текста выполняется пересчёт
    // Y := pageSize.Height - y (см. методы AddExecutorName, AddMark и др.).
    //
    // Все публичные методы ловят исключения и показывают пользователю сообщение через
    // IMessageService, возвращая null/false вместо проброса ошибки.
    public class PdfService : IPdfService
    {
        // Сервис сообщений для показа диалогов об ошибках пользователю.
        private readonly IMessageService _messageService;

        // Текст-маркер, по которому в документе ищется якорная точка (все надписи
        // позиционируются относительно координат этого текста).
        private const string MarkerText = "(ФИО)";

        // Оценка по умолчанию, которая проставляется в сервисный лист ("5").
        private const int DefaultMark = 5;

        // Конструктор: получает сервис сообщений (единственная зависимость).
        // messageService — сервис для показа диалогов об ошибках.
        public PdfService(IMessageService messageService)
        {
            _messageService = messageService;
        }

        // Ищет маркер "(ФИО)" на всех страницах документа и возвращает координаты его первого вхождения.
        // filePath — полный путь к PDF-файлу.
        // Возвращает PointF с координатами (в системе Spire: X вправо, Y вниз от верхнего левого угла)
        // или null, если маркер не найден либо произошла ошибка чтения файла.
        public PointF? GetMarkerCoordinates(string filePath)
        {
            try
            {
                // Загружаем документ через Spire.PDF (using — документ освобождён после поиска).
                using var doc = new SpirePdfDocument();
                doc.LoadFromFile(filePath);

                // Перебираем страницы и ищем маркер на каждой из них.
                foreach (PdfPageBase page in doc.Pages)
                {
                    // PdfTextFinder выполняет поиск фрагментов текста на странице;
                    // IgnoreCase — чтобы находить маркер независимо от регистра.
                    var finder = new PdfTextFinder(page)
                    {
                        Options = new PdfTextFindOptions
                        {
                            Parameter = TextFindParameter.IgnoreCase
                        }
                    };

                    // Нашли хотя бы одно вхождение — берём позицию первого символа первого фрагмента.
                    var fragments = finder.Find(MarkerText);
                    if (fragments.Count > 0)
                    {
                        return fragments[0].Positions[0];
                    }
                }

                // Маркер не найден ни на одной странице.
                return null;
            }
            catch (Exception ex)
            {
                _messageService.ShowError($"Ошибка поиска маркера в PDF: {ex.Message}");
                return null;
            }
        }

        // Читает контрольную дату из фиксированной области первой страницы.
        // Область задана в PDF-координатах iTextSharp (начало — нижний левый угол страницы):
        // RectangleJ(x, y, ширина, высота). Вся логика извлечения — в ExtractDateFromRegion.
        // Возвращает дату или null.
        public DateTime? GetControlDate(string filePath)
        {
            return ExtractDateFromRegion(filePath, new RectangleJ(115, 620, 70, 25));
        }

        // Читает дату поступления заявки из фиксированной области первой страницы.
        // Аналогично GetControlDate — см. ExtractDateFromRegion.
        // Возвращает дату или null.
        public DateTime? GetReceiptDate(string filePath)
        {
            return ExtractDateFromRegion(filePath, new RectangleJ(170, 650, 40, 25));
        }

        // Универсальный метод извлечения даты из заданной прямоугольной области первой страницы.
        // filePath — путь к PDF-файлу; rect — область в координатах iTextSharp.
        // Как это работает: PdfReader читает документ, RegionTextRenderFilter отбрасывает весь текст
        // вне области, LocationTextExtractionStrategy извлекает оставшийся текст с учётом расположения,
        // далее полученная строка парсится через DateTime.TryParse.
        // Возвращает распознанную дату или null (в т.ч. при ошибке чтения — пользователь уведомлён).
        private DateTime? ExtractDateFromRegion(string filePath, RectangleJ rect)
        {
            try
            {
                using var reader = new PdfReader(filePath);

                // Фильтр по области + стратегия извлечения, обёрнутая в FilteredTextRenderListener:
                // на выходе — только текст, попавший внутрь rect.
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
                _messageService.ShowError($"Не удалось прочитать дату из файла.\n" +
                    $"Возможно, выбран не тот документ или документ уже был отсканирован.\n" +
                    $"Системное сообщение: {ex.Message}");
                return null;
            }
        }

        // Извлекает ФИО заявителя из фиксированной области первой страницы (та же техника, что и у дат).
        // filePath — путь к PDF-файлу.
        // Текст области склеивается в одну строку (переводы строк заменяются пробелами) и разбивается
        // по пробелам; первые три слова считаются фамилией, именем и отчеством.
        // Возвращает ApplicantInfo или null, если слов меньше трёх либо чтение завершилось ошибкой.
        private ApplicantInfo? ExtractApplicantFromPdf(string filePath)
        {
            try
            {
                using var reader = new PdfReader(filePath);

                // Область с ФИО заявителя (координаты подогнаны под конкретный макет сервисного листа).
                var rect = new RectangleJ(90, 720, 50, 65);
                var filter = new RenderFilter[] { new RegionTextRenderFilter(rect) };
                var strategy = new FilteredTextRenderListener(new LocationTextExtractionStrategy(), filter);
                var text = iTextPdfTextExtractor.GetTextFromPage(reader, 1, strategy);

                // Склейка переносов в пробелы и разбиение на слова с удалением пустых.
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

        // Асинхронная обёртка над ProcessPdf: выполняет обработку файла в потоке из пула (Task.Run),
        // чтобы не блокировать UI. Внимание: внутри ProcessPdf могут показываться модальные диалоги
        // (ShowYesNo при превышении контрольной даты) — при async/void-обработчике кнопки это работает,
        // но диалоги показываются из фонового потока; реализация MessageService это допускает.
        // Аргументы и результат — см. ProcessPdf.
        public async Task<bool> ProcessPdfAsync(string fileName, AppSettings settings, PdfProcessingOptions options)
        {
            return await Task.Run(() => ProcessPdf(fileName, settings, options));
        }

        // Основной метод обработки одного сервисного листа.
        // fileName — имя файла (полные пути собираются из settings.InputPath / settings.OutputPath);
        // settings — настройки (пути и данные исполнителя); options — опции заполнения из главной формы.
        // Порядок действий:
        //   1. Найти маркер и получить координаты якоря (без маркера обработка невозможна — false);
        //   2. Открыть исходный PDF через PdfReader и создать PdfStamper для записи результата;
        //   3. Оставить только страницы 1-2, получить слой наложения (GetOverContent) первой страницы;
        //   4. Создать шрифт (системный Arial с Unicode-кодировкой IDENTITY_H — для кириллицы);
        //   5. Наложить ФИО исполнителя, оценку, дату и заявителя.
        // Возвращает true при успехе; false — при любой ошибке (пользователь уведомлён).
        private bool ProcessPdf(string fileName, AppSettings settings, PdfProcessingOptions options)
        {
            // Полные пути входного и выходного файлов (имя файла сохраняется).
            var inputPath = System.IO.Path.Combine(settings.InputPath, fileName);
            var outputPath = System.IO.Path.Combine(settings.OutputPath, fileName);

            // Шаг 1: поиск якорной точки по маркеру.
            var coordinates = GetMarkerCoordinates(inputPath);
            if (!coordinates.HasValue)
            {
                _messageService.ShowError($"Не найден маркер в файле: {fileName}");
                return false;
            }

            // Округление координат до целых пикселей — дальше работаем с System.Drawing.Point.
            var point = Point.Round(coordinates.Value);

            try
            {
                // Шаг 2-3: открываем исходник, создаём выходной поток и штамповщик.
                // Все ресурсы освобождаются через using (reader, поток и stamper закроются корректно).
                using var reader = new PdfReader(inputPath);
                using var outputStream = new FileStream(outputPath, FileMode.Create);
                using var stamper = new PdfStamper(reader, outputStream);

                // Оставляем в документе только первые две страницы.
                reader.SelectPages("1-2");
                var pageSize = reader.GetPageSize(1);

                // Слой поверх содержимого первой страницы — именно сюда пишутся все надписи.
                var contentByte = stamper.GetOverContent(1);

                // Шаг 4: базовый шрифт для всех надписей.
                var baseFont = CreateBaseFont();

                // Шаг 5: последовательное наложение надписей.

                // Добавление ФИО исполнителя
                AddExecutorName(point, contentByte, pageSize, settings, baseFont);

                // Добавление оценки
                AddMark(point, contentByte, pageSize, baseFont);

                // Обработка даты (может потребовать диалога с пользователем;
                // возвращает false, если даты прочитать не удалось — обработка отменяется)
                if (!ProcessDate(point, contentByte, pageSize, inputPath, options, baseFont, fileName))
                {
                    return false;
                }

                // Обработка заявителя (пользовательский или извлечённый из документа)
                ProcessApplicant(point, contentByte, pageSize, inputPath, options, baseFont);

                return true;
            }
            catch (Exception ex)
            {
                _messageService.ShowError($"Ошибка обработки файла {fileName}: {ex.Message}");
                return false;
            }
        }

        // Создаёт базовый шрифт для надписей: берётся системный ARIAL.TTF из каталога шрифтов Windows.
        // IDENTITY_H — горизонтальная Unicode-кодировка, обязательна для корректного вывода кириллицы;
        // NOT_EMBEDDED — шрифт в PDF не встраивается (файл меньше, но на ПК без Arial текст не отобразится).
        private BaseFont CreateBaseFont()
        {
            var fontPath = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Fonts),
                "ARIAL.TTF");
            return BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        }

        // Наносит на документ ФИО исполнителя в сокращённом виде (берётся из настроек).
        // coordinates — координаты маркера (система Spire, Y вниз);
        // content — слой наложения страницы; pageSize — размер страницы (нужен для пересчёта Y);
        // settings — настройки с данными исполнителя; baseFont — базовый шрифт.
        // Тонкости: Y пересчитывается в систему iTextSharp (pageSize.Height - y),
        // сдвиги X-8 и Y+7 подогнаны под макет формы (текст должен попасть в рамку рядом с маркером),
        // выравнивание по центру (Element.ALIGN_CENTER), размер шрифта 9.
        private void AddExecutorName(Point coordinates, PdfContentByte content,
            iTextRectangle pageSize, AppSettings settings, BaseFont baseFont)
        {
            var font = new iTextFont(baseFont, 9, iTextFont.NORMAL);
            int y = (int)(pageSize.Height - coordinates.Y);
            var text = settings.GetExecutorShortName();

            ColumnText.ShowTextAligned(content, Element.ALIGN_CENTER,
                new Phrase(text, font), coordinates.X - 8, y + 7, 0);
        }

        // Наносит на документ оценку (значение по умолчанию — DefaultMark = "5").
        // Позиция: на одной строке с маркером, смещение +215 по X от его начала,
        // размер шрифта 12, выравнивание по центру.
        private void AddMark(Point coordinates, PdfContentByte content,
            iTextRectangle pageSize, BaseFont baseFont)
        {
            var font = new iTextFont(baseFont, 12, iTextFont.NORMAL);
            int y = (int)(pageSize.Height - coordinates.Y);

            ColumnText.ShowTextAligned(content, Element.ALIGN_CENTER,
                new Phrase(DefaultMark.ToString(), font), coordinates.X + 215, y, 0);
        }

        // Определяет и наносит дату/время закрытия заявки — самая сложная часть заполнения.
        // coordinates — якорь; content — слой наложения; pageSize — размер страницы;
        // inputPath — путь к исходному файлу (оттуда читаются контрольная и поступившая даты);
        // options — опции; baseFont — шрифт; fileName — имя файла для сообщений.
        // Логика выбора даты:
        //   - опция "только месяц и год" — наносятся только цифры месяца и последняя цифра года;
        //   - "своя дата" — берётся выбранная пользователем дата; если она больше контрольной,
        //     пользователю задаётся вопрос: либо продолжить с выбранной датой, либо проставить
        //     "задним числом" = контрольная дата минус 1 час;
        //   - иначе — берётся текущее время, но если оно больше контрольной даты,
        //     автоматически проставляется контрольная минус 1 час;
        //   - если поступившая или контрольная дата из документа прочитать не удалось — false.
        // Возвращает false, если полную дату нанести не удалось (файл считается не обработанным).
        private bool ProcessDate(Point coordinates, PdfContentByte content,
            iTextRectangle pageSize, string inputPath,
            PdfProcessingOptions options, BaseFont baseFont, string fileName)
        {
            // Шрифт крупнее (20), т.к. цифры даты пишутся по одной в большие рамки полей.
            var font = new iTextFont(baseFont, 20, iTextFont.NORMAL);
            int y = (int)(pageSize.Height - coordinates.Y);

            // Ветка "только месяц и год": полные дата/время не нужны.
            if (options.FillOnlyMonthYear)
            {
                AddMonthYearOnly(coordinates, content, y, font);
                return true;
            }

            DateTime dateToUse;

            // Ветка "своя дата": пользователь задал дату и время вручную.
            if (options.UseCustomDate && options.CustomDateTime.HasValue)
            {
                dateToUse = options.CustomDateTime.Value;

                // Контроль: выбранная дата не должна превышать контрольную дату из документа.
                var controlDate = GetControlDate(inputPath);

                if (controlDate.HasValue && dateToUse > controlDate.Value)
                {
                    // Собираем подробный вопрос пользователю с датами из документа.
                    var receiptDate = GetReceiptDate(inputPath);
                    var message = $"Выбранная дата превышает контрольную!\n" +
                        $"Имя СЛ: {fileName}\n" +
                        $"Контрольная дата: {controlDate.Value}\n" +
                        $"Дата поступления заявки: {receiptDate}\n" +
                        $"Да - Продолжить, Нет - Заполнить задним числом";

                    // Отказ — проставляем "задним числом": на час раньше контрольной даты.
                    if (_messageService.ShowYesNo(message) == System.Windows.Forms.DialogResult.No)
                    {
                        dateToUse = controlDate.Value.AddHours(-1);
                    }
                }
            }
            else
            {
                // Автоматический режим: читаем обе даты из документа.
                var receiptDate = GetReceiptDate(inputPath);
                var controlDate = GetControlDate(inputPath);

                // Без контрольной даты корректную дату закрытия определить нельзя — отмена.
                if (!receiptDate.HasValue || !controlDate.HasValue)
                    return false;

                // Берём "сейчас", но не позже контрольной даты (иначе — минус 1 час).
                dateToUse = DateTime.Now;
                if (dateToUse > controlDate.Value)
                {
                    dateToUse = controlDate.Value.AddHours(-1);
                }
            }

            // Наносим полную дату и время.
            AddFullDateTime(coordinates, content, y, font, dateToUse);
            return true;
        }

        // Наносит только цифры месяца и последнюю цифру текущего года (режим "только месяц и год").
        // Цифры выводятся по одной (отдельной фразой каждая), потому что в бланке под каждую
        // цифру своя рамка; смещения +259/+279/+368 по X подогнаны под макет формы.
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
        // Каждая цифра выводится отдельной фразой в свою рамку бланка
        // (аналогично AddMonthYearOnly); смещения по X захардкожены под конкретный макет СЛ.
        private void AddFullDateTime(Point coordinates, PdfContentByte content,
            int y, iTextFont font, DateTime dateTime)
        {
            // Разбиваем компоненты даты/времени на строки по две цифры.
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

        // Наносит ФИО заявителя (в сокращённом виде) рядом с полем заявителя.
        // Заявитель выбирается так: если задан корректный "свой заявитель" в опциях — берётся он,
        // иначе ФИО извлекается из самого документа (ExtractApplicantFromPdf).
        // Если итоговое ФИО некорректно (меньше трёх компонентов) — надпись не наносится.
        // Позиция: смещение +285 по X и +7 по Y относительно маркера, шрифт 9, по центру.
        private void ProcessApplicant(Point coordinates, PdfContentByte content,
            iTextRectangle pageSize, string inputPath,
            PdfProcessingOptions options, BaseFont baseFont)
        {
            var font = new iTextFont(baseFont, 9, iTextFont.NORMAL);
            int y = (int)(pageSize.Height - coordinates.Y);

            ApplicantInfo? applicant;

            // Выбор источника ФИО: пользовательский вариант приоритетнее, иначе — из документа.
            if (options.UseCustomApplicant && options.CustomApplicant?.IsValid() == true)
            {
                applicant = options.CustomApplicant;
            }
            else
            {
                applicant = ExtractApplicantFromPdf(inputPath);
            }

            // Печатаем только корректное ФИО.
            if (applicant?.IsValid() == true)
            {
                ColumnText.ShowTextAligned(content, Element.ALIGN_CENTER,
                    new Phrase(applicant.GetShortName(), font), coordinates.X + 285, y + 7, 0);
            }
        }
    }
}
