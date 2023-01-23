using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class FavoriteShopsModel
    {
       public Guid[] ShopIds { get; set; }
    }
}