using System.ComponentModel.DataAnnotations;

namespace Data.DTO
{
    public class CreateAdminDTO
    {
        public string FirstName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}