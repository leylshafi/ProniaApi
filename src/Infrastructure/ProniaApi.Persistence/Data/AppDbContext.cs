using Microsoft.EntityFrameworkCore;
using ProniaApi.Domain.Entities;
using ProniaApi.Domain.Entities.Common;
using ProniaApi.Persistence.Common;
using System.Reflection;

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
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyQueryFilters();
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
            var entites = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in entites)
            {
                switch (data.State)
                {
                    case EntityState.Modified:
                        data.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Added:
                        data.Entity.CreatedAt= DateTime.UtcNow;
                        break;
                    default:
                        break;
                }
            }
			return base.SaveChangesAsync(cancellationToken);
		}
	}
}
