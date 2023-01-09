﻿using Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPi.Models
{
    public class LoginModel
    {
          [Required]
        public string Email { get; set; }
        [Required]
        public string Password{ get; set; }
    }
}
