using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ConsoleNotifier : IDocumentObserver
{
    public void OnDocumentChanged(string filePath, string changeDescription)
    {
        Console.WriteLine($"[Уведомление] Документ изменён: {Path.GetFileName(filePath)} ({changeDescription})");
    }

    public void OnDocumentDeleted(string filePath)
    {
        Console.WriteLine($"[Уведомление] Документ удалён: {Path.GetFileName(filePath)}");
    }
}
