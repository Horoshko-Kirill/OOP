using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class JsonDocumentSaver : IDocumentSaver
{
    public void Save(List<string> lines, string filePath) =>
        File.WriteAllText(filePath, JsonSerializer.Serialize(lines));
}