

using System.Drawing;

internal class Rectangle : Figure
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
            a = value;
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
            b = value;
        }
    }


    public Rectangle(int a, int b)
    {
        A = a;
        B = b;
    }
}