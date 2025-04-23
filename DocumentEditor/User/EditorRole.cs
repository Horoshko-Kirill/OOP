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

    public void ManageUsers(User current, User targetUser, IRole newRole)
    {

    }

    public bool CanCreateDocuments => true;
    public bool CanViewDocuments => true;
    public bool CanEditDocuments => true;
    public bool CanDeleteDocuments => false;
    public bool CanSaveAsTxt => true;
    public bool CanSaveAsJson => true;
    public bool CanSaveAsXml => false;

}
