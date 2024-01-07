using Microsoft.AspNetCore.Identity;

namespace ProniaApi.Domain.Entities
{
	public class AppUser:IdentityUser
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public bool IsActive { get; set; }
		public string? RefreshToken { get; set; }
		public DateTime? RefreshTokenExpiredAt { get; set; }
	}
}
