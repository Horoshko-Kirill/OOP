using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

public class StudentService
{

    private readonly IStudentRepository _repository;
    private readonly StudentValidator _validator;

    public StudentService(IStudentRepository repository, StudentValidator validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public void AddStudent(StudentDTO studentDTO)
    {

        _validator.Validate(studentDTO);

        Student student = StudentFactory.Create(studentDTO);

        _repository.Add(student);

    }

    public void UpdateStudent(int Id, StudentDTO studentDto)
    {

        _validator.Validate(studentDto);

        _repository.Update(Id, studentDto);

    }

    public List<Student> GetAll()
    {
        return _repository.GetAll();
    }
}
