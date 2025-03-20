using Microsoft.EntityFrameworkCore;
using ProfilTest.Models;

namespace ProfilTest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) 
        { 
        
        }

        public DbSet<Profiles> Profiles { get; set; }
        public DbSet<Emails> Emails { get; set; }
        public DbSet<Users> User { get; set; }
    }
}
