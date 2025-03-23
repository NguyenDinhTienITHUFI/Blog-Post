using API.Entites;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTO.Models
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string Usermame { get; set; }
        [StringLength(10)]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        //[RegularExpression(@"^((?=.*@)(?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Password should contain atleast 1 capital letter,atleast 1 small letter and special character @")]

        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }

        public Guid RoleId { get; set; }
        public string RoleName { get; set; }

        public List<BlogPostDTO> BlogPosts { get; set; } = new();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
