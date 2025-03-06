

using System.Runtime.CompilerServices;
using System.Xml.Linq;

class Program
{
    static void Main()
    {

        Action action = new Action();

        while (true)
        {
            int a = Convert.ToInt32(Console.ReadLine());
            action.Execute(a);
        }
       
       
    }
}