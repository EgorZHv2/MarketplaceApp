using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
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
        [Compare("Password",ErrorMessage ="Пароли не совпадают")]
        public string RepeatPassword { get; set; }
    }
}
