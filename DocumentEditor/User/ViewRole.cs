using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ViewRole : IRole
{

    public void View()
    {

    }

    public void Edit()
    {

    }

    public void ManageUsers(User current, User targetUser, IRole newRole)
    {

    }

    public List<string> ViewableFiles { get; } = new List<string>();
    public List<string> EditableFiles { get; } = new List<string>();


    public bool CanViewFile(string filePath) => ViewableFiles.Contains(filePath);
    public bool CanEditFile(string filePath) => false; // ViewRole не может редактировать


    public bool CanCreateDocuments => false;
    public bool CanViewDocuments => true;
    public bool CanEditDocuments => false;
    public bool CanDeleteDocuments => false;
    public bool CanSaveAsTxt => false;
    public bool CanSaveAsJson => false;
    public bool CanSaveAsXml => false;
    public bool CanSaveAsMd => false;
    public bool CanSaveAsRtf => false;


}
