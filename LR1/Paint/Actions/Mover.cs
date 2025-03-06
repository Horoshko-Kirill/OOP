


internal class Mover
{

    public void Move(Figure figure, int x, int index)
    {
        switch (index)
        {
            case 1:
                figure.X -= x;
                break;
            case 2:
                figure.X += x;
                break;
            case 3:
                figure.Y -= x;
                break;
            case 4:
                figure.Y += x;
                break;
        }
    }

}