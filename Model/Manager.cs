using System.ComponentModel.DataAnnotations;

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
    }
}