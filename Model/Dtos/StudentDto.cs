using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace internapi.Model
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public ICollection<Internship> Internships { get; set; }

    }
}