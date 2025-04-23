using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

public class DocumentManager
{
    private string _currentContent = string.Empty;
    private string _currentFilePath = null;
    private readonly User _currentUser;

    public DocumentManager(User user)
    {
        _currentUser = user;
    }

    public void CreateDocument()
    {
        if (!_currentUser.Role.CanCreateDocuments)
        {
            Console.WriteLine("Access denied: Cannot create documents");
            return;
        }

        _currentContent = string.Empty;
        _currentFilePath = null;
        Console.WriteLine("New document created");
    }

    public void OpenDocument(string filePath)
    {
        if (!_currentUser.Role.CanViewDocuments)
        {
            Console.WriteLine("Access denied: Cannot view documents");
            return;
        }

        try
        {
            _currentContent = File.ReadAllText(filePath);
            _currentFilePath = filePath;
            Console.WriteLine($"Document opened: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error opening file: {ex.Message}");
        }
    }

    public void EditDocument(string newContent)
    {
        if (!_currentUser.Role.CanEditDocuments)
        {
            Console.WriteLine("Access denied: Cannot edit documents");
            return;
        }

        _currentContent = newContent;
        Console.WriteLine("Document content updated");
    }

    public void SaveDocument(string format = "TXT", string customPath = null)
    {
        if (!CanSaveFormat(format))
        {
            Console.WriteLine($"Access denied: Cannot save as {format}");
            return;
        }

        string savePath = customPath ?? _currentFilePath;
        if (string.IsNullOrEmpty(savePath))
        {
            Console.WriteLine("Please specify file path");
            return;
        }

        try
        {
            IDocumentSaver saver = GetDocumentSaver(format);
            saver.Save(_currentContent, savePath);
            _currentFilePath = savePath;
            Console.WriteLine($"Document saved as {format}: {savePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving document: {ex.Message}");
        }
    }

    public void DeleteDocument(string filePath = null)
    {
        if (!_currentUser.Role.CanDeleteDocuments)
        {
            Console.WriteLine("Access denied: Cannot delete documents");
            return;
        }

        string pathToDelete = filePath ?? _currentFilePath;
        if (string.IsNullOrEmpty(pathToDelete))
        {
            Console.WriteLine("Please specify file path");
            return;
        }

        try
        {
            if (File.Exists(pathToDelete))
            {
                File.Delete(pathToDelete);
                Console.WriteLine($"Document deleted: {pathToDelete}");

                if (pathToDelete == _currentFilePath)
                {
                    _currentFilePath = null;
                    _currentContent = string.Empty;
                }
            }
            else
            {
                Console.WriteLine("File not found");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting document: {ex.Message}");
        }
    }

    public void ViewDocument()
    {
        if (!_currentUser.Role.CanViewDocuments)
        {
            Console.WriteLine("Access denied: Cannot view documents");
            return;
        }

        Console.WriteLine("Document content:");
        Console.WriteLine(_currentContent);
    }

    private bool CanSaveFormat(string format)
    {
        return format.ToUpper() switch
        {
            "TXT" => _currentUser.Role.CanSaveAsTxt,
            "JSON" => _currentUser.Role.CanSaveAsJson,
            "XML" => _currentUser.Role.CanSaveAsXml,
            _ => false
        };
    }

    private IDocumentSaver GetDocumentSaver(string format)
    {
        return format.ToUpper() switch
        {
            "TXT" => new TextDocumentSaver(),
            "JSON" => new JsonDocumentSaver(),
            "XML" => new XmlDocumentSaver(),
            _ => throw new NotSupportedException($"Format {format} not supported")
        };
    }
}

// Интерфейс и реализации для сохранения документов (Adapter Pattern)
public interface IDocumentSaver
{
    void Save(string content, string filePath);
}

public class TextDocumentSaver : IDocumentSaver
{
    public void Save(string content, string filePath) => File.WriteAllText(filePath, content);
}

public class JsonDocumentSaver : IDocumentSaver
{
    public void Save(string content, string filePath) =>
        File.WriteAllText(filePath, JsonSerializer.Serialize(new { Content = content }));
}

public class XmlDocumentSaver : IDocumentSaver
{
    public void Save(string content, string filePath)
    {
        using var writer = XmlWriter.Create(filePath, new XmlWriterSettings { Indent = true });
        writer.WriteStartDocument();
        writer.WriteStartElement("Document");
        writer.WriteElementString("Content", content);
        writer.WriteEndElement();
        writer.WriteEndDocument();
    }
}
