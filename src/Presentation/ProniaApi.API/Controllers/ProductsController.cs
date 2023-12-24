using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaApi.Application.Abstractions.Services;
using ProniaApi.Application.DTOs.Category;
using ProniaApi.Application.DTOs.Product;

namespace ProniaApi.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IProductService _service;

		public ProductsController(IProductService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> Get(int page = 1, int take = 2)
		{
			return Ok(await _service.GetAllPaginated(page, take));
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			return Ok(await _service.GetByIsAsync(id));
		}
		[HttpPost]
		public async Task<IActionResult> Create([FromForm] CreateProductDto productDto)
		{
			await _service.CreateAsync(productDto);
			return StatusCode(StatusCodes.Status201Created);
		}
	}
}
