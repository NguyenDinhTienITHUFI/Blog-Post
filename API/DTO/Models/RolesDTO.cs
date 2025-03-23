using API.Entites;

namespace API.DTO.Models
{
    public class RolesDTO
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public List<User> Users { get; set; } = new List<User>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
