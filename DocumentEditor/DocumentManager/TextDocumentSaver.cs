using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TextDocumentSaver : IDocumentSaver
{
    public void Save(List<string> lines, string filePath) =>
        File.WriteAllLines(filePath, lines);
}
