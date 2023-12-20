using ProniaApi.Application.DTOs.Category;

namespace ProniaApi.Application.Abstractions.Services
{
	public interface ICategoryService
	{
		Task<ICollection<GetCategoryDto>> GetAllAsync(int page, int take);
		Task<GetCategoryDto> GetAsync(int id);
		Task CreateAsync(CreateCategoryDto categoryDto);
		Task UpdateAsync(int id, UpdateCategoryDto categoryDto);
		Task DeleteAsync(int id);
	}
}
