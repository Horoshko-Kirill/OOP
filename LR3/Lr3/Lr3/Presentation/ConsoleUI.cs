using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


public class ConsoleUI
{

    private readonly Dictionary<string, ICommand> _commands;

    public ConsoleUI(Dictionary<string, ICommand> commands)
    {
        _commands = commands;
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("Меню:");
            foreach (var cmd in _commands)
            {
                Console.WriteLine($"{cmd.Key}. {cmd.Value.Name}");
            }

            string choise = Console.ReadLine();

            if (_commands.TryGetValue(choise, out var command))
            {
                command.Execute(); 
            }
            else
            {
                Console.WriteLine("Неверный выбор");
            }

            Console.WriteLine();
        }
    }

}
