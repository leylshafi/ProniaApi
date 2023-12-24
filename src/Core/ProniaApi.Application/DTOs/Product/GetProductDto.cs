using ProniaApi.Application.DTOs.Category;

namespace ProniaApi.Application.DTOs.Product
{
	public record GetProductDto(decimal Price,string SKU,string? Description,int CategoryId,IncludeCategoryDto Category);
}

