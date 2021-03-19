using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace internapi.Model
{
    public class InternshipDto
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Position { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int StudentId { get; set; }
    }
}