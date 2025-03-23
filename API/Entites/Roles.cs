using API.Entites.BaseEntity;
using System.ComponentModel.DataAnnotations;

namespace API.Entites
{
    public class Roles:IBaseEntity
    {
        [Key]
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public string Description {  get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
