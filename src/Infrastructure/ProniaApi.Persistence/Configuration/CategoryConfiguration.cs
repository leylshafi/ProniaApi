using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProniaApi.Domain.Entities;

namespace ProniaApi.Persistence.Configuration
{
	public class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.Property(b=>b.Name).IsRequired().HasMaxLength(50);
		}
	}
}
