using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    public class FavoriteShopsModel
    {
        [Required]
        public Guid ShopId { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
}
