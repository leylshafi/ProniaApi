using ProniaApi.Domain.Entities.Common;

namespace ProniaApi.Domain.Entities
{
	public class Tag:BaseNameableEntity
	{
		public ICollection<ProductTag>? ProductTags { get; set; }
	}
}
