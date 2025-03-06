

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

    public Triangle Clone()
    {
        return new Triangle(a)
        {
            X = this.X,
            Y = this.Y,
            Sym = this.Sym,
            Back = this.Back
        };
    }

}