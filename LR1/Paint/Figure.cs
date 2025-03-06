
internal abstract class Figure
{

    protected int x = 50;
    protected int y = 100;
    protected char sym = ' ';
    protected bool back = false;

    public int X
    {
        get
        {
            return x;
        }

        set
        {
            x = value;            
        }
    }

    public int Y
    {
        get
        {
            return y;
        }

        set
        {
            y = value;
        }
    }

    public char Sym
    {
        get
        {
            return sym;
        }

        set
        {
            sym = value;
        }
    }

    public bool Back
    {
        get
        {
            return back;
        }

        set
        {
            back = value;
        }
    }

}