using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DocumentHistory
{
    public List<DocumentVersion> Versions { get; } = new List<DocumentVersion>();

    public void AddVersion(string content, string changeDescription)
    {
        Versions.Add(new DocumentVersion(content, DateTime.Now, changeDescription));
    }
}
