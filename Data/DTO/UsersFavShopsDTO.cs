using Data.Entities;

namespace Data.DTO
{
    public class UsersFavShopsDTO
    {
        public Guid UserId { get; set; }
       
        public Guid ShopId { get; set; }
    }
}
