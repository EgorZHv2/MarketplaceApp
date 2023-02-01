using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.User
{
    public class CreateUserDTO:BaseDTO
    {
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } 
        public bool IsEmailConfirmed { get; set; }
        public string EmailConfirmationCode { get; set; }
        public Role Role { get; set; }
    }
}
