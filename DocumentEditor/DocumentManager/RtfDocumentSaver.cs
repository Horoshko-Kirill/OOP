using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RtfDocumentSaver : IDocumentSaver
{
    public void Save(List<string> lines, string filePath)
    {
        var header = @"{\rtf1\ansi\ansicpg1251\deff0\nouicompat\deflang1049
{\fonttbl{\f0\fnil\fcharset204 Calibri;}}
{\colortbl ;\red0\green0\blue0;}
{\*\generator DocumentManager}\viewkind4\uc1
\pard\sa200\sl276\slmult1\f0\fs22\lang9 ";

        var footer = @"\par}";

        var sb = new StringBuilder(header);

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                sb.Append(@"\par");
            }
            else
            {
                var escaped = line
                    .Replace(@"\", @"\\")
                    .Replace("{", @"\{")
                    .Replace("}", @"\}")
                    .Replace("\n", @"\line ");

                sb.Append(escaped);
            }
            sb.Append(@"\par");
        }

        sb.Append(footer);
        File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
    }
}