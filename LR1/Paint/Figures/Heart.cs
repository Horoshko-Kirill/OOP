

internal class Heart : Figure
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

    public Heart(int a)
    {
        A = a;
    }
}