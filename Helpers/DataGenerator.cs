using Bogus;
using internapi.Data;
using internapi.Model;

namespace internapi.Helpers
{
    public class DataGenerator
    {
        private readonly ApplicationDbContext _db;

        public DataGenerator(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool GenerateStudent(int count)
        {
            var fakeStudents = new Faker<Student>()
                .RuleFor(r => r.Mail, f => f.Internet.Email())
                .RuleFor(r => r.Name, f => f.Person.FirstName)
                .RuleFor(r => r.Surname, f => f.Person.LastName)
                .RuleFor(r => r.Phone, f => f.Person.Phone)
                .Generate(count);

            _db.Students.AddRange(fakeStudents);
            _db.SaveChanges();
            return true;
        }
        public bool GenerateInternship(int count)
        {
            var fakeInternships = new Faker<Internship>()
                .RuleFor(r => r.CompanyId, f => f.Random.Int(1, 10))
                .RuleFor(r => r.StudentId, f => f.Random.Int(1, 20))
                .RuleFor(r => r.Position, f => f.Commerce.Department())
                .RuleFor(r => r.Start, f => f.Date.Past())
                .RuleFor(r => r.End, f => f.Date.Future())
                .Generate(count);

            _db.Internships.AddRange(fakeInternships);
            _db.SaveChanges();
            return true;
        }
        public bool GenerateManager(int count)
        {
            var fakeManagers = new Faker<Manager>()
                .RuleFor(r => r.Mail, f => f.Person.Email)
                .RuleFor(r => r.Password, f => f.Internet.Password())
                .Generate(count);

            _db.Managers.AddRange(fakeManagers);
            _db.SaveChanges();
            return true;
        }
        public bool GenerateCompany(int count)
        {
            var fakeCompanies = new Faker<Company>()
                .RuleFor(r => r.Name, f => f.Company.CompanyName())
                .Generate(count);

            _db.Companies.AddRange(fakeCompanies);
            _db.SaveChanges();
            return true;
        }
    }
}