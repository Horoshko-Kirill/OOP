using System;
using System.Runtime.InteropServices;

public static class WindowsConsoleHelper
{
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr GetStdHandle(int nStdHandle);

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern bool SetCurrentConsoleFontEx(
        IntPtr consoleOutput,
        bool maximumWindow,
        ref CONSOLE_FONT_INFOEX consoleCurrentFont);

    private const int STD_OUTPUT_HANDLE = -11;
    private const int LF_FACESIZE = 32;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct CONSOLE_FONT_INFOEX
    {
        public uint cbSize;
        public uint nFont;
        public COORD dwFontSize;
        public int FontFamily;
        public int FontWeight;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = LF_FACESIZE)]
        public string FaceName;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct COORD
    {
        public short X;
        public short Y;
    }

    public static bool SetConsoleFont(string fontName, short fontSize)
    {
        IntPtr handle = GetStdHandle(STD_OUTPUT_HANDLE);
        if (handle == IntPtr.Zero)
        {
            return false;
        }

        CONSOLE_FONT_INFOEX fontInfo = new CONSOLE_FONT_INFOEX
        {
            cbSize = (uint)Marshal.SizeOf<CONSOLE_FONT_INFOEX>(),
            FontFamily = 54, // TrueType font
            FaceName = fontName,
            dwFontSize = new COORD { X = 8, Y = fontSize }, // X - ширина, Y - высота
            FontWeight = 400 // Normal weight
        };

        return SetCurrentConsoleFontEx(handle, false, ref fontInfo);
    }
}