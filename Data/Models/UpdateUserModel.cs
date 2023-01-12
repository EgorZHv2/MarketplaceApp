using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class UpdateUserModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string RepeatPassword { get; set; }
    }
}