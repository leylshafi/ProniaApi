using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProniaApi.Domain.Entities;

namespace ProniaApi.Persistence.Configuration
{
	public class TagConfiguration : IEntityTypeConfiguration<Tag>
	{
		public void Configure(EntityTypeBuilder<Tag> builder)
		{
			builder.Property(b => b.Name).IsRequired().HasMaxLength(50);
			builder.HasIndex(b => b.Name).IsUnique();
		}
	}
}
