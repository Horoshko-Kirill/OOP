using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IStudentRepository
{
    public void Add(Student student);
    public void Update(int Id, StudentDTO studentDto);

    public List<Student> GetAll();

}
