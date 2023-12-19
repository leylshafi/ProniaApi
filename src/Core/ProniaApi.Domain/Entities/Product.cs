using ProniaApi.Domain.Entities.Common;

namespace ProniaApi.Domain.Entities
{
    public class Product:BaseNameableEntity
    {
        public decimal Price { get; set; }
        public string SKU { get; set; } = null!;
        public string? Description { get; set; }

        // Relational Properties
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public int ColorId { get; set; }
        public Color Color { get; set; } = null!;

        public ICollection<ProductColor>? ProductColors { get; set; }
    }
}
