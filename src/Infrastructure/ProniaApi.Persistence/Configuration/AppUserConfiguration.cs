﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProniaApi.Domain.Entities;

namespace ProniaApi.Persistence.Configuration
{
	public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
	{
		public void Configure(EntityTypeBuilder<AppUser> builder)
		{
			builder.Property(x => x.UserName).IsRequired().HasMaxLength(50);
			builder.Property(x=>x.Email).IsRequired().HasMaxLength(256);
			builder.Property(x=>x.Name).IsRequired().HasMaxLength(50);
			builder.Property(x=>x.Surname).IsRequired().HasMaxLength(50);
		}
	}
}
