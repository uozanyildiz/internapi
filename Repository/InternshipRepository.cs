using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using internapi.Data;
using internapi.Model;

namespace internapi.Repository.IRepository
{
    public class InternshipRepository : IInternshipRepository
    {
        private readonly ApplicationDbContext _db;
        public InternshipRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CreateInternship(Internship internship)
        {
            _db.Add(internship);
            return Save();
        }

        public bool CreateMultipleInternships(List<Internship> internships)
        {
            _db.AddRange(internships);
            return Save();
        }

        public bool DeleteInternship(Internship internship)
        {
            _db.Remove(internship);
            return Save();
        }

        public Internship GetInternship(int id)
        {
            return _db.Internships.FirstOrDefault(x => x.Id == id);
        }

        public ICollection<Internship> GetInternshipsByCompanyId(int companyId)
        {
            var internshipList = _db.Internships.Where(x => x.CompanyId == companyId).ToList();
            return internshipList;
        }

        public ICollection<Internship> GetInternshipByDate(DateTime start, DateTime end)
        {
            var internshipList = _db.Internships.Where(x => x.Start >= start && x.End <= end).OrderBy(x => x.Start).ToList(); ;
            return internshipList;
        }

        public ICollection<Internship> GetInternshipByStudentId(int studentId)
        {
            var internshipList = _db.Internships.Where(x => x.StudentId == studentId).ToList();
            return internshipList;
        }

        public ICollection<Internship> GetInternships()
        {
            return _db.Internships.OrderBy(x => x.Id).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateInternship(Internship internship)
        {
            _db.Update(internship);
            return Save();
        }

        public bool InternshipExists(int id)
        {
            return _db.Internships.Any(x => x.Id == id);
        }
    }
}