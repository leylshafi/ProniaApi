using ProniaApi.Domain.Entities.Common;

namespace ProniaApi.Domain.Entities
{
    public class Category:BaseNameableEntity
    {
        public ICollection<Product>? Products { get; set; }
    }
}
