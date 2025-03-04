
internal abstract class Figure
{

    protected int x = 50;
    protected int y = 100;

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

}