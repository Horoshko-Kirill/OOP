using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Text.Json;
using System.Text.RegularExpressions;

public class DocumentManager
{
    public List<string> _documentLines = new List<string>();
    public string _currentFilePath = null;
    private readonly User _currentUser;
    private readonly DocumentHistory _history = new DocumentHistory();
    private readonly List<IDocumentObserver> _observers = new List<IDocumentObserver>();
    private const string HistoryFileName = "document_history.json";
    private readonly string _historyFilePath;


    public void SubscribeObserver(IDocumentObserver observer)
    {
        _observers.Add(observer);
    }

    public List<string> CurrentDocument => new List<string>(_documentLines);

    public DocumentManager(User user)
    {
        _currentUser = user;

        _historyFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, HistoryFileName);
        LoadHistory();
    }

    private void LoadHistory()
    {
        try
        {
            if (File.Exists(_historyFilePath))
            {
                string json = File.ReadAllText(_historyFilePath);
                var versions = JsonSerializer.Deserialize<List<DocumentVersion>>(json);
                if (versions != null)
                {
                    _history.Versions.Clear();
                    _history.Versions.AddRange(versions);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка загрузки истории: {ex.Message}");
        }
    }

    public void SaveHistory()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(_history.Versions, options);
            File.WriteAllText(_historyFilePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка сохранения истории: {ex.Message}");
        }
    }

    public void ShowDocumentHistory()
    {
        Console.WriteLine("=== История изменений ===");
        foreach (var version in _history.Versions.OrderByDescending(v => v.Timestamp))
        {
            Console.WriteLine($"[{version.Timestamp}] {version.Description}");
        }
    }

    private void NotifyObservers(string changeDescription)
    {
        foreach (var observer in _observers)
        {
            observer.OnDocumentChanged(_currentFilePath, changeDescription);
        }
    }

    // Основной метод редактирования документа
    public void EditDocument(List<string> newLines)
    {
        if (!_currentUser.Role.CanEditDocuments)
        {
            Console.WriteLine("У вас нет прав для редактирования документов");
            return;
        }

        if (_documentLines.Count > 0)
        {
            string oldContent = string.Join(Environment.NewLine, _documentLines);
            _history.AddVersion(oldContent, "Редактирование документа");
        }

    }

    // Создание нового документа
    public void CreateDocument(List<string> initialLines = null)
    {
        if (!_currentUser.Role.CanCreateDocuments)
        {
            Console.WriteLine("У вас нет прав для создания документов");
            return;
        }

        _documentLines = initialLines ?? new List<string> { "" };
        _currentFilePath = null;
        Console.WriteLine($"Создан новый документ");
    }

    // Открытие документа
    public void OpenDocument(string filePath)
    {
        if (!_currentUser.Role.CanViewDocuments)
        {
            Console.WriteLine("У вас нет прав для просмотра документов");
            return;
        }

        try
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл не существует");
                return;
            }

            string extension = Path.GetExtension(filePath).ToLower();

            switch (extension)
            {
                case ".json":
                    string jsonContent = File.ReadAllText(filePath);
                    _documentLines = JsonSerializer.Deserialize<List<string>>(jsonContent);
                    break;

                case ".xml":
                    var xmlDoc = new XmlDocument();
                    xmlDoc.Load(filePath);
                    _documentLines = new List<string>();
                    foreach (XmlNode node in xmlDoc.SelectNodes("/Document/Line"))
                    {
                        _documentLines.Add(node.InnerText);
                    }
                    break;

                case ".txt":
                default:
                    _documentLines = new List<string>(File.ReadAllLines(filePath));
                    break;
            }

            List<string> previousLines = new List<string>(_documentLines);

            _currentFilePath = filePath;

            _history.AddVersion(string.Join(Environment.NewLine, previousLines),
                $"Открыт документ: {Path.GetFileName(filePath)}");

            NotifyObservers($"Документ открыт: {Path.GetFileName(filePath)}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при открытии файла {filePath}: {ex.Message}");
        }
    }

    // Сохранение документа
    public void SaveDocument(string format = "TXT", string customPath = null)
    {
        string normalizedFormat = format?.Trim().ToUpperInvariant();

        if (!CanSaveFormat(normalizedFormat))
        {
            Console.WriteLine($"У вас нет прав для сохранения в формате {normalizedFormat}");
            return;
        }

        string savePath = customPath ?? _currentFilePath;
        if (string.IsNullOrEmpty(savePath))
        {
            Console.WriteLine("Укажите путь для сохранения");
            return;
        }

        try
        {

            if (normalizedFormat == "RTF")
            {
                for (int i = 0; i < _documentLines.Count(); i++)
                {
                    _documentLines[i] = FormatConverter.MarkdownToRtf(_documentLines[i]);
                }
            }
            else if (normalizedFormat == "MD")
            {
                for (int i = 0; i < _documentLines.Count(); i++)
                {
                    _documentLines[i] = FormatConverter.RtfToMarkdown(_documentLines[i]);
                }
            }

            IDocumentSaver saver = GetDocumentSaver(normalizedFormat);
            saver.Save(_documentLines, savePath);  // Без приведения — уже List<string>
            _currentFilePath = savePath;

            Console.WriteLine($"Документ сохранен как {normalizedFormat}: {savePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сохранении: {ex.Message}");
        }
    }


    // Удаление документа
    public void DeleteDocument(string filePath = null)
    {
        if (!_currentUser.Role.CanDeleteDocuments)
        {
            Console.WriteLine("У вас нет прав для удаления документов");
            return;
        }

        string pathToDelete = filePath ?? _currentFilePath;

        NotifyObservers($"Документ будет удалён: {Path.GetFileName(pathToDelete)}");

        try
        {
            if (File.Exists(pathToDelete))
            {
                File.Delete(pathToDelete);
                Console.WriteLine($"Документ удален: {pathToDelete}");

                if (pathToDelete == _currentFilePath)
                {
                    _currentFilePath = null;
                    _documentLines.Clear();
                }
            }
            else
            {
                Console.WriteLine("Файл не найден");
            }

            foreach (var observer in _observers)
            {
                observer.OnDocumentDeleted(pathToDelete);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при удалении: {ex.Message}");
        }
    }

    
    public void ViewDocument()
    {
        if (!_currentUser.Role.CanViewDocuments)
        {
            Console.WriteLine("У вас нет прав для просмотра документов");
            return;
        }

        Console.WriteLine(string.Join(Environment.NewLine, _documentLines));
    }

    public void ViewMd()
    {
        if (!_currentUser.Role.CanViewDocuments)
        {
            Console.WriteLine("У вас нет прав для просмотра документов");
            return;
        }

        foreach (var line in _documentLines)
        {
            string formattedLine = line;

            // Обработка жирного текста **text**
            formattedLine = System.Text.RegularExpressions.Regex.Replace(
                formattedLine,
                @"\*\*(.*?)\*\*",
                match => $"\x1b[1m{match.Groups[1].Value}\x1b[0m");

            // Обработка курсива *text* или _text_
            formattedLine = System.Text.RegularExpressions.Regex.Replace(
                formattedLine,
                @"(\*|_)(.*?)\1",
                match => $"\x1b[3m{match.Groups[2].Value}\x1b[0m");

            // Обработка подчёркивания <u>text</u>
            formattedLine = System.Text.RegularExpressions.Regex.Replace(
                formattedLine,
                @"<u>(.*?)</u>",
                match => $"\x1b[4m{match.Groups[1].Value}\x1b[0m");

            Console.WriteLine(formattedLine);
        }
    }

    public void ViewRtf()
    {
        if (!_currentUser.Role.CanViewDocuments)
        {
            Console.WriteLine("У вас нет прав для просмотра документов");
            return;
        }

        foreach (var line in _documentLines)
        {
            string formattedLine = line;

            // Обработка всех RTF-команд в любом порядке (например, {\b\iТекст})
            formattedLine = System.Text.RegularExpressions.Regex.Replace(
                formattedLine,
                @"\\b|\\i|\\ul",  // Ищем любые из этих команд
                match =>
                {
                    return match.Value switch
                    {
                        @"\b" => "\x1b[1m",  // Жирный
                        @"\i" => "\x1b[3m",  // Курсив
                        @"\ul" => "\x1b[4m", // Подчёркивание
                        _ => ""
                    };
                });

            // Закрываем все стили после }
            formattedLine = System.Text.RegularExpressions.Regex.Replace(
                formattedLine,
                @"}",
                "\x1b[0m");

            // Удаляем оставшиеся RTF-спецсимволы (но сохраняем текст внутри)
            formattedLine = System.Text.RegularExpressions.Regex.Replace(
                formattedLine,
                @"[{}]|\\[a-z]+\d*",
                "");

            Console.WriteLine(formattedLine);
        }
    }

    private bool CanSaveFormat(string format)
    {
        return format.ToUpper() switch
        {
            "TXT" => _currentUser.Role.CanSaveAsTxt,
            "JSON" => _currentUser.Role.CanSaveAsJson,
            "XML" => _currentUser.Role.CanSaveAsXml,
            "MD" => _currentUser.Role.CanSaveAsMd,
            "RTF" => _currentUser.Role.CanSaveAsRtf,
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
            "MD" => new MarkdownDocumentSaver(),
            "RTF" => new RtfDocumentSaver(),
            _ => throw new NotSupportedException($"Формат {format} не поддерживается")
        };
    }

    public void SaveHistoryToFile(string filePath)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(_history.Versions, options);
        File.WriteAllText(filePath, json);
    }
}

