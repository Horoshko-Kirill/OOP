

using System.Xml.Linq;

class Program
{
    static void Main()
    {

        
        Drawer b = new Drawer();
        Triangle a = new Triangle(25);
        b.DrawTriangle(a);
        Circle c = new Circle(20);
        b.DrawCircle(c);
        Rectangle d = new Rectangle(15, 15);
        b.DrawRectangle(d);
        Heart h = new Heart(7);
        b.DrawHeart(h);

        Star s = new Star(6);
        b.DrawStar(s);
        
        Console.ReadKey();
    }
}