using ProniaApi.Application.DTOs.Tokens;
using ProniaApi.Domain.Entities;
using System.Security.Claims;

namespace ProniaApi.Application.Abstractions.Services
{
	public interface ITokenHandler
	{
		TokenResponseDto CreateJwt(AppUser user, IEnumerable<Claim> claims, int minutes);
		string CreateRefreshToken();
	}
}
