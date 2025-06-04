using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EditStudentCommand : ICommand
{
    public string Name
    {
        get
        { return "Редактировать студента"; }
    }

    private readonly StudentService _studentService;

    public EditStudentCommand(StudentService studentService)
    {
        _studentService = studentService;
    }

    public void Execute()
    {
        try { 
            Console.Write("Введите ID студента для редактирования: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("Неверный ID");
                return;
            }

            Console.Write("Введите новое имя ");
            var name = Console.ReadLine();

            Console.Write("Введите новую оценку ");
            if (!int.TryParse(Console.ReadLine(), out var grade))
            {
                throw new ArgumentException("Оценка должна быть числом");
            }

            StudentDTO studentDTO = new StudentDTO();
        
            studentDTO.Grade = grade;
            studentDTO.Name = name;

            _studentService.UpdateStudent(id, studentDTO);
            Console.WriteLine("Студент успешно обновлен");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}
