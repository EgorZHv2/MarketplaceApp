using System.ComponentModel.DataAnnotations;

namespace Data.DTO.Auth
{
    public class LoginDTO : BaseDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}