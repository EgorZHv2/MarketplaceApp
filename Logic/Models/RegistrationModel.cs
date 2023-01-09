using Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace WebAPi.Models
{
    public class RegistrationModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public Role Role{ get; set; }
    }
}
