
using BulkyRazor.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyWebRazorApp.Data 
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
                new Category { Id = 1, Name = "Web Development", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Mobile App Development", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Game Development", DisplayOrder = 3 },
                new Category { Id = 4, Name = "Machine Learning", DisplayOrder = 4 }
            );
        }

    }


}

