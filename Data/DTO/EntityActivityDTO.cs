using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO
{
    public class EntityActivityDTO
    {
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public Guid Id { get; set; }
    }
}
