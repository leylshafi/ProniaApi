namespace ProniaApi.Domain.Entities.Common
{
    public abstract class BaseNameableEntity:BaseEntity
    {
        public string Name { get; set; } = null!;
    }
}
