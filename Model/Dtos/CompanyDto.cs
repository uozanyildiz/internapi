using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace internapi.Model
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Internship> Internships { get; set; }

    }
}