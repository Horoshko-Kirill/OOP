using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MarkdownDocumentSaver : IDocumentSaver
{
    public void Save(List<string> lines, string filePath)
    {
        var sb = new StringBuilder();
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                sb.Append("\n\n");
            }
            else
            {
                sb.AppendLine(line);
                // Добавляем два пробела в конце для переноса строки в Markdown
                if (!line.EndsWith("  ")) sb.Append("  ");
                sb.AppendLine();
            }
        }
        File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
    }
}