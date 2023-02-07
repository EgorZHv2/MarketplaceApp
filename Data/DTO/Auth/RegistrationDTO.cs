using Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Data.DTO.Auth
{
    public class RegistrationDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public Role Role { get; set; }
    }
}