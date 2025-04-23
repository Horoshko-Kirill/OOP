using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class User
{

    public string Username {  get; }
    public IRole Role { get; private set;  }

    
    public User(string username, IRole role)
    {
        Username = username;
        Role = role;
    }

    public void ChangeRole(IRole role)
    {
        Role = role;
    }

    public void ViewDocument()
    {

        Role.View();

    }

    public void EditDocument()
    {
        Role.Edit();
    }

    public void ManageUsers(User targetUser, IRole role)
    {
        Role.ManageUsers(this, targetUser, role);
    }


    public void PrintUserInfo()
    {

        Console.WriteLine($"Пользователь: {Username}");
        Console.WriteLine($"Роль: {Role.GetType().Name}");

    }

}
