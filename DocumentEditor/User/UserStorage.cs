using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

public class UserStorage
{
    private class UserDto
    {
        public string Username { get; set; }
        public string RoleName { get; set; }
    }

    public static void SaveToFile(List<User> users, string filePath)
    {
        var userDtos = users.Select(u => new UserDto
        {
            Username = u.Username,
            RoleName = u.Role.GetType().Name
        }).ToList();

        var json = JsonSerializer.Serialize(userDtos, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
    }

    public static List<User> LoadFromFile(string filePath)
    {
        var json = File.ReadAllText(filePath);
        var userDtos = JsonSerializer.Deserialize<List<UserDto>>(json);

        var users = new List<User>();

        foreach (var dto in userDtos)
        {
            IRole role = dto.RoleName switch
            {
                nameof(ViewRole) => new ViewRole(),
                nameof(AdminRole) => new AdminRole(),
                nameof(EditorRole) => new EditorRole(),
                _ => throw new Exception($"Неизвестная роль: {dto.RoleName}")
            };

            users.Add(new User(dto.Username, role));
        }

        return users;
    }
}
