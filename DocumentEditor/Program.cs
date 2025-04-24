using System;
using System.Runtime.InteropServices;

class Program
{   
    static void Main()
    {

        string mdText = "***Жирный и курсив***, <u>подчёркивание</u>.";
        string rtfText = FormatConverter.MarkdownToRtf(mdText);
        string backToMd = FormatConverter.RtfToMarkdown(rtfText);

        Console.WriteLine($"Исходный MD: {mdText}");
        Console.WriteLine($"→ RTF: {rtfText}");        
        Console.WriteLine($"→ Обратно в MD: {backToMd}");

        var user = new User("author", new EditorRole());
        var docManager = new DocumentManager(user);

        // Создаем документ
        var initialContent = new List<string> { "Первая строка", "Вторая строка" };
        docManager.CreateDocument(initialContent);

        // Редактируем документ
        var newContent = new List<string> { "**Новое содержимое**", "*Вторая строка*", "<u>Третья строка</u>", "**<u>Третья строка</u>**", "*<u>Третья строка</u>*", "***Третья строка***" };
        docManager.EditDocument(newContent);

        // Сохраняем в разных форматах
        docManager.SaveDocument("TXT", "document.txt");
        docManager.SaveDocument("JSON", "1.md");

        docManager.ViewDocument();

        docManager.ViewMd();

        Console.WriteLine("////////");

        docManager.SaveDocument("RTF", "1.rtf");

        docManager._currentFilePath = "1.rtf";

        docManager.ViewRtf();

        docManager.ViewDocument();

        Console.WriteLine("////////");

        docManager.SaveDocument("MD", "1.md");

        docManager.ViewMd();

        docManager.ViewDocument();

        Console.WriteLine("////////");

        docManager.SaveDocument("RTF", "1.rtf");

        docManager._currentFilePath = "1.rtf";

        docManager.ViewRtf();

        docManager.ViewDocument();

        Console.WriteLine("////////");

        newContent = new List<string> { @"{\bНовое содержимое}", @"{\iВторая строка}", @"{\ulТретья строка}", @"{\b\iТретья строка}" };
        docManager.EditDocument(newContent);

        docManager.SaveDocument("TXT", "document.rtf");

        docManager.ViewRtf();

        user = new User("author", new ViewRole());

        newContent = new List<string> { "Новое содержимое", "Вторая строка", "Третья строка" };

        docManager = new DocumentManager(user);
        docManager.EditDocument(newContent);
        docManager.SaveDocument("JSON", "document.json");


        docManager.OpenDocument("document.json");

        docManager.ViewDocument();

    }
}
