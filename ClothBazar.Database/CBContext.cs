using ClothBazar.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClothBazar.Database
{
    public class CBContext : DbContext
    {

        public CBContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Product>().ToTable("Products");

        }



    }
}
