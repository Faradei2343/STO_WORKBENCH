using System;
using System.IO;
using System.Text.RegularExpressions;

namespace STO_WORKBENCH.FileNameExporter
{
    public static class TemplateEngine
    {
        public static string ApplyTemplate(
            string fileName,
            string template,
            string separator)
        {
            if (string.IsNullOrWhiteSpace(template))
                return string.Empty;

            string nameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            string extension = Path.GetExtension(fileName);

            string[] parts;

            if (string.IsNullOrEmpty(separator))
            {
                parts = new[] { nameWithoutExtension };
            }
            else
            {
                parts = nameWithoutExtension.Split(
                    new[] { separator },
                    StringSplitOptions.None
                );
            }

            string result = template;

            result = result.Replace("{file}", fileName);
            result = result.Replace("{name}", nameWithoutExtension);
            result = result.Replace("{ext}", extension);

            result = ReplaceIndexedParts(result, parts);
            result = ReplaceNameSubstring(result, nameWithoutExtension);
            result = ReplaceRegexMatches(result, fileName);

            return result;
        }

        /// <summary>
        /// {0}
        /// {3}_{4}
        /// {4:0:4}
        /// </summary>
        private static string ReplaceIndexedParts(string template, string[] parts)
        {
            return Regex.Replace(template, @"\{(\d+)(?::(\d+):(\d+))?\}", match =>
            {
                int index = int.Parse(match.Groups[1].Value);

                if (index < 0 || index >= parts.Length)
                    return string.Empty;

                string value = parts[index];

                bool hasSubstring =
                    match.Groups[2].Success &&
                    match.Groups[3].Success;

                if (!hasSubstring)
                    return value;

                int start = int.Parse(match.Groups[2].Value);
                int length = int.Parse(match.Groups[3].Value);

                return SafeSubstring(value, start, length);
            });
        }

        /// <summary>
        /// {name:0:10}
        /// </summary>
        private static string ReplaceNameSubstring(
            string template,
            string nameWithoutExtension)
        {
            return Regex.Replace(template, @"\{name:(\d+):(\d+)\}", match =>
            {
                int start = int.Parse(match.Groups[1].Value);
                int length = int.Parse(match.Groups[2].Value);

                return SafeSubstring(nameWithoutExtension, start, length);
            });
        }

        /// <summary>
        /// {match:m\d+}
        /// {match:m\d{3}}
        /// {match:№\s*(\d+):1}
        /// </summary>
        private static string ReplaceRegexMatches(string template, string fileName)
        {
            return Regex.Replace(template, @"\{match:(.+?)\}", match =>
            {
                string body = match.Groups[1].Value;

                if (string.IsNullOrWhiteSpace(body))
                    return string.Empty;

                string pattern = body;
                int groupIndex = 0;

                int lastColonIndex = body.LastIndexOf(':');

                if (lastColonIndex > -1)
                {
                    string possibleGroup = body.Substring(lastColonIndex + 1);

                    if (int.TryParse(possibleGroup, out int parsedGroup))
                    {
                        pattern = body.Substring(0, lastColonIndex);
                        groupIndex = parsedGroup;
                    }
                }

                try
                {
                    Match regexMatch = Regex.Match(
                        fileName,
                        pattern,
                        RegexOptions.IgnoreCase
                    );

                    if (!regexMatch.Success)
                        return string.Empty;

                    if (groupIndex >= 0 && groupIndex < regexMatch.Groups.Count)
                        return regexMatch.Groups[groupIndex].Value;

                    return regexMatch.Value;
                }
                catch
                {
                    return string.Empty;
                }
            });
        }

        private static string SafeSubstring(string value, int start, int length)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            if (start < 0)
                start = 0;

            if (length < 0)
                length = 0;

            if (start >= value.Length)
                return string.Empty;

            if (start + length > value.Length)
                length = value.Length - start;

            return value.Substring(start, length);
        }
    }
}