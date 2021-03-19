using System;
using System.Collections;
using System.Collections.Generic;
using internapi.Model;

namespace internapi.Repository.IRepository
{
    public interface IInternshipRepository
    {
        ICollection<Internship> GetInternships();
        Internship GetInternship(int id);
        ICollection<Internship> GetInternshipsByCompanyId(int companyId);
        ICollection<Internship> GetInternshipByStudentId(int studentId);
        ICollection<Internship> GetInternshipByDate(DateTime start, DateTime end);

        bool CreateInternship(Internship internship);
        bool DeleteInternship(Internship internship);
        bool UpdateInternship(Internship internship);
        bool CreateMultipleInternships(List<Internship> internships);

        bool InternshipExists(int id);

        bool Save();

    }
}