﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaApi.Application.Abstractions.Services;
using ProniaApi.Application.DTOs.Category;
using ProniaApi.Application.DTOs.Tag;

namespace ProniaApi.API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class TagsController : ControllerBase
	{
		private readonly ITagService _service;

		public TagsController(ITagService service)
		{
			_service = service;
		}
		[HttpGet]
		public async Task<IActionResult> Get(int page = 1, int take = 2)
		{
			return Ok(await _service.GetAllAsync(page, take));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

			return StatusCode(StatusCodes.Status200OK, await _service.GetAsync(id));
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromForm] CreateTagDto tagDto)
		{
			await _service.CreateAsync(tagDto);
			return StatusCode(StatusCodes.Status201Created);
		}
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromForm] UpdateTagDto tagDto)
		{
			if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
			await _service.UpdateAsync(id, tagDto);
			return NoContent();
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
			await _service.DeleteAsync(id);
			return NoContent();
		}
	}
}
