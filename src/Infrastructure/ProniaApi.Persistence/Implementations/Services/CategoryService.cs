using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaApi.Application.Abstractions.Repositories;
using ProniaApi.Application.Abstractions.Services;
using ProniaApi.Application.DTOs.Category;
using ProniaApi.Domain.Entities;
using System.Collections.Generic;

namespace ProniaApi.Persistence.Implementations.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly ICategoryRepository _repository;
		private readonly IMapper _mapper;
		public CategoryService(ICategoryRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task CreateAsync(CreateCategoryDto categoryDto)
		{
			var category = _mapper.Map<Category>(categoryDto);
			await _repository.AddAsync(category);
			await _repository.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			Category category = await _repository.GetByIdAsync(id);
			if (category is null) throw new Exception("Not found");
			_repository.Delete(category);
			await _repository.SaveChangesAsync();
		}

		public async Task<ICollection<GetCategoryDto>> GetAllAsync(int page, int take)
		{
			var categories = await _repository.GetAllAsync(skip: (page - 1) * take, take: take, isTracking: false).ToListAsync();
			var result = _mapper.Map<ICollection<GetCategoryDto>>(categories);
			return result;
		}


		public async Task<GetCategoryDto> GetAsync(int id)
		{
			Category category = await _repository.GetByIdAsync(id);
			if (category is null) throw new Exception("Not found");
			return new GetCategoryDto(category.Id, category.Name);
		}

		public async Task UpdateAsync(int id, UpdateCategoryDto categoryDto)
		{
			Category category = await _repository.GetByIdAsync(id);
			if (category is null) throw new Exception("Not found");
			category.Name = categoryDto.Name;
			_repository.Update(category);
			await _repository.SaveChangesAsync();
		}
	}
}
