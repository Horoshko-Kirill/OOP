using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IRole
{
    void View();
    void Edit();
    void ManageUsers(User currentUser, User targetUser, IRole newRole);


    bool CanViewFile(string filePath);
    bool CanEditFile(string filePath);


    List<string> ViewableFiles { get; }
    List<string> EditableFiles { get; }

    bool CanCreateDocuments { get; }
    bool CanViewDocuments { get; }
    bool CanEditDocuments { get; }
    bool CanDeleteDocuments { get; }
    bool CanSaveAsTxt { get; }
    bool CanSaveAsJson { get; }
    bool CanSaveAsXml { get; }
    bool CanSaveAsMd { get; }
    bool CanSaveAsRtf { get; }
}
