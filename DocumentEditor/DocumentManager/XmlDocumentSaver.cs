using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

public class XmlDocumentSaver : IDocumentSaver
{
    public void Save(List<string> lines, string filePath)
    {
        using var writer = XmlWriter.Create(filePath, new XmlWriterSettings { Indent = true });
        writer.WriteStartDocument();
        writer.WriteStartElement("Document");

        foreach (var line in lines)
        {
            writer.WriteElementString("Line", line);
        }

        writer.WriteEndElement();
        writer.WriteEndDocument();
    }
}
