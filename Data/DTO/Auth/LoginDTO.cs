﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Auth
{
    public class LoginDTO:BaseDTO
    {
         [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
