

public class EditorState
{
    public List<string> Lines { get; set; }
    public int CursorX { get; set; }
    public int CursorY { get; set; }

    public EditorState(List<string> lines, int cursorX, int cursorY)
    {

        Lines = new List<string>(lines.Count);
        foreach (var line in lines)
        {
            Lines.Add(string.Copy(line));
        }
        CursorX = cursorX;
        CursorY = cursorY;
    }
}