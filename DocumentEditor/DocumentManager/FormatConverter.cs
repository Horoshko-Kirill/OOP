using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class FormatConverter
{
    // MD → RTF
    public static string MarkdownToRtf(string mdText)
    {
        string rtfText = mdText;

        // Сначала обрабатываем самые сложные комбинации
        rtfText = Regex.Replace(rtfText, @"\*\*\*(.*?)\*\*\*", @"{\b\i$1}");
        rtfText = Regex.Replace(rtfText, @"\*\*(.*?)\*\*", @"{\b$1}");
        rtfText = Regex.Replace(rtfText, @"\*(.*?)\*", @"{\i$1}");
        rtfText = Regex.Replace(rtfText, @"<u>(.*?)</u>", @"{\ul$1}");

        return "{" + rtfText + "}";
    }

    // RTF → MD
    public static string RtfToMarkdown(string rtfText)
    {
        if (string.IsNullOrEmpty(rtfText))
            return rtfText;

        // Обрабатываем все группы, включая вложенные
        string mdText = rtfText;
        int safetyCounter = 0;
        const int maxIterations = 10; // Защита от бесконечного цикла

        while (safetyCounter++ < maxIterations && mdText.Contains('{'))
        {
            mdText = Regex.Replace(mdText, @"{((?:\\[a-z]+\d*\s*|{[^{}]*}|[^{}])*)}", match =>
            {
                string groupContent = match.Groups[1].Value;

                // Проверяем флаги форматирования
                bool isBold = groupContent.Contains(@"\b");
                bool isItalic = groupContent.Contains(@"\i");
                bool isUnderline = groupContent.Contains(@"\ul");

                // Убираем управляющие команды из текста (но сохраняем вложенные группы)
                string textContent = Regex.Replace(groupContent, @"\\([a-z]+)\d*\s*", "");

                // Применяем форматирование в нужном порядке
                string result = textContent;

                if (isBold && isItalic)
                    result = $"***{result}***";
                else if (isBold)
                    result = $"**{result}**";
                else if (isItalic)
                    result = $"*{result}*";

                if (isUnderline)
                    result = $"<u>{result}</u>";

                return result;
            });
        }

        // Убираем оставшиеся RTF-команды и скобки
        mdText = Regex.Replace(mdText, @"\\[a-z]+\d*\s*", "");
        mdText = mdText.Replace("{", "").Replace("}", "");

        return mdText;
    }

}
