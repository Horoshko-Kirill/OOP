using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EditorRole : IRole
{
    public void View()
    {

    }

    public void Edit()
    {

    }

    public List<string> ViewableFiles { get; } = new List<string>();
    public List<string> EditableFiles { get; } = new List<string>();

    public void ManageUsers(User current, User targetUser, IRole newRole)
    {

    }


    public bool CanViewFile(string filePath) => true; // Редактор может просматривать все
    public bool CanEditFile(string filePath) => EditableFiles.Contains(filePath);
    public bool CanCreateDocuments => true;
    public bool CanViewDocuments => true;
    public bool CanEditDocuments => true;
    public bool CanDeleteDocuments => false;
    public bool CanSaveAsTxt => true;
    public bool CanSaveAsJson => true;
    public bool CanSaveAsXml => true;
    public bool CanSaveAsMd => true;
    public bool CanSaveAsRtf => true;


}
