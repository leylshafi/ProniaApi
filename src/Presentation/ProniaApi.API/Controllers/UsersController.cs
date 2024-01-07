using Microsoft.AspNetCore.Mvc;
using ProniaApi.Application.Abstractions.Services;
using ProniaApi.Application.DTOs.Users;

namespace ProniaApi.API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IAuthService _service;

		public UsersController(IAuthService service)
		{
			_service = service;
		}

		[HttpPost]
		public async Task<IActionResult> Register([FromForm]RegisterDto registerDto)
		{
			await _service.Register(registerDto);
			return NoContent();
		}

		[HttpPost("[Action]")]
		public async Task<IActionResult> Login([FromForm] LoginDto loginDto)
		{
			return Ok(await _service.Login(loginDto));
		}

		[HttpPost("[Action]")]
		public async Task<IActionResult> LoginByRefresh(string refreshToken)
		{
			return Ok(await _service.LoginByRefreshToken(refreshToken));
		}
	}
}
