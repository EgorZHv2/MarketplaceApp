using Data.DTO.BaseDTOs;
using System.ComponentModel.DataAnnotations;

namespace Data.DTO.Auth
{
    public class LoginDTO 
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}