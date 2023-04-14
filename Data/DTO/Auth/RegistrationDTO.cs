using Data.Enums;
using Data.Options;
using Data.Options.Сonstants;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace Data.DTO.Auth
{
    public class RegistrationDTO
    {
        [Required]
        [EmailAddress(ErrorMessage = "Некорректный Email") ]
        public string Email { get; set; }

        [Required]
        [MinLength(8,ErrorMessage = "Пароль должен быть от 8 до 16 символов")]
        [MaxLength(16,ErrorMessage = "Пароль должен быть от 8 до 16 символов")]
        [RegularExpression(ApplicationConstants.PasswordRegex,
            ErrorMessage = "Пароль должен содержать цифры, строчные и заглавные буквы латинского алфавита, а так же спецсимволы")]
        public string Password { get; set; }

        [Required]
        public Role Role { get; set; }

        
    }
}