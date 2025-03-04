

internal class Triangle : Figure
{

    private int a = 0;
    private int b = 0;

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

    public int B
    {
        get
        {
            return b;
        }

        set
        {
            if (value < 0)
            {
                b = -1;
            }
            else
            {
                b = value;
            }
        }
    }

    public Triangle(int a)
    {
        A = a;
    }

    public Triangle(int a, int b)
    {
        A = a;
        B = b;
    } 

}