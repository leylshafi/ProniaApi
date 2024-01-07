using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProniaApi.Application.Abstractions.Services;
using ProniaApi.Application.DTOs.Tokens;
using ProniaApi.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ProniaApi.Infrastructure.Implementations
{
	public class TokenHandler : ITokenHandler
	{
		private readonly IConfiguration _config;

		public TokenHandler(IConfiguration config)
		{
			_config = config;
		}

		public TokenResponseDto CreateJwt(AppUser user, IEnumerable<Claim> claims, int minutes)
		{
			
			SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecurityKey"]));
			SigningCredentials signin = new(securityKey, SecurityAlgorithms.HmacSha256);
			JwtSecurityToken token = new(
				issuer: _config["JWT:Issuer"],
				audience: _config["JWT:Audience"],
				claims: claims,
				notBefore: DateTime.UtcNow,
				expires: DateTime.UtcNow.AddMinutes(minutes),
				signingCredentials: signin
				);
			JwtSecurityTokenHandler handler = new();
			TokenResponseDto dto = new TokenResponseDto(handler.WriteToken(token), token.ValidTo, user.UserName, CreateRefreshToken(),token.ValidTo.AddMinutes(minutes/4));
			return dto;
		}

		public string CreateRefreshToken()
		{
			return Guid.NewGuid().ToString();
		}
	}
}
