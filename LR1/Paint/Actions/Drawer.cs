


using System.Security.Cryptography;

internal class Drawer
{
    public void Draw(Figure figure)
    {
        if (figure is Circle circle)
        {
            DrawCircle(circle);
        }
        else if (figure is Rectangle rectangle) 
        {
            DrawRectangle(rectangle);
        }
        else if (figure is Triangle triangle)
        {
            DrawTriangle(triangle);
        }
        else if (figure is Heart heart)
        {
            DrawHeart(heart);
        }
        else if (figure is Star star)
        {
            DrawStar(star);
        }
    }


    private void DrawCircle(Circle circle)
    {

        for (int y = -circle.A; y <= circle.A; y++)
        {
            for (int x = -circle.A*2; x <= circle.A*2; x += 2)
            {
                   
                double distance = Math.Sqrt((x/2) * (x/2) + y * y);

                if (Math.Abs(distance - circle.A) < 0.5)
                {
                    Console.SetCursorPosition(circle.X+x, circle.Y+y);
                    Console.WriteLine('.');
          
                }
            }
        }

    }

    private void DrawRectangle(Rectangle rectangle)
    {

        for (int x = -rectangle.A; x <= rectangle.A; x += 2)
        {
            Console.SetCursorPosition(rectangle.X + x, rectangle.Y - rectangle.B/2);
            Console.WriteLine('.');
            Console.SetCursorPosition(rectangle.X + x, rectangle.Y + rectangle.B - rectangle.B / 2);
            Console.WriteLine('.');
        }


        for (int y = -rectangle.B/2;  y <= rectangle.B - rectangle.B/2; y++)
        {
            Console.SetCursorPosition(rectangle.X + rectangle.A, rectangle.Y + y);
            Console.WriteLine('.');
            Console.SetCursorPosition(rectangle.X - rectangle.A, rectangle.Y + y);
            Console.WriteLine('.');
        }

    }


    private void DrawTriangle(Triangle triangle)
    {

        int x = 0;

        for (int y = -triangle.A/2; y <= triangle.A-triangle.A/2; y++)
        {
            Console.SetCursorPosition(triangle.X - x, triangle.Y + y);
            Console.WriteLine('.');
            Console.SetCursorPosition(triangle.X + x, triangle.Y + y);
            Console.WriteLine('.');
            Console.SetCursorPosition(triangle.X - x, triangle.Y + triangle.A / 2 + triangle.A%2);
            Console.WriteLine('.');
            Console.SetCursorPosition(triangle.X + x, triangle.Y + triangle.A / 2 + triangle.A % 2);
            Console.WriteLine('.');
            x += 2;
        }


    }

    private void DrawHeart(Heart heart)
    {
        int x = 0;

        for(int y = heart.A; y < 2*heart.A; y++)
        {
            Console.SetCursorPosition(heart.X - x, heart.Y - y);
            Console.WriteLine(".");
            Console.SetCursorPosition(heart.X + x, heart.Y - y);
            Console.WriteLine(".");
            Console.SetCursorPosition(heart.X - 4*heart.A + x, heart.Y - y);
            Console.WriteLine(".");
            Console.SetCursorPosition(heart.X + 4*heart.A - x, heart.Y - y);
            Console.WriteLine(".");
            x += 2;
        }
           
        Console.SetCursorPosition(heart.X - x, heart.Y - 2*heart.A+1);
        Console.WriteLine(".");
        Console.SetCursorPosition(heart.X + x, heart.Y -2 * heart.A + 1);
        Console.WriteLine(".");

        for (int i = 0; i <= heart.A; i++)
        {
            Console.SetCursorPosition(heart.X - 4 * heart.A, heart.Y - heart.A+i);
            Console.WriteLine(".");
            Console.SetCursorPosition(heart.X + 4 * heart.A, heart.Y - heart.A  +i);
            Console.WriteLine(".");
        }


        for (int y = 1; y <= 2*heart.A+1; y++)
        {
            Console.SetCursorPosition(heart.X - 2*heart.A - x, heart.Y + y);
            Console.WriteLine(".");
            Console.SetCursorPosition(heart.X + 2 * heart.A + x, heart.Y + y);
            Console.WriteLine(".");
            x -= 2;
        }
    }

    private void DrawStar(Star star)
    {
        
        int x = 0;
        int y = star.A;

        while (y > x/2)
        {
            Console.SetCursorPosition(star.X + x, star.Y + y);
            Console.WriteLine(".");
            Console.SetCursorPosition(star.X - x, star.Y + y);
            Console.WriteLine(".");
            Console.SetCursorPosition(star.X + x, star.Y - y);
            Console.WriteLine(".");
            Console.SetCursorPosition(star.X - x, star.Y - y);
            Console.WriteLine(".");

            x += 2;
            y -= 2;

        }

       
        if (y < x / 2)
        {
            y++;
            Console.SetCursorPosition(star.X + x, star.Y + y);
            Console.WriteLine(".");
            Console.SetCursorPosition(star.X - x, star.Y + y);
            Console.WriteLine(".");
            Console.SetCursorPosition(star.X + x, star.Y - y);
            Console.WriteLine(".");
            Console.SetCursorPosition(star.X - x, star.Y - y);
            Console.WriteLine(".");
            y--;
            if ((star.A - 7)%3 == 0)
            {
                x += 4;
            }
            else
            {
                x += 2;
            }
            
          
        }

        while(x <= 2 * star.A)
        {
            Console.SetCursorPosition(star.X + x, star.Y + y);
            Console.WriteLine(".");
            Console.SetCursorPosition(star.X - x, star.Y + y);
            Console.WriteLine(".");
            Console.SetCursorPosition(star.X + x, star.Y - y);
            Console.WriteLine(".");
            Console.SetCursorPosition(star.X - x, star.Y - y);
            Console.WriteLine(".");
            y--;
            x += 4;
        }

    }

}