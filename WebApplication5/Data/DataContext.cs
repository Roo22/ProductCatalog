using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplication5.Domain;
using WebApplication5.Models;

namespace WebApplication5.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions options) : base(options) 
        {
        
        
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Category>()
                .HasData(
                 new Category { Id = 1, Name = "Category 1" },
                 new Category { Id = 2, Name = "Category 2" }
                );
            builder.Entity<Product>()
                .HasData(
                new Product
                {
                    Id = 1,
                    Name = "Product 1",
                    Description = "This Product 1 description",
                    Price = 35,
                    Duration = 5000,
                    ImageURL = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8M3x8cHJvZHVjdHxlbnwwfHwwfHx8MA%3D%3D&w=1000&q=80",
                    CreatedDate = DateTime.Now,
                    StartDate = DateTime.Now.AddDays(1),
                    CategoryId = 1,
                    UserId = 1,
                },
                 new Product
                 {
                     Id = 2,
                     Name = "Product 2",
                     Description = "This Product 2 description",
                     Price = 50,
                     Duration = 2000,
                     ImageURL = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8M3x8cHJvZHVjdHxlbnwwfHwwfHx8MA%3D%3D&w=1000&q=80",
                     CreatedDate = DateTime.Now,
                     StartDate = DateTime.Now.AddDays(2),
                     CategoryId = 2,
                     UserId = 1
                 }
                );
        }
        public DbSet<User> User { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
