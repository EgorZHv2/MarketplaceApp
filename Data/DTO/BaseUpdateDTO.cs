using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO
{
    public class BaseUpdateDTO
    {
        [Required]
        public Guid Id { get; set; }      
    }
}
