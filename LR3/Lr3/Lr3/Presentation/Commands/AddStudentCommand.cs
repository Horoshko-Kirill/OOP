using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AddStudentCommand : ICommand
{

    public string Name
    {
        get
        { return "Добавить студента"; }
    }

    private StudentService _studentService;
    private QuoteService _quoteService;

    public AddStudentCommand(StudentService studentService, QuoteService quoteService)
    {
        _studentService = studentService;
        _quoteService = quoteService;
    }


    public void Execute()
    {

        try
        {
            Console.Write("Введите имя студента: ");
            var name = Console.ReadLine();

            Console.Write("Введите оценку: ");
            if (!int.TryParse(Console.ReadLine(), out var grade))
            {
                throw new ArgumentException("Оценка должна быть числом");
            }

            var studentDto = new StudentDTO { Name = name, Grade = grade };
            _studentService.AddStudent(studentDto);


            var quote = _quoteService.GetQuote();

            Console.WriteLine($"Студент добавлен! Мотивационная цитата: {quote.Content} - {quote.Author}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}
