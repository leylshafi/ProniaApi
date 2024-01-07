using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProniaApi.Application.Abstractions.Services;
using ProniaApi.Application.DTOs.Tokens;
using ProniaApi.Application.DTOs.Users;
using ProniaApi.Domain.Entities;
using ProniaApi.Domain.Enums;
using System.Security.Claims;
using System.Text;

namespace ProniaApi.Persistence.Implementations.Services
{
	public class AuthService : IAuthService
	{
		private readonly IMapper _mapper;
		private readonly UserManager<AppUser> _userManager;
		private readonly ITokenHandler _handler;
		public AuthService(IMapper mapper, UserManager<AppUser> userManager, ITokenHandler handler)
		{
			_mapper = mapper;
			_userManager = userManager;
			_handler = handler;
		}



		public async Task Register(RegisterDto registerDto)
		{
			AppUser user = await _userManager.Users.FirstOrDefaultAsync(u=>u.UserName== registerDto.UserName|| u.Email==registerDto.Email);
			if (user is not null) throw new Exception("This user already exists");
			user = _mapper.Map<AppUser>(registerDto);
			var result = await _userManager.CreateAsync(user,registerDto.Password);
			if(!result.Succeeded)
			{
				StringBuilder message = new StringBuilder();
				foreach (var item in result.Errors)
				{
					message.AppendLine(item.Description);
				}
				throw new Exception(message.ToString());
			}

			await _userManager.AddToRoleAsync(user, UserRole.Member.ToString());
		}

		public async Task<TokenResponseDto> Login(LoginDto loginDto)
		{
			AppUser user = await _userManager.FindByNameAsync(loginDto.UserNameOrEmail);
			if (user is null)
			{
				user = await _userManager.FindByEmailAsync(loginDto.UserNameOrEmail);
				if (user is null) throw new Exception("Username, Email or Password incorrect");
			}
			if (!await _userManager.CheckPasswordAsync(user, loginDto.Password)) throw new Exception("Username, Email or Password incorrect");
			
			ICollection<Claim> claims = await CreateClaims(user);
			TokenResponseDto tokenDto = _handler.CreateJwt(user, claims, 60);
			user.RefreshToken = tokenDto.RefreshToken;
			user.RefreshTokenExpiredAt = tokenDto.RefreshTokenExpiredAt;
			await _userManager.UpdateAsync(user);
			return tokenDto;
		}

		private async Task<ICollection<Claim>> CreateClaims(AppUser user)
		{
			ICollection<Claim> claims = new List<Claim>()
			{
				new Claim(ClaimTypes.NameIdentifier,user.Id),
				new Claim(ClaimTypes.Name,user.UserName),
				new Claim(ClaimTypes.GivenName,user.Name),
				new Claim(ClaimTypes.Email,user.Email),
				new Claim(ClaimTypes.Surname,user.Surname),
			};
			foreach (var role in (await _userManager.GetRolesAsync(user)))
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}

			return claims;
		}

		public async Task<TokenResponseDto> LoginByRefreshToken(string refresh)
		{
			AppUser user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refresh);
			if (user is null) throw new Exception("Not found");
			if (user.RefreshTokenExpiredAt < DateTime.UtcNow) throw new Exception("Refresh token expired");
			TokenResponseDto tokenDto = _handler.CreateJwt(user, await CreateClaims(user), 1);
			user.RefreshToken = tokenDto.RefreshToken;
			user.RefreshTokenExpiredAt = tokenDto.RefreshTokenExpiredAt;
			await _userManager.UpdateAsync(user);
			return tokenDto;
		}
	}
}
