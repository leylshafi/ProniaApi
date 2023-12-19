using ProniaApi.Domain.Entities.Common;

namespace ProniaApi.Domain.Entities
{
    public class Color:BaseNameableEntity
    {
        public ICollection<ProductColor>? ProductColors { get; set; }
    }
}
