using System.Collections.Generic;
using internapi.Model;

namespace internapi.Repository.IRepository
{
    public interface ICompanyRepository
    {
        ICollection<Company> GetCompanies();
        Company GetCompany(int id);
        Company GetCompany(string name);

        bool CreateCompany(Company company);
        bool CreateMultipleCompany(List<Company> companies);
        bool UpdateCompany(Company company);
        bool RemoveCompany(Company company);
        bool CompanyExists(int id);
        bool CompanyExists(string name);

        bool Save();
    }
}