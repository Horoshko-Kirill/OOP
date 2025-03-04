

internal class Triangle : Figure
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
            a = value;
        }
    }

    public Triangle(int a)
    {
        A = a;
    }


}