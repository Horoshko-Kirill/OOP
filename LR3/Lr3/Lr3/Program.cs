

class Programm
{

    private static readonly HttpClient _httpClient = new HttpClient();
    static void Main()
    {

        var studentRepository = new JsonStudentRepository("students.json");
        var studentValidator = new StudentValidator();

        var studentService = new StudentService(studentRepository, studentValidator);

        var quoteService = new QuoteService(_httpClient);

        var commands = new Dictionary<string, ICommand>
        {
            { "1", new AddStudentCommand(studentService, quoteService) },
            { "2", new ViewStudentsCommand(studentService) },
            { "3", new EditStudentCommand(studentService) },
            { "4", new ExitCommand() }
        };

        var consoleUi = new ConsoleUI(commands);
        consoleUi.Run();
    }
}