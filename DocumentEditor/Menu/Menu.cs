using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Intrinsics.Arm;

public class Menu
{
    private List<User> users;
    private const string FilePath = "users.json";

    public void ShowMenu()
    {
        if (File.Exists(FilePath))
            users = UserStorage.LoadFromFile(FilePath);
        else
            users = new List<User>();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== МЕНЮ =====");
            Console.WriteLine("1. Выбрать существующего пользователя");
            Console.WriteLine("2. Создать нового пользователя");
            Console.WriteLine("3. Удалить пользователя");
            Console.WriteLine("4. Выйти");

            Console.Write("Выберите опцию: ");
            int choice = ReadInt(1, 4);

            switch (choice)
            {
                case 1:
                    SelectUser();
                    break;
                case 2:
                    CreateUser();
                    break;
                case 3:
                    DeleteUser();
                    break;
                case 4:
                    UserStorage.SaveToFile(users, "users.json");
                    Console.WriteLine("Выход...");
                    return;
            }
        }
       
    }

    private void SelectUser()
    {
        if (users.Count == 0)
        {
            Console.WriteLine("Нет пользователей.");
            Pause();
            return;
        }

        Console.WriteLine("Список пользователей:");
        for (int i = 0; i < users.Count; i++)
        {
            Console.Write($"{i}. ");
            users[i].PrintUserInfo();
        }

        Console.Write("Выберите номер пользователя: ");
        int index = ReadInt(0, users.Count - 1);

        DocManage(users[index], ref users);
    }

    private void CreateUser()
    {
        Console.Write("Введите имя пользователя: ");
        string username = Console.ReadLine();

        Console.WriteLine("Выберите роль:");
        Console.WriteLine("1. ViewRole");
        Console.WriteLine("2. AdminRole");
        Console.WriteLine("3. EditorRole");
        int roleChoice = ReadInt(1, 3);

        IRole role;

        if (roleChoice == 1)
        {
            role = new ViewRole();
        }
        else if (roleChoice == 2)
        {

            role = new AdminRole();

        }
        else
        {

            role = new EditorRole();

        }


        users.Add(new User(username, role));
        UserStorage.SaveToFile(users, FilePath);
        Console.WriteLine("Пользователь создан.");
        Pause();
    }

    private void DeleteUser()
    {
        if (users.Count == 0)
        {
            Console.WriteLine("Нет пользователей для удаления.");
            Pause();
            return;
        }

        Console.WriteLine("Выберите пользователя для удаления:");
        for (int i = 0; i < users.Count; i++)
        {
            Console.Write($"{i}. ");
            users[i].PrintUserInfo();
        }

        int index = ReadInt(0, users.Count - 1);
        Console.WriteLine($"Удалён: {users[index].Username}");

        users.RemoveAt(index);
        UserStorage.SaveToFile(users, FilePath);
        Pause();
    }

    private int ReadInt(int min, int max)
    {
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max)
                return value;
            Console.Write($"Введите число от {min} до {max}: ");
        }
    }

    private void Pause()
    {
        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
    }

    private void DocManage(User user, ref List<User> users)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"== Управление файлами для {user.Username} ==");
            Console.WriteLine("1. Работа с локальными файлами");
            Console.WriteLine("2. Работа с файлами в облаке");
            Console.WriteLine("3. Назад");
            if (user.Role is AdminRole)
            {
                Console.WriteLine("4.Изменить роль других участников");
            }

            Console.Write("Выберите опцию: ");
            int choice = ReadInt(1, 4);

            switch (choice)
            {
                case 1:
                    LocalFileMenu(user);
                    break;
                case 2:
                    CloundFileMenu(user);
                    break;
                case 3:
                    return;
                case 4:

                    Console.WriteLine("=== Список пользователей ===");

                    
                    var regularUsers = users
                        .Select((user, idx) => new { User = user, OriginalIndex = idx })
                        .Where(x => !(x.User.Role is AdminRole))
                        .ToList();

                   
                    for (int i = 0; i < regularUsers.Count; i++)
                    {
                        Console.Write($"{i + 1}. ");  
                        regularUsers[i].User.PrintUserInfo();
                    }

                    if (regularUsers.Count == 0)
                    {
                        Console.WriteLine("Нет доступных пользователей");
                        return;
                    }

                    Console.Write($"Выберите номер пользователя (1-{regularUsers.Count}): ");
                    int selectedNumber = ReadInt(1, regularUsers.Count);

      
                    int originalIndex = regularUsers[selectedNumber - 1].OriginalIndex;

                    Console.WriteLine("Выберите роль");
                    Console.WriteLine("1.ViewRole");
                    Console.WriteLine("2.EditRole");

                    selectedNumber = ReadInt(1, 2);

                    if (selectedNumber == 1)
                    {
                        users[originalIndex].ChangeRole(new ViewRole());
                    }
                    else
                    {
                        users[originalIndex].ChangeRole(new EditorRole());
                    }
                    break;
            }
        }
    }

    private void CloundFileMenu(User user)
    {
        string folderPath = "LR2";
        Directory.CreateDirectory(folderPath);

        DocumentManager docManager = new DocumentManager(user);

        while (true)
        {
            Console.Clear();
            Console.WriteLine("== Локальное меню ==");
            Console.WriteLine("1. Создать новый файл");
            Console.WriteLine("2. Выбрать существующий файл");
            Console.WriteLine("3. Назад");
            Console.Write("Выберите опцию: ");

            int choice = ReadInt(1, 3);

            if (choice == 1)
            {
                Console.Write("Введите имя нового файла (без расширения): ");
                string fileName = Console.ReadLine();
                string filePath = Path.Combine(folderPath, fileName + ".txt");

                if (File.Exists(filePath))
                {
                    Console.WriteLine("Файл с таким именем уже существует.");
                    Pause();
                    continue;
                }

                File.WriteAllText(filePath, "");
                Console.WriteLine("Файл создан.");
                Pause();
            }
            else if (choice == 2)
            {
                List<string> files = Directory.GetFiles(folderPath).ToList();

                if (files.Count == 0)
                {
                    Console.WriteLine("Нет файлов в папке Doc.");
                    Pause();
                    continue;
                }

                Console.WriteLine("Список файлов:");
                for (int i = 0; i < files.Count; i++)
                {
                    Console.WriteLine($"{i}. {Path.GetFileName(files[i])}");
                }

                Console.Write("Выберите файл по номеру: ");
                int selectedFileIndex = ReadInt(0, files.Count - 1);

                string selectedFile = files[selectedFileIndex];
                string fileContent = File.ReadAllText(selectedFile);
                bool fileLoaded = true;

                docManager.OpenDocument(selectedFile);
                docManager.SubscribeObserver(new ConsoleNotifier());
                docManager.LoadHistory();

                while (fileLoaded)
                {
                    Console.Clear();
                    Console.WriteLine($"Файл: {Path.GetFileName(selectedFile)}");
                    Console.WriteLine("1. Посмотреть содержимое");
                    Console.WriteLine("2. Отредактировать");
                    Console.WriteLine("3. Удалить файл");
                    Console.WriteLine("4. Сохранить изменения в облаке");
                    Console.WriteLine("5. Посмотреть историю изменений файла");
                    Console.WriteLine("6. Выйти в меню");

                    Console.Write("Выберите действие: ");
                    int action = ReadInt(1, 6);

                    switch (action)
                    {
                        case 1:
                            Console.WriteLine("\n== Содержимое ==");

                            string ext = Path.GetExtension(selectedFile).ToLower();

                            if (ext == ".md")
                            {
                                docManager.ViewMd();
                            }
                            else if (ext == ".rtf")
                            {
                                docManager.ViewRtf();
                            }
                            else
                            {
                                docManager.ViewDocument();
                            }

                            Pause();
                            break;


                        case 2:
                            Console.Clear();

                            if (!(user.Role is ViewRole))
                            {

                                ConsoleEditor.lines = docManager._documentLines;

                                ConsoleEditor.StartEditor();

                                Console.Clear();

                                docManager.EditDocument(new List<string>(ConsoleEditor.lines));

                                ConsoleEditor.lines.Clear();

                               
                            }
                            else
                            {
                                Console.WriteLine("Нет прав доступа");
                            }


                            Pause();
                            break;

                        case 3:
                            docManager.OpenDocument(selectedFile);
                            docManager.DeleteDocument();
                            Pause();
                            return;

                        case 4:

                            docManager.SaveHistory();

                            Console.WriteLine("Введите название документа");
                            string Name = Console.ReadLine();

                            Console.WriteLine("Выберите формат файла (TXT, JSON, XML, MD, RTF):");
                            string Format = Console.ReadLine();
                            var manager = new GoogleDriveManager();

                            switch (Format.ToUpper())
                            {
                                case "TXT":
                                    docManager.SaveDocument("TXT", "LR2/" + Name + ".txt");
                                    manager.UploadFile("LR2/" + Name + ".txt");
                                    break;
                                case "JSON":
                                    docManager.SaveDocument("JSON", "LR2/" + Name + ".json");
                                    manager.UploadFile("LR2/" + Name + ".json");
                                    break;
                                case "XML":
                                    docManager.SaveDocument("XML", "LR2/" + Name + ".xml");
                                    manager.UploadFile("LR2/" + Name + ".xml");
                                    break;
                                case "MD":
                                    docManager.SaveDocument("MD", "LR2/" + Name + ".md");
                                    manager.UploadFile("LR2/" + Name + ".md");
                                    break;
                                case "RTF":
                                    docManager.SaveDocument("RTF", "LR2/" + Name + ".rtf");
                                    manager.UploadFile("LR2/" + Name + ".rtf");
                                    break;
                                default:
                                    Console.WriteLine("Неподдерживаемый формат.");
                                    break;
                            }

                            Pause();
                            break;
                        case 5:
                            docManager.ShowDocumentHistory();
                            Pause();
                            break;
                        case 6:
                            return;
                    }
                }
            }
            else if (choice == 3)
            {
                return;
            }
        }
    }

    private void LocalFileMenu(User user)
    {
        string folderPath = "Doc";
        Directory.CreateDirectory(folderPath);

        DocumentManager docManager = new DocumentManager(user);

        while (true)
        {
            Console.Clear();
            Console.WriteLine("== Локальное меню ==");
            Console.WriteLine("1. Создать новый файл");
            Console.WriteLine("2. Выбрать существующий файл");
            Console.WriteLine("3. Назад");
            Console.Write("Выберите опцию: ");

            int choice = ReadInt(1, 3);

            if (choice == 1)
            {
                Console.Write("Введите имя нового файла (без расширения): ");
                string fileName = Console.ReadLine();
                string filePath = Path.Combine(folderPath, fileName + ".txt");

                if (File.Exists(filePath))
                {
                    Console.WriteLine("Файл с таким именем уже существует.");
                    Pause();
                    continue;
                }

                File.WriteAllText(filePath, "");
                Console.WriteLine("Файл создан.");
                Pause();
            }
            else if (choice == 2)
            {
                List<string> files = Directory.GetFiles(folderPath).ToList();

                if (files.Count == 0)
                {
                    Console.WriteLine("Нет файлов в папке Doc.");
                    Pause();
                    continue;
                }

                Console.WriteLine("Список файлов:");
                for (int i = 0; i < files.Count; i++)
                {
                    Console.WriteLine($"{i}. {Path.GetFileName(files[i])}");
                }

                Console.Write("Выберите файл по номеру: ");
                int selectedFileIndex = ReadInt(0, files.Count - 1);

                string selectedFile = files[selectedFileIndex];
                string fileContent = File.ReadAllText(selectedFile);
                bool fileLoaded = true;

                docManager.OpenDocument(selectedFile);
                docManager.SubscribeObserver(new ConsoleNotifier());
                docManager.LoadHistory();

                while (fileLoaded)
                {
                    Console.Clear();
                    Console.WriteLine($"Файл: {Path.GetFileName(selectedFile)}");
                    Console.WriteLine("1. Посмотреть содержимое");
                    Console.WriteLine("2. Отредактировать");
                    Console.WriteLine("3. Удалить файл");
                    Console.WriteLine("4. Сохранить изменения локально");
                    Console.WriteLine("5. Сохранить изменения локально и в облаке");
                    Console.WriteLine("6. Посмотреть историю изменений файла");
                    Console.WriteLine("7. Выйти в меню");

                    Console.Write("Выберите действие: ");
                    int action = ReadInt(1, 7);

                    switch (action)
                    {
                        case 1:
                            Console.WriteLine("\n== Содержимое ==");

                            string ext = Path.GetExtension(selectedFile).ToLower();

                            if (ext == ".md")
                            {
                                docManager.ViewMd();
                            }
                            else if (ext == ".rtf")
                            {
                                docManager.ViewRtf();
                            }
                            else
                            {
                                docManager.ViewDocument();
                            }

                            Pause();
                            break;


                        case 2:

                            Console.Clear();

                            if (!(user.Role is ViewRole))
                            {

                                ConsoleEditor.lines = docManager._documentLines;

                                ConsoleEditor.StartEditor();

                                Console.Clear();

                                docManager.EditDocument(new List<string>(ConsoleEditor.lines));

                                ConsoleEditor.lines.Clear();


                            }
                            else
                            {
                                Console.WriteLine("Нет прав доступа");
                            }

                            Pause();

                            break;

                        case 3:
                            docManager.OpenDocument(selectedFile);
                            docManager.DeleteDocument();
                            Pause();
                            return;

                        case 4:

                            docManager.SaveHistory();

                            Console.WriteLine("Введите название документа");
                            string name = Console.ReadLine();

                            Console.WriteLine("Выберите формат файла (TXT, JSON, XML, MD, RTF):");
                            string format = Console.ReadLine();

                            switch (format.ToUpper())
                            {
                                case "TXT":
                                    docManager.SaveDocument("TXT", "Doc/" + name + ".txt");
                                    break;
                                case "JSON":
                                    docManager.SaveDocument("JSON", "Doc/" + name + ".json");
                                    break;
                                case "XML":
                                    docManager.SaveDocument("XML", "Doc/" + name + ".xml");
                                    break;
                                case "MD":
                                    docManager.SaveDocument("MD", "Doc/" + name + ".md");
                                    break;
                                case "RTF":
                                    docManager.SaveDocument("RTF", "Doc/" + name + ".rtf");
                                    break;
                                default:
                                    Console.WriteLine("Неподдерживаемый формат.");
                                    break;
                            }
                            Pause();
                            break;

                        case 5:

                            docManager.SaveHistory();

                            Console.WriteLine("Введите название документа");
                            string Name = Console.ReadLine();

                            Console.WriteLine("Выберите формат файла (TXT, JSON, XML, MD, RTF):");
                            string Format = Console.ReadLine();
                            var manager = new GoogleDriveManager();

                            switch (Format.ToUpper())
                            {
                                case "TXT":
                                    docManager.SaveDocument("TXT", "Doc/" + Name + ".txt");
                                    docManager.SaveDocument("TXT", "LR2/" + Name + ".txt");
                                    manager.UploadFile("LR2/" + Name + ".txt");
                                    break;
                                case "JSON":
                                    docManager.SaveDocument("JSON", "Doc/" + Name + ".json");
                                    docManager.SaveDocument("JSON", "LR2/" + Name + ".json");
                                    manager.UploadFile("LR2/" + Name + ".json");
                                    break;
                                case "XML":
                                    docManager.SaveDocument("XML", "Doc/" + Name + ".xml");
                                    docManager.SaveDocument("XML", "LR2/" + Name + ".xml");
                                    manager.UploadFile("LR2/" + Name + ".xml");
                                    break;
                                case "MD":
                                    docManager.SaveDocument("MD", "Doc/" + Name + ".md");
                                    docManager.SaveDocument("MD", "LR2/" + Name + ".md");
                                    manager.UploadFile("LR2/" + Name + ".md");
                                    break;
                                case "RTF":
                                    docManager.SaveDocument("RTF", "Doc/" + Name + ".rtf");
                                    docManager.SaveDocument("RTF", "LR2/" + Name + ".rtf");
                                    manager.UploadFile("LR2/" + Name + ".rtf");
                                    break;
                                default:
                                    Console.WriteLine("Неподдерживаемый формат.");
                                    break;
                            }

                            break;
                        case 6:
                            docManager.ShowDocumentHistory();
                            Pause();
                            break;
                        case 7:
                            return;
                    }
                }
            }
            else if (choice == 3)
            {
                return;
            }
        }
    }


}
