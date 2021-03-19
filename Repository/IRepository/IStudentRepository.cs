using System.Collections.Generic;
using internapi.Model;

namespace internapi.Repository.IRepository
{
    public interface IStudentRepository
    {
        ICollection<Student> GetStudents();
        Student GetStudent(int id);
        Student GetStudent(string name, string surname);
        Student GetStudent(string mail);

        bool CreateStudent(Student student);
        bool UpdateStudent(Student student);
        bool RemoveStudent(Student student);
        bool CreateMultipleStudents(List<Student> students);
        bool StudentExists(int id);
        bool StudentExists(string mail);

        bool Save();
    }
}