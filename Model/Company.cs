using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace internapi.Model
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Internship> Internships { get; set; }

    }
}