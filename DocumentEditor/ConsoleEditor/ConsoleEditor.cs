using System;
using System.Collections.Generic;
using System.Text;

public class ConsoleEditor
{
    public static List<string> lines = new List<string> { "" };
    static int cursorX = 0, cursorY = 0;

    static bool selecting = false;
    static int selectionStartX = 0, selectionStartY = 0;

    const int MaxLines = 52;

    
    static Stack<EditorState> undoStack = new Stack<EditorState>();
    static Stack<EditorState> redoStack = new Stack<EditorState>();
    static bool isPerformingUndoRedo = false;


    public static List<string> StartEditor()
    {
        Console.CursorVisible = true;
        SaveState(); 

        StringBuilder screenBuffer = new StringBuilder();

        while (true)
        {
            var key = Console.ReadKey(intercept: true);
            if (key.Key == ConsoleKey.Escape) break;

            HandleKey(key);
            Redraw(screenBuffer);
        }

        return lines;
    }

    static void SaveState()
    {
        if (!isPerformingUndoRedo)
        {
            undoStack.Push(new EditorState(lines, cursorX, cursorY));
            redoStack.Clear(); 
        }
    }

    static void Undo()
    {
        if (undoStack.Count > 1) 
        {
            redoStack.Push(new EditorState(lines, cursorX, cursorY));
            var state = undoStack.Pop();
            isPerformingUndoRedo = true;
            ApplyState(state);
            isPerformingUndoRedo = false;
        }
    }

    static void Redo()
    {
        if (redoStack.Count > 0)
        {
            undoStack.Push(new EditorState(lines, cursorX, cursorY));
            var state = redoStack.Pop();
            isPerformingUndoRedo = true;
            ApplyState(state);
            isPerformingUndoRedo = false;
        }
    }

    static void ApplyState(EditorState state)
    {
        lines = state.Lines;
        cursorX = state.CursorX;
        cursorY = state.CursorY;
    }

    static void HandleKey(ConsoleKeyInfo key)
    {
        bool ctrl = (key.Modifiers & ConsoleModifiers.Control) != 0;
        bool shift = (key.Modifiers & ConsoleModifiers.Shift) != 0;

        
        if (ctrl)
        {
            if (key.Key == ConsoleKey.Z)
            {
                Undo();
                return;
            }
            else if (key.Key == ConsoleKey.Y)
            {
                Redo();
                return;
            }
            else if (key.Key == ConsoleKey.F) 
            {
                FindText();
                return;
            }
        }

        if (shift && !selecting)
        {
            selecting = true;
            selectionStartX = cursorX;
            selectionStartY = cursorY;
        }

        
        if (!isPerformingUndoRedo &&
            (key.Key != ConsoleKey.LeftArrow && key.Key != ConsoleKey.RightArrow &&
             key.Key != ConsoleKey.UpArrow && key.Key != ConsoleKey.DownArrow))
        {
            SaveState();
        }

        switch (key.Key)
        {
            case ConsoleKey.LeftArrow:
                if (cursorX > 0) cursorX--;
                else if (cursorY > 0)
                {
                    cursorY--;
                    cursorX = lines[cursorY].Length;
                }
                break;

            case ConsoleKey.RightArrow:
                if (cursorX < lines[cursorY].Length) cursorX++;
                else if (cursorY < lines.Count - 1)
                {
                    cursorY++;
                    cursorX = 0;
                }
                break;

            case ConsoleKey.UpArrow:
                if (cursorY > 0)
                {
                    cursorY--;
                    cursorX = Math.Min(cursorX, lines[cursorY].Length);
                }
                break;

            case ConsoleKey.DownArrow:
                if (cursorY < lines.Count - 1 && cursorY + 1 < MaxLines)
                {
                    cursorY++;
                    cursorX = Math.Min(cursorX, lines[cursorY].Length);
                }
                break;

            case ConsoleKey.Backspace:
                if (HasSelection())
                {
                    DeleteSelection();
                }
                else if (cursorX > 0)
                {
                    lines[cursorY] = lines[cursorY].Remove(cursorX - 1, 1);
                    cursorX--;
                }
                else if (cursorY > 0)
                {
                    cursorX = lines[cursorY - 1].Length;
                    lines[cursorY - 1] += lines[cursorY];
                    lines.RemoveAt(cursorY);
                    cursorY--;
                }
                break;

            case ConsoleKey.Delete:
                if (HasSelection())
                {
                    DeleteSelection();
                }
                else if (cursorX < lines[cursorY].Length)
                {
                    lines[cursorY] = lines[cursorY].Remove(cursorX, 1);
                }
                else if (cursorY < lines.Count - 1)
                {
                    lines[cursorY] += lines[cursorY + 1];
                    lines.RemoveAt(cursorY + 1);
                }
                break;

            case ConsoleKey.Enter:
                if (HasSelection()) DeleteSelection();

                if (lines.Count >= MaxLines) break;

                string remainder = lines[cursorY].Substring(cursorX);
                lines[cursorY] = lines[cursorY].Substring(0, cursorX);
                lines.Insert(cursorY + 1, remainder);
                cursorY++;
                cursorX = 0;
                break;

            default:
                if (!char.IsControl(key.KeyChar))
                {
                    if (HasSelection()) DeleteSelection();

                    if (lines[cursorY].Length >= 201 &&
                        cursorY == lines.Count - 1 &&
                        lines.Count >= MaxLines)
                    {
                        break;
                    }

                    lines[cursorY] = lines[cursorY].Insert(cursorX, key.KeyChar.ToString());
                    cursorX++;

                    while (lines[cursorY].Length > 201)
                    {
                        if (lines.Count >= MaxLines) break;

                        string overflow = lines[cursorY].Substring(201);
                        lines[cursorY] = lines[cursorY].Substring(0, 201);

                        if (cursorY + 1 < lines.Count)
                        {
                            lines[cursorY + 1] = overflow + lines[cursorY + 1];
                        }
                        else
                        {
                            lines.Insert(cursorY + 1, overflow);
                        }

                        if (cursorX > 201)
                        {
                            cursorY++;
                            cursorX -= 201;
                        }
                    }
                }
                break;
        }

        if (!shift)
        {
            selecting = false;
        }

        if (lines.Count >= MaxLines && cursorY == lines.Count - 1 && cursorX == lines[cursorY].Length && key.Key == ConsoleKey.Enter)
        {
            return;
        }
    }

    static bool HasSelection()
    {
        return selecting && (cursorX != selectionStartX || cursorY != selectionStartY);
    }

    static void DeleteSelection()
    {
        (int sx, int sy, int ex, int ey) = GetSelectionBounds();

        if (sy == ey)
        {
            lines[sy] = lines[sy].Remove(sx, ex - sx);
        }
        else
        {
            lines[sy] = lines[sy].Substring(0, sx) + lines[ey].Substring(ex);
            lines.RemoveRange(sy + 1, ey - sy);
        }

        cursorX = sx;
        cursorY = sy;
        selecting = false;
    }

    static void FindText()
    {
        // Сохраняем исходное состояние курсора
        bool wasCursorVisible = Console.CursorVisible;

        try
        {
            Console.CursorVisible = true;

            // Безопасная позиция для вывода сообщений
            int statusLine = Math.Min(Console.WindowHeight - 1, MaxLines);

            Console.SetCursorPosition(0, statusLine);
            Console.Write("Поиск: ");
            string searchText = Console.ReadLine();

            if (string.IsNullOrEmpty(searchText))
            {
                ClearStatusLine(statusLine);
                return;
            }

            bool found = false;
            int searchStartY = cursorY;
            int searchStartX = Math.Min(cursorX + 1, lines[cursorY].Length);

            // Ищем во всем документе (с циклическим переходом)
            for (int i = 0; i < lines.Count * 2 && !found; i++)
            {
                int y = (searchStartY + i) % lines.Count;
                int startX = (i == 0) ? searchStartX : 0;

                if (lines[y].Length == 0) continue;

                startX = Math.Min(startX, lines[y].Length - 1);
                int foundPos = lines[y].IndexOf(
                    searchText,
                    startX,
                    StringComparison.OrdinalIgnoreCase);

                if (foundPos >= 0)
                {
                    cursorY = y;
                    cursorX = foundPos;
                    found = true;
                }
            }

            if (!found)
            {
                Console.SetCursorPosition(0, statusLine);
                Console.Write($"'{searchText}' не найден".PadRight(Console.WindowWidth));
                Console.ReadKey();
            }
        }
        finally
        {
            // Восстанавливаем исходную видимость курсора
            Console.CursorVisible = wasCursorVisible;
            // Принудительно обновляем отображение курсора
            RedrawCursorPosition();
        }
    }

    // Новый метод для явного обновления позиции курсора
    static void RedrawCursorPosition()
    {
        try
        {
            Console.SetCursorPosition(
                Math.Min(cursorX, Console.WindowWidth - 1),
                Math.Min(cursorY, Console.WindowHeight - 1));
            Console.CursorVisible = true;
        }
        catch
        {
            // Защита от возможных ошибок при установке курсора
        }
    }

    static void ClearStatusLine(int line)
    {
        try
        {
            Console.SetCursorPosition(0, line);
            Console.Write(new string(' ', Console.WindowWidth));
        }
        catch
        {
            // Игнорируем ошибки очистки, чтобы не сломать основной интерфейс
        }
    }

    static (int sx, int sy, int ex, int ey) GetSelectionBounds()
    {
        if ((cursorY < selectionStartY) ||
            (cursorY == selectionStartY && cursorX < selectionStartX))
        {
            return (cursorX, cursorY, selectionStartX, selectionStartY);
        }
        return (selectionStartX, selectionStartY, cursorX, cursorY);
    }

    static void Redraw(StringBuilder screenBuffer)
    {
        screenBuffer.Clear();


        for (int y = 0; y < lines.Count && y < MaxLines; y++)
        {
            string line = lines[y];
            for (int x = 0; x < line.Length && x < Console.WindowWidth; x++)
            {
                if (IsCharSelected(x, y))
                {
                    screenBuffer.Append("\x1b[7m");
                }

                screenBuffer.Append(line[x]);

                if (IsCharSelected(x, y))
                {
                    screenBuffer.Append("\x1b[0m");
                }
            }

            screenBuffer.AppendLine();
        }

        Console.Clear();
        Console.Write(screenBuffer.ToString());
        Console.SetCursorPosition(cursorX, cursorY);
    }

    static bool IsCharSelected(int x, int y)
    {
        if (!HasSelection()) return false;
        (int sx, int sy, int ex, int ey) = GetSelectionBounds();

        if (y < sy || y > ey) return false;
        if (y == sy && y == ey) return x >= sx && x < ex;
        if (y == sy) return x >= sx;
        if (y == ey) return x < ex;
        return true;
    }
}