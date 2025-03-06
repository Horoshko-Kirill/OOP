


internal class Action
{

    private List<Figure> figures = new List<Figure>();

    private List<List<Figure>> memory = new List<List<Figure>>();

    private Drawer drawer = new Drawer();

    private Backgrounder background = new Backgrounder();  

    private Mover mover = new Mover();

    private FileManager fileManager = new FileManager();

    private Input input = new Input();

    private int index = 0;

    private int memoryIndex = -1;

    private int x = 0;

    private int y = 0;

    private int hight = 100;

    private int width = 100;

    public void Execute(int index)
    {

        if (index == 0) {

            Console.WriteLine("0 - Circle; 1 - Rectangle; 2 - Triangle; 3 - Heart; 4 - Star");

            int choose = input.CorrectInput();

            while (choose < 0 || choose > 4)
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop);

                Console.WriteLine("Number must be between 0 and 4");

                choose = input.CorrectInput();
            }

            int a = 0;
            switch (choose)
            {
                case 0:
                    Console.WriteLine("Input radius");
                    a = input.CorrectInput();
                    while (a < 0 || a > 40)
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.Write(new string(' ', Console.WindowWidth));
                        Console.SetCursorPosition(0, Console.CursorTop);

                        Console.WriteLine("Number must be between 0 and 40");

                        a = input.CorrectInput();
                    }
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Circle circle = new Circle(a);
                    figures.Add(circle);
                    AddMemory(figures);
                    drawer.Draw(circle);
                    break;
                case 1:
                    Console.WriteLine("Input height");
                    a = input.CorrectInput();
                    while (a < 0 || a > 40)
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.Write(new string(' ', Console.WindowWidth));
                        Console.SetCursorPosition(0, Console.CursorTop);

                        Console.WriteLine("Number must be between 0 and 40");

                        a = input.CorrectInput();
                    }
                    int b = 0;
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.WriteLine("Input whight");
                    b = input.CorrectInput();
                    while (b < 0 || b > 40)
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.Write(new string(' ', Console.WindowWidth));
                        Console.SetCursorPosition(0, Console.CursorTop);

                        Console.WriteLine("Number must be between 0 and 40");

                        b = input.CorrectInput();
                    }
                    Rectangle rectangle = new Rectangle(a, b);
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, Console.CursorTop);
                    figures.Add(rectangle);
                    AddMemory(figures);
                    drawer.Draw(rectangle);
                    break;
                case 2:
                    Console.WriteLine("Input size");
                    a = input.CorrectInput();
                    while (a < 0 || a > 40)
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.Write(new string(' ', Console.WindowWidth));
                        Console.SetCursorPosition(0, Console.CursorTop);

                        Console.WriteLine("Number must be between 0 and 40");

                        a = input.CorrectInput();
                    }
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Triangle triangle = new Triangle(a);
                    figures.Add(triangle);
                    AddMemory(figures);
                    drawer.Draw(triangle);
                    break;
                case 3:
                    Console.WriteLine("Input size");
                    a = input.CorrectInput();
                    while (a < 0 || a > 40)
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.Write(new string(' ', Console.WindowWidth));
                        Console.SetCursorPosition(0, Console.CursorTop);

                        Console.WriteLine("Number must be between 0 and 40");

                        a = input.CorrectInput();
                    }
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Heart heart = new Heart(a);
                    figures.Add(heart);
                    AddMemory(figures);
                    drawer.Draw(heart);
                    break;
                case 4:
                    Console.WriteLine("Input size");
                    a = input.CorrectInput();
                    while (a < 0 || a > 40)
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.Write(new string(' ', Console.WindowWidth));
                        Console.SetCursorPosition(0, Console.CursorTop);

                        Console.WriteLine("Number must be between 0 and 40");

                        a = input.CorrectInput();
                    }
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Star star = new Star(a);
                    figures.Add(star);
                    AddMemory(figures);
                    drawer.Draw(star);
                    break;
            }

        }
        else if (index == 1)
        {
            if (figures.Count > 0)
            {
                ShowList(figures);

                Console.WriteLine("Input number of figure which you wont to erase");

                int choose = input.CorrectInput();

                while (choose < 0 || choose >= figures.Count)
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, Console.CursorTop);

                    Console.WriteLine($"Number must be between 0 and {figures.Count - 1}");

                    choose = input.CorrectInput();
                }

                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop);

                figures.RemoveAt(choose);
                AddMemory(figures);


                ClearArea(x, y, hight, width);

                foreach (Figure f in figures)
                {
                    drawer.Draw(f);
                    if (f.Back)
                    {
                        background.Background(f, f.Sym);
                    }
                }
            }
            else
            {
                Console.WriteLine("Canvas does not have figures. Press key");
                Console.ReadKey();
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop);
            }

        }
        else if (index == 2)
        {
            if (figures.Count > 0)
            {
                ShowList(figures);

                Console.WriteLine("Input number of figure which you wont to move");

                int choose = input.CorrectInput();

                while (choose < 0 || choose >= figures.Count)
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, Console.CursorTop);

                    Console.WriteLine($"Number must be between 0 and {figures.Count - 1}");

                    choose = input.CorrectInput();
                }

                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop);


                int move = -1;

                Console.WriteLine("1 - left; 2 - right; 3 - up; 4 - down; 5 - exit");

                while (move != 5)
                {
                    
                    move = input.CorrectInput();


                    while (move < 1 || move > 5)
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.Write(new string(' ', Console.WindowWidth));
                        Console.SetCursorPosition(0, Console.CursorTop);

                        Console.WriteLine($"Number must be between 0 and 5");

                        move = input.CorrectInput();
                    }

                    mover.Move(figures[choose], 1, move);

                    AddMemory(figures);

                    ClearArea(x, y, hight, width);

                    foreach (Figure f in figures)
                    {
                        drawer.Draw(f);
                        if (f.Back)
                        {
                            background.Background(f, f.Sym);
                        }
                    }

                }

                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop);
            }
            else
            {
                Console.WriteLine("Canvas does not have figures. Press key");
                Console.ReadKey();
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop);
            }
        }
        else if (index == 3)
        {
            if (figures.Count > 0)
            {
                ShowList(figures);

                Console.WriteLine("Input number of figure which you wont to add a backdroung");

                int choose = input.CorrectInput();

                while (choose < 0 || choose >= figures.Count)
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, Console.CursorTop);

                    Console.WriteLine($"Number must be between 0 and {figures.Count - 1}");

                    choose = input.CorrectInput();
                }

                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop);

                Console.WriteLine("Input symbol: ");

                string sym = Console.ReadLine();

                int count = 1;
                
                while (sym.Length != 1)
                {
                    for (int i = 0; i < count; i++)
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.Write(new string(' ', Console.WindowWidth));
                        Console.SetCursorPosition(0, Console.CursorTop);
                    }
                    Console.WriteLine("Input not correct, try again");

                    count = 2;

                    sym = Console.ReadLine();
                }


                if (figures[choose] is Circle circle)
                {
                    figures.Add(circle.Clone());
                }
                else if (figures[choose] is Rectangle rectangle)
                {
                    figures.Add(rectangle.Clone());
                }
                else if (figures[choose] is Triangle triangle)
                {
                    figures.Add(triangle.Clone());
                }
                else if (figures[choose] is Heart heart)
                {
                    figures.Add(heart.Clone());
                }
                else if (figures[choose] is Star star)
                {
                    figures.Add(star.Clone());
                }

                background.Background(figures[figures.Count-1], sym[0]);
                figures.RemoveAt(choose);
                AddMemory(figures);


                ClearArea(x, y, hight, width);

                foreach (Figure f in figures)
                {
                    drawer.Draw(f);
                    if (f.Back)
                    {
                        background.Background(f, f.Sym);
                    }
                }
            }
            else
            {
                Console.WriteLine("Canvas does not have figures. Press key");
                Console.ReadKey();
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop);
            }
        }
        else if (index == 4)
        {

            List<Figure> clone = new List<Figure>();

            foreach (Figure f in figures)
            {
                if (f is Circle circle)
                {
                    clone.Add(circle.Clone());
                }
                else if (f is Rectangle rectangle)
                {
                    clone.Add(rectangle.Clone());
                }
                else if (f is Triangle triangle)
                {
                    clone.Add(triangle.Clone());
                }
                else if (f is Heart heart)
                {
                    clone.Add(heart.Clone());
                }
                else if (f is Star star)
                {
                    clone.Add(star.Clone());
                }
            }

            fileManager.InputFile(clone);
        }
        else if (index == 5)
        {

            figures = fileManager.OutputFile();
            AddMemory(figures);

            if (figures.Count > 0)
            {
                foreach (Figure f in figures)
                {
                    drawer.Draw(f);
                    if (f.Back)
                    {
                        background.Background(f, f.Sym);
                    }
                }
            }
            else
            {
                ClearArea(x, y, hight, width);
            }

        }
        else if (index == 6)
        {
            Undo();
        }
        else if (index == 7)
        {
            Redo();
        }


    }


    private void AddMemory(List<Figure> figures)
    {
        memoryIndex++;

        while(memoryIndex < memory.Count) { 
            memory.RemoveAt(memoryIndex);
        }

        List<Figure> clone = new List<Figure>();

        foreach (Figure f in figures)
        {
            if (f is Circle circle)
            {
                clone.Add(circle.Clone());
            }
            else if (f is Rectangle rectangle)
            {
                clone.Add(rectangle.Clone());
            }
            else if (f is Triangle triangle)
            {
                clone.Add(triangle.Clone());
            }
            else if (f is Heart heart)
            {
                clone.Add(heart.Clone());
            }
            else if (f is Star star)
            {
                clone.Add(star.Clone());
            }
        }

        memory.Add(clone);

        if (memory.Count > 15) {
            memory.RemoveAt(0);
        }
    }

    private void ClearArea(int x, int y, int width, int height)
    {
        for (int i = 0; i < height; i++)
        {
            Console.SetCursorPosition(x, y + i);
            Console.Write(new string(' ', width));
        }

        Console.SetCursorPosition(x, y);
    }

    private void Undo()
    {

        ClearArea(x, y, hight, width);

        if (memoryIndex > 0)
        {
            memoryIndex--;

            foreach(Figure f in memory[memoryIndex])
            {
                drawer.Draw(f);
                if (f.Back)
                {
                    background.Background(f, f.Sym);
                }
            }

        }
        else
        {
            memoryIndex = -1;
        }



    }


    private void Redo()
    {

        if (memoryIndex < memory.Count)
        {
            ClearArea(x, y, hight, width);

            if (memoryIndex + 1 <= memory.Count - 1)
            {
                memoryIndex++;
            }

            foreach (Figure f in memory[memoryIndex])
            {
                drawer.Draw(f);
                if (f.Back)
                {
                    background.Background(f, f.Sym);
                }
            }

            
            
        }

    }

    private void ShowList(List<Figure> figures)
    {
        int i = 0;

        foreach (Figure f in figures)
        {
            if (f is Circle circle)
            {
                Console.Write($"{i}. Circle ");
            }
            else if (f is Rectangle rectangle)
            {
                Console.Write($"{i}. Rectangle ");
            }
            else if (f is Triangle triangle)
            {
                Console.Write($"{i}. Triangle ");
            }
            else if (f is Heart heart)
            {
                Console.Write($"{i}. Heart ");
            }
            else if (f is Star star)
            {
                Console.Write($"{i}. Star ");
            }

            ++i;

            if (i % 5 == 0)
            {
                Console.WriteLine();
            }
        }

        if (i % 5 != 0)
        {
            Console.WriteLine();
        }
    }
} 