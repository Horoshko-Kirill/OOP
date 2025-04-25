using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IDocumentObserver
{
    void OnDocumentChanged(string filePath, string changeDescription);
    void OnDocumentDeleted(string filePath);
}
