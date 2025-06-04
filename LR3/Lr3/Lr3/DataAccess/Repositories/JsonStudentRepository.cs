using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class JsonStudentRepository : IStudentRepository
{

    private readonly string _filePath;

    private List<Student> _students;

    public JsonStudentRepository(string filePath)
    {
        _filePath = filePath;
        LoadData();
    }


    public void Add(Student student)
    {
        student.Id = _students.Any() ? _students.Max(s => s.Id) + 1 : 1;
        _students.Add(student);
        SaveData();
    }

    private void LoadData()
    {
        if (File.Exists(_filePath))
        {
            var json = File.ReadAllText(_filePath);
            _students = JsonSerializer.Deserialize<List<Student>>(json);
        }
        else
        {
            _students = new List<Student>();
        }
    }

    private void SaveData()
    {
        var json = JsonSerializer.Serialize(_students);
        File.WriteAllText(_filePath, json);
    }


    public void Update(int Id, StudentDTO studentDto)
    {

        foreach (var st in _students)
        {

            if (Id == st.Id)
            {

                st.Name = studentDto.Name;
                st.Grade = studentDto.Grade;

            }

        }

    }

    public List<Student> GetAll()
    {
        return _students;
    }

}
