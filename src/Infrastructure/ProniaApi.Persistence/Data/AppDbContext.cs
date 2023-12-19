using Microsoft.EntityFrameworkCore;
using ProniaApi.Domain.Entities;

namespace ProniaApi.Persistence.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Color> Colors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(p => p.Price).IsRequired().HasColumnType("decimal(6,2)");
            modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Product>().Property(p => p.Description).IsRequired(false).HasColumnType("text");
            modelBuilder.Entity<Product>().Property(p => p.SKU).IsRequired().HasMaxLength(10);
            base.OnModelCreating(modelBuilder);
        }
    }
}
