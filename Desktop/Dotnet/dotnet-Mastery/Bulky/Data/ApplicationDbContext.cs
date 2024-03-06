using Bulky.Models;
using Microsoft.EntityFrameworkCore;


namespace Bulky.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

     protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Category>().HasData(
        new Category { Id = 11, Name = "Web Development", DisplayOrder = 17 },
        new Category { Id = 22, Name = "Mobile App Development", DisplayOrder = 23 },
        new Category { Id = 33, Name = "Game Development", DisplayOrder = 37 },
        new Category { Id = 44, Name = "Machine Learning", DisplayOrder = 42 }
    );

    modelBuilder.Entity<Product>().HasData(
        new Product
        {
            Id = 1,
            Title = "Web Development",
            Description = "Web Development",
            ISBN = "123456",
            Author = "Cutline",
            ListPrice = 100,
            Price = 50,
            Price50 = 40,
            Price100 = 30,
            CategoryId = 11,
            ImageUrl = ""
        },
        new Product
        {
            Id = 2,
            Title = "Mobile App Development",
            Description = "Mobile App Development",
            ISBN = "456123",
            Author = "James",
            ListPrice = 1000,
            Price = 500,
            Price50 = 400,
            Price100 = 300,
            CategoryId = 22,
            ImageUrl = ""
        },
        new Product
        {
            Id = 3,
            Title = "Game Development",
            Description = "Game Development",
            ISBN = "789012",
            Author = "Gamer",
            ListPrice = 500,
            Price = 250,
            Price50 = 200,
            Price100 = 150,
            CategoryId = 33,
            ImageUrl = ""
        },
        new Product
        {
            Id = 4,
            Title = "Machine Learning",
            Description = "Machine Learning",
            ISBN = "345678",
            Author = "AI Expert",
            ListPrice = 200,
            Price = 100,
            Price50 = 80,
            Price100 = 60,
            CategoryId = 44,
            ImageUrl = ""
        }
    );

    base.OnModelCreating(modelBuilder);
}

    }


    //             public class ApplicationDbContext : DbContext
    // {
    //     public ApplicationDbContext()
    //     {  
    //     }
    //     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //     {
    //         optionsBuilder.UseSqlServer("DefaultConnection");
    //     }

    //       public DbSet<Category> Categories { get; set; } = null;

    //           protected override void OnModelCreating(ModelBuilder modelBuilder)
    //         {
    //             modelBuilder.Entity<Category>().HasData(
    //                 new Category { Id = 1, Name = "Web Development", DisplayOrder = 1 },
    //                 new Category { Id = 2, Name = "Mobile App Development", DisplayOrder = 2 },
    //                 new Category { Id = 3, Name = "Game Development", DisplayOrder = 3 },
    //                 new Category { Id = 4, Name = "Machine Learning", DisplayOrder = 4 }
    //                 );

    //                 base.OnModelCreating(modelBuilder);
    //         }
    // }

}