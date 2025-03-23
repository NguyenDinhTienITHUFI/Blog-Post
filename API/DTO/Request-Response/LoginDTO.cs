using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class LoginDTO
    {
        [Required]
        [StringLength(20)]
        public string UserName { get; set; }
               public string Password { get; set; }
    }
}
