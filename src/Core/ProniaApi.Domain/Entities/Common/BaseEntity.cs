namespace ProniaApi.Domain.Entities.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set;}
        public string CreatedBy { get; set; } = null!;

        public BaseEntity()
        {
            CreatedAt= DateTime.Now;
            CreatedBy = "leyla.shafi";
        }
    }
}
