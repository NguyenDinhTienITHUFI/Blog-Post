using API.Entites.BaseEntity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace API.Entites
{
    public class User:IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Usermame { get; set; }
        public string PasswordHash { get; set; }
        public string Name {  get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }

        [ForeignKey("Role")]
        public Guid RoleId { get; set; }
        public Roles Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
