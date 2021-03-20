using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace internapi.Model
{
    public class Manager
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Mail { get; set; }
        [Required]
        public string Password { get; set; }
        [NotMapped]
        public string Token { get; set; }
    }
}