using Microsoft.EntityFrameworkCore;
using MyProjectApi.Models;

namespace MyProjectApi.Data  
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets representing your tables
        public DbSet<Car> Cars { get; set; }
        public DbSet<Photo> Photos { get; set; }

        // Configure the one-to-one relationship
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}

