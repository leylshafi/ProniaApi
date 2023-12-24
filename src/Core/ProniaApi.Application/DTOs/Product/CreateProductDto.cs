namespace ProniaApi.Application.DTOs.Product
{
	public record CreateProductDto(string Name,decimal Price, string SKU, string? Description, int CategoryId,ICollection<int>?ColorIds);
}
