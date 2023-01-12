using Data.Enums;

namespace Data.DTO
{
    public class UserDTO
    { 
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } 
        public bool IsEmailConfirmed { get; set; }
        public string EmailConfirmationCode { get; set; }
        public Role Role { get; set; }
    }
}
