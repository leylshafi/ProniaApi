namespace ProniaApi.Application.DTOs.Tokens
{
	public record TokenResponseDto(string Token, DateTime ExpireTime, string UserName,string RefreshToken, DateTime RefreshTokenExpiredAt);
}
