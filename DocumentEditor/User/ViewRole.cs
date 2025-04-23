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


    public bool CanCreateDocuments => false;
    public bool CanViewDocuments => true;
    public bool CanEditDocuments => false;
    public bool CanDeleteDocuments => false;
    public bool CanSaveAsTxt => false;
    public bool CanSaveAsJson => false;
    public bool CanSaveAsXml => false;

}
