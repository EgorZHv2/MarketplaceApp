using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class FavoriteShopsModel
    {
        [Required]
        public Guid ShopId { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}