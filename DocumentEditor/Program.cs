using System;
using System.Runtime.InteropServices;

class Program
{

    /*const int STD_OUTPUT_HANDLE = -11;
    const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;

    [DllImport("kernel32.dll")]
    static extern IntPtr GetStdHandle(int nStdHandle);

    [DllImport("kernel32.dll")]
    static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

    [DllImport("kernel32.dll")]
    static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

    static void EnableVirtualTerminalProcessing()
    {
        IntPtr handle = GetStdHandle(STD_OUTPUT_HANDLE);
        GetConsoleMode(handle, out uint mode);
        SetConsoleMode(handle, mode | ENABLE_VIRTUAL_TERMINAL_PROCESSING);
    }

    EnableVirtualTerminalProcessing();

    Console.WriteLine("\x1b[1mЖирный\x1b[0m");
        Console.WriteLine("\x1b[4mПодчёркнутый\x1b[0m");
        Console.WriteLine("\x1b[3mКурсив (если поддерживается)\x1b[0m");*/


    static void Main()
    {

        ConsoleEditor.StartEditor();

    }
}
