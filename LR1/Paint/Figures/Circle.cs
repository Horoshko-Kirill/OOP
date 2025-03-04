

internal class Circle : Figure
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

    public Circle(int a)
    {
        A = a; 
    }


}