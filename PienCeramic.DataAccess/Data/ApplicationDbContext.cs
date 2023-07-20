using Microsoft.EntityFrameworkCore;
using PienCeramic.Models;

namespace PienCeramic.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(

                new Category { Id = 1, Name = "Cup", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Plate", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Mug", DisplayOrder = 3 }

            );
        }
    }
}
