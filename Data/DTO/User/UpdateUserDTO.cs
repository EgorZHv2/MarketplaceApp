using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.User
{
   public class UpdateUserDTO:BaseUpdateDTO
    {
        public string? FirstName { get; set; }

        public  IFormFile? Photo { get; set; }
    }
}
