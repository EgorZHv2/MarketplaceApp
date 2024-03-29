﻿using Data.DTO.BaseDTOs;
using Data.Enums;

namespace Data.DTO.User
{
    public class CreateUserDTO : BaseCreateDTO
    {
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string EmailConfirmationCode { get; set; }
        public Role Role { get; set; }
    }
}