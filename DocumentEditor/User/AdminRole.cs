using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AdminRole : IRole
{
    public void View()
    {

    }

    public void Edit()
    {

    }

    public void ManageUsers(User current, User targetUser, IRole newRole)
    {

        targetUser.ChangeRole(newRole);

    }

    public List<string> ViewableFiles { get; } = new List<string>();
    public List<string> EditableFiles { get; } = new List<string>();

    public bool CanViewFile(string filePath) => true; 
    public bool CanEditFile(string filePath) => EditableFiles.Contains(filePath);

    public bool CanCreateDocuments => true;
    public bool CanViewDocuments => true;
    public bool CanEditDocuments => true;
    public bool CanDeleteDocuments => true;
    public bool CanSaveAsTxt => true;
    public bool CanSaveAsJson => true;
    public bool CanSaveAsXml => true;
    public bool CanSaveAsMd => true;
    public bool CanSaveAsRtf => true;

}
