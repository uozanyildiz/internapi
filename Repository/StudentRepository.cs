using System.Collections.Generic;
using System.Linq;
using Bogus;
using internapi.Data;
using internapi.Model;
using internapi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace internapi.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _db;
        public StudentRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CreateMultipleStudents(List<Student> students)
        {
            _db.AddRange(students);
            return Save();
        }

        public bool CreateStudent(Student student)
        {
            _db.Add(student);
            return Save();
        }

        public Student GetStudent(int id)
        {
            return _db.Students.Include(c => c.Internships).FirstOrDefault(x => x.Id == id);
        }

        public Student GetStudent(string name, string surname)
        {
            return _db.Students.Include(c => c.Internships)
                .FirstOrDefault(x => x.Name.ToLower().Trim() == name.ToLower().Trim()
                && x.Surname.ToLower().Trim() == surname.ToLower().Trim());
        }

        public Student GetStudent(string mail)
        {
            return _db.Students.Include(c => c.Internships)
                .FirstOrDefault(x => x.Mail.ToLower().Trim() == mail.ToLower().Trim());
        }

        public ICollection<Student> GetStudents()
        {
            return _db.Students.Include(c => c.Internships)
                .OrderBy(x => x.Id).ToList();
        }

        public bool RemoveStudent(Student student)
        {
            _db.Remove(student);
            return Save();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool StudentExists(int id)
        {
            return _db.Students.Any(x => x.Id == id);
        }

        public bool StudentExists(string mail)
        {
            return _db.Students.Any(x => x.Mail.ToLower().Trim() == mail.ToLower().Trim());
        }

        public bool UpdateStudent(Student student)
        {
            _db.Update(student);
            return Save();
        }
    }
}