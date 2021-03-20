using System.Linq;
using System.Collections.Generic;
using Bogus;
using internapi.Model;
using internapi.Repository.IRepository;
using internapi.Data;
using Microsoft.EntityFrameworkCore;

namespace internapi
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public ICollection<Company> GetCompanies()
        {
            return _db.Companies.Include(c => c.Internships).ToList();
        }

        public Company GetCompany(int id)
        {
            return _db.Companies.Include(c => c.Internships).FirstOrDefault(x => x.Id == id);
        }

        public Company GetCompany(string name)
        {
            return _db.Companies.Include(c => c.Internships).FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
        }
        public bool CreateCompany(Company company)
        {
            _db.Companies.Add(company);
            return Save();
        }
        public bool CreateMultipleCompany(List<Company> companies)
        {
            _db.Companies.AddRange(companies);
            return Save();
        }

        public bool RemoveCompany(Company company)
        {
            _db.Companies.Remove(company);
            return Save();
        }

        public bool UpdateCompany(Company company)
        {
            _db.Companies.Update(company);
            return Save();
        }

        public bool CompanyExists(int id)
        {
            return _db.Companies.Any(x => x.Id == id);
        }

        public bool CompanyExists(string name)
        {
            return _db.Companies.Any(x => x.Name.ToLower() == name.ToLower());
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }


    }
}
