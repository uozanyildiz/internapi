using internapi.Model;
using Microsoft.EntityFrameworkCore;

namespace internapi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Internship> Internships { get; set; }
        public DbSet<Manager> Managers { get; set; }

    }
}