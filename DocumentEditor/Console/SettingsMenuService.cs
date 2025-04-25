public class SettingsMenuService
{
    private readonly EditorSettings _settings;

    public SettingsMenuService()
    {
        _settings = EditorSettings.Instance;
    }

    public void ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            DisplayMenu();

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    ChangeTheme();
                    break;
                case ConsoleKey.Escape:
                    return;
            }
        }
    }

    private void DisplayMenu()
    {
        Console.WriteLine("=== НАСТРОЙКИ РЕДАКТОРА ===");
        Console.WriteLine("1.Изменить тему оформления");
        Console.WriteLine("ESC. Назад\n");
    }

    private void ChangeTheme()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== ВЫБЕРИТЕ ТЕМУ ===");
            Console.WriteLine("1. Светлая (чёрный на белом)");
            Console.WriteLine("2. Тёмная (белый на чёрном)");
            Console.WriteLine("3. Классический терминал (зелёный на чёрном)");
            Console.WriteLine("4. Solarized Light");
            Console.WriteLine("5. Голубая (синий на тёмно-синем)");
            Console.WriteLine("6. Жёлтая (для чтения)");
            Console.WriteLine("0. Назад");

            var key = Console.ReadKey(true);
            switch (key.KeyChar)
            {
                case '1':
                    _settings.ApplyTheme(ConsoleColor.White, ConsoleColor.Black);
                    return;
                case '2':
                    _settings.ApplyTheme(ConsoleColor.Black, ConsoleColor.White);
                    return;
                case '3':
                    _settings.ApplyTheme(ConsoleColor.Black, ConsoleColor.Green);
                    return;
                case '4':
                    _settings.ApplyTheme(
                        ParseColor("#fdf6e3"),  // кремовый
                        ParseColor("#657b83")); // серо-синий
                    return;
                case '5':
                    _settings.ApplyTheme(
                        ConsoleColor.DarkBlue,
                        ConsoleColor.Cyan);
                    return;
                case '6':
                    _settings.ApplyTheme(
                        ConsoleColor.Yellow,
                        ConsoleColor.DarkBlue);
                    return;
                case '0':
                    return;
            }
        }
    }

    // Вспомогательный метод для преобразования HEX в ConsoleColor
    private ConsoleColor ParseColor(string hexColor)
    {
        try
        {
            var color = System.Drawing.ColorTranslator.FromHtml(hexColor);
            return ClosestConsoleColor(color.R, color.G, color.B);
        }
        catch
        {
            return ConsoleColor.Gray; // fallback
        }
    }

    // Находит ближайший ConsoleColor для RGB
    private ConsoleColor ClosestConsoleColor(byte r, byte g, byte b)
    {
        ConsoleColor closest = ConsoleColor.Black;
        double closestDiff = double.MaxValue;

        foreach (ConsoleColor color in Enum.GetValues(typeof(ConsoleColor)))
        {
            var colorName = color.ToString();
            if (colorName == "DarkYellow") continue; // Игнорируем проблемный цвет

            var consoleColor = System.Drawing.Color.FromName(
                colorName == "DarkYellow" ? "Orange" : colorName);

            double diff = Math.Pow(consoleColor.R - r, 2) +
                          Math.Pow(consoleColor.G - g, 2) +
                          Math.Pow(consoleColor.B - b, 2);

            if (diff < closestDiff)
            {
                closestDiff = diff;
                closest = color;
            }
        }

        return closest;
    }

}