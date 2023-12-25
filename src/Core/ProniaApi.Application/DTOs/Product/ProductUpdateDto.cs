namespace ProniaApi.Application.DTOs.Product
{
	public record ProductUpdateDto(string Name, decimal Price, string SKU, string? Description, int CategoryId, ICollection<int>? ColorIds);
}
