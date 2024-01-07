using ProniaApi.Application.DTOs.Tokens;
using ProniaApi.Application.DTOs.Users;

namespace ProniaApi.Application.Abstractions.Services
{
	public interface IAuthService
	{
		Task Register(RegisterDto registerDto);
		Task<TokenResponseDto> Login(LoginDto loginDto);
		Task<TokenResponseDto> LoginByRefreshToken(string refresh);
	}
}
