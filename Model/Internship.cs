using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace internapi.Model
{
    public class Internship
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("CompanyId")]
        public int CompanyId { get; set; }
        public string Position { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Start { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime End { get; set; }
        [Required]
        [ForeignKey("StudentId")]
        public int StudentId { get; set; }
    }
}