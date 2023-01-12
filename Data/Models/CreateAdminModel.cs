using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class CreateAdminModel
    {
        public string FirstName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}