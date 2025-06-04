using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StudentValidator
{
    public void Validate(StudentDTO studentDto)
    {
        if (string.IsNullOrWhiteSpace(studentDto.Name))
            throw new ArgumentException("Имя студента не может быть пустым");

        if (studentDto.Grade < 0 || studentDto.Grade > 10)
            throw new ArgumentException("Оценка должна быть между 0 и 10");
    }
}
