

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
            a = value;
        }
    }

    public Star(int a)
    {
        A = a;
    }
}