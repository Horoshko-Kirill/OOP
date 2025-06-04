using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ViewStudentsCommand : ICommand
{

    public string Name
    {
        get
        { return "Просмотреть всех студентов"; }
    }

    private readonly StudentService _studentService;

    public ViewStudentsCommand(StudentService studentService)
    {
        _studentService = studentService;
    }

    public void Execute()
    {
        var students = _studentService.GetAll();

        if (!students.Any())
        {
            Console.WriteLine("Нет студентов в системе");
            return;
        }

        foreach (var student in students)
        {
            Console.WriteLine($"ID: {student.Id}, Имя: {student.Name}, Оценка: {student.Grade}");
        }
    }
}