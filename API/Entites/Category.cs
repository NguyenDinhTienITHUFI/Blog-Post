using API.Entites.BaseEntity;

namespace API.Entites
{
    public class Category:IBaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UrlHandle { get; set; }

        public ICollection<BlogPost> BlogPosts { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
