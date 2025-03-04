

internal class Star : Figure
{
    private int a = 0;

    public int A
    {
        get
        {
            return a;
        }

        set
        {
            if (value < 0)
            {
                a = -1;
            }
            else
            {
                a = value;
            }
        }
    }

    public Star(int a)
    {
        A = a;
    }
}