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

    bool CanCreateDocuments { get; }
    bool CanViewDocuments { get; }
    bool CanEditDocuments { get; }
    bool CanDeleteDocuments { get; }
    bool CanSaveAsTxt { get; }
    bool CanSaveAsJson { get; }
    bool CanSaveAsXml { get; }
}
