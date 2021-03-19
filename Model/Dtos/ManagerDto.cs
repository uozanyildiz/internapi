using System.ComponentModel.DataAnnotations;

namespace internapi.Model
{
    public class ManagerDto
    {
        public int Id { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}