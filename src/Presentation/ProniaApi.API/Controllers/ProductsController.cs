using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProniaApi.Application.Abstractions.Services;
using ProniaApi.Application.DTOs.Product;

namespace ProniaApi.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
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
		[Authorize(Roles ="Admin")]
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
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromForm] ProductUpdateDto productDto)
		{
			if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
			await _service.UpdateAsync(id, productDto);
			return StatusCode(StatusCodes.Status204NoContent);
		}
	}
}
