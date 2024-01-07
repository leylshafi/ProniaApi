using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProniaApi.Domain.Entities;
using ProniaApi.Domain.Enums;

namespace ProniaApi.Persistence.Data
{
	public class AppDbContextInitializer
	{
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly UserManager<AppUser> _userManager;
		private readonly IConfiguration _config;
		private readonly AppDbContext _context;
		public AppDbContextInitializer(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, IConfiguration config, AppDbContext context)
		{
			_roleManager = roleManager;
			_userManager = userManager;
			_config = config;
			_context = context;
		}
		public async Task InitializeDbContext()
		{
			await _context.Database.MigrateAsync();
		}
		public async Task CreateRolesAsync()
		{
			foreach (UserRole role in Enum.GetValues(typeof(UserRole)))
			{
				if(!await _roleManager.RoleExistsAsync(role.ToString()))
				{
					await _roleManager.CreateAsync(new IdentityRole(role.ToString()));
				}
			}
		}

		public async Task InitializeAdmin()
		{
			AppUser admin = new()
			{
				Name = "Admin",
				Surname = "Admin",
				Email = _config["AdminSettings:Email"],
				UserName = _config["AdminSettings:Username"]
			};
			await _userManager.CreateAsync(admin, _config["AdminSettings:Password"]);
			await _userManager.AddToRoleAsync(admin, UserRole.Admin.ToString());
		}
	}
}
