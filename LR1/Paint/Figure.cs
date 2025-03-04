
internal abstract class Figure
{

    protected int x = 0;
    protected int y = 0;

    public int X
    {
        get
        {
            return x;
        }

        set
        {
            if (value < 0)
            {
                x = -1;
            }
            else
            {
                x = value;
            }
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
            if (value < 0)
            {
                y = -1;
            }
            else
            {
                y = value;
            }
        }
    }

}