using Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class RegistrationModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public Role Role { get; set; }
    }
}