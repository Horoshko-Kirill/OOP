




public class Backgrounder
{

    char sym = '.';

    public void SetSymbol(char sym)
    {
        this.sym = sym;
    }

    public void Background(Figure figure)
    {
        if (figure is Circle circle)
        {
            BackCircle(circle);
        }
        else if (figure is Rectangle rectangle)
        {
            BackRectangle(rectangle);
        }
        else if (figure is Triangle triangle)
        {
            BackTriangle(triangle);
        }
        else if (figure is Heart heart)
        {
            BackHeart(heart);
        }
        else if (figure is Star star)
        {
            BackStar(star);
        }
    }


    public void Background(Figure figure, char sym)
    {

        this.sym = sym;
        

        if (figure is Circle circle)
        {
            BackCircle(circle);
        }
        else if (figure is Rectangle rectangle)
        {
            BackRectangle(rectangle);
        }
        else if (figure is Triangle triangle)
        {
            BackTriangle(triangle);
        }
        else if (figure is Heart heart)
        {
            BackHeart(heart);
        }
        else if (figure is Star star)
        {
            BackStar(star);
        }
    }


    private void BackCircle(Circle circle)
    {

        circle.Sym = sym;
        circle.Back = true;

        for (int y = -circle.A; y <= circle.A; y++)
        {
            for (int x = -circle.A * 2; x <= circle.A * 2; x += 1)
            {

                double distance = Math.Sqrt((x / 2) * (x / 2) + y * y);

                if (distance < circle.A-0.51)
                {
                    Console.SetCursorPosition(circle.X + x, circle.Y + y);
                    Console.WriteLine(circle.Sym);

                }
            }
        }

    }

    private void BackRectangle(Rectangle rectangle)
    {

        rectangle.Sym = sym;
        rectangle.Back = true;

        for (int x = -rectangle.A+1; x <= rectangle.A-1; x += 1)
        {
            for (int y = -rectangle.B / 2+1; y <= rectangle.B - rectangle.B / 2-1; y++)
            {
                Console.SetCursorPosition(rectangle.X + x, rectangle.Y + y);
                Console.WriteLine(sym);
                Console.SetCursorPosition(rectangle.X - x, rectangle.Y + y);
                Console.WriteLine(sym);
            }
        }

    }


    private void BackTriangle(Triangle triangle)
    {

        triangle.Sym = sym;
        triangle.Back = true;

        int x = 0;

        for (int y = -triangle.A / 2; y < triangle.A - triangle.A / 2; y++)
        {
            for (int i = 0; i < x; i+= 1)
            {
                Console.SetCursorPosition(triangle.X - i, triangle.Y + y);
                Console.WriteLine(sym);
                Console.SetCursorPosition(triangle.X + i, triangle.Y + y);
                Console.WriteLine(sym);
            }
            x += 2;
        }


    }

    private void BackHeart(Heart heart)
    {

        heart.Sym = sym;
        heart.Back = true;

        int x = 0;

        for (int y = heart.A; y < 2 * heart.A; y++)
        {

            for (int i = x+4; i < 4*heart.A-x; i += 1) {


                Console.SetCursorPosition(heart.X - i, heart.Y - y);
                Console.WriteLine(sym);
                Console.SetCursorPosition(heart.X + i, heart.Y - y);
                Console.WriteLine(sym);
                Console.SetCursorPosition(heart.X - 4 * heart.A + i, heart.Y - y);
                Console.WriteLine(sym);
                Console.SetCursorPosition(heart.X + 4 * heart.A - i, heart.Y - y);
                Console.WriteLine(sym);

            }
            x += 2;
        }



        Console.SetCursorPosition(heart.X - x, heart.Y - 2 * heart.A + 1);
        Console.WriteLine(".");
        Console.SetCursorPosition(heart.X + x, heart.Y - 2 * heart.A + 1);
        Console.WriteLine(".");

        for (int i = 0; i <= heart.A; i++)
        {
            for (int j = 1; j < 4 * heart.A+2; j += 1)
            {
                Console.SetCursorPosition(heart.X - 4 * heart.A+j, heart.Y - heart.A + i);
                Console.WriteLine(sym);
                Console.SetCursorPosition(heart.X + 4 * heart.A-j, heart.Y - heart.A + i);
                Console.WriteLine(sym);
            }
        }


        for (int y = 1; y <= 2 * heart.A + 1; y++)
        {
            for (int i = 0; i < x*2; i += 1)
            {
                Console.SetCursorPosition(heart.X  - i, heart.Y + y);
                Console.WriteLine(sym);
                Console.SetCursorPosition(heart.X + i, heart.Y + y);
                Console.WriteLine(sym);
            }
            x -= 1;
        }

        Console.SetCursorPosition(heart.X, heart.Y-heart.A);
        Console.WriteLine('.');
    }

    private void BackStar(Star star)
    {

        star.Sym = sym;
        star.Back = true;

        int x = 0;
        int y = star.A;

        while (y >= x / 2)
        {

            for (int i = 0; i < x; i += 1)
            {
                Console.SetCursorPosition(star.X + i, star.Y + y);
                Console.WriteLine(sym);
                Console.SetCursorPosition(star.X - i, star.Y + y);
                Console.WriteLine(sym);
                Console.SetCursorPosition(star.X + i, star.Y - y);
                Console.WriteLine(sym);
                Console.SetCursorPosition(star.X - i, star.Y - y);
                Console.WriteLine(sym);
            }
     
            x += 1;
            y -= 1;

        }

        if (y < x / 2)
        {

            for (int i = 0; i < x; i+= 1)
            {
                for (int j = 0; j <= y; j++)
                {
                    Console.SetCursorPosition(star.X + i, star.Y + j);
                    Console.WriteLine(sym);
                    Console.SetCursorPosition(star.X - i, star.Y + j);
                    Console.WriteLine(sym);
                    Console.SetCursorPosition(star.X + i, star.Y - j);
                    Console.WriteLine(sym);
                    Console.SetCursorPosition(star.X - i, star.Y - j);
                    Console.WriteLine(sym);
                }
            }

            
        }

       


        while (x < 2 * star.A)
        {

            for (int i = 0; i <= x; i += 1)
            {
                Console.SetCursorPosition(star.X + i, star.Y + y);
                Console.WriteLine(sym);
                Console.SetCursorPosition(star.X - i, star.Y + y);
                Console.WriteLine(sym);
                Console.SetCursorPosition(star.X + i, star.Y - y);
                Console.WriteLine(sym);
                Console.SetCursorPosition(star.X - i, star.Y - y);
                Console.WriteLine(sym);
            }

            x += 4;

            y--;
            

        }

    }


}
