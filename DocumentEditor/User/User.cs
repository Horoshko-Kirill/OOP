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

    public void ViewDocument(string filePath)
    {
        if (Role.CanViewFile(filePath))
        {
            Role.View();
        }
        else
        {
            Console.WriteLine($"Ошибка: У вас нет прав для просмотра файла {filePath}");
        }
    }

    public void EditDocument(string filePath)
    {
        if (Role.CanEditFile(filePath))
        {
            Role.Edit();
        }
        else
        {
            Console.WriteLine($"Ошибка: У вас нет прав для редактирования файла {filePath}");
        }
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
