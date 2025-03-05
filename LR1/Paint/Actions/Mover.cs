


internal class Mover
{

    public void Move(Figure figure, int x, int index)
    {
        switch (index)
        {
            case 0:
                figure.X -= x;
                break;
            case 1:
                figure.X += x;
                break;
            case 2:
                figure.Y += x;
                break;
            case 3:
                figure.Y += x;
                break;
        }
    }

}