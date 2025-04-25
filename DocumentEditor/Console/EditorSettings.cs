using System;
using System.Runtime.InteropServices;

public sealed class EditorSettings
{
    private static readonly Lazy<EditorSettings> _instance = new Lazy<EditorSettings>(() => new EditorSettings());
    public static EditorSettings Instance => _instance.Value;

    public ConsoleColor BackgroundColor { get; private set; } = ConsoleColor.Black;
    public ConsoleColor TextColor { get; private set; } = ConsoleColor.White;
    public short FontSize { get; private set; } = 16;

    private EditorSettings() { }

    public void ApplyTheme(ConsoleColor bg, ConsoleColor text)
    {
        BackgroundColor = bg;
        TextColor = text;
        UpdateConsoleAppearance();
    }

    public void SetFontSize(short size)
    {
        FontSize = Math.Clamp(size, (short)8, (short)36);
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            WindowsConsoleHelper.SetConsoleFont("Consolas", FontSize);
        }
        else
        {
            Console.WriteLine("Изменение размера шрифта поддерживается только в Windows");
        }
    }

    private void UpdateConsoleAppearance()
    {
        Console.BackgroundColor = BackgroundColor;
        Console.ForegroundColor = TextColor;
        Console.Clear();
    }

    
    
}