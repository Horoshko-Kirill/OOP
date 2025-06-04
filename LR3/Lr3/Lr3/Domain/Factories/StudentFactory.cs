using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StudentFactory
{

    public static Student Create(StudentDTO dto)
    {
        return new Student
        {
            Name = dto.Name,
            Grade = dto.Grade
        };
    }

}
