namespace Data.Entities
{
    public class UserFavoriteShopEntity
    {
        public Guid ShopId { get; set; }
        public ShopEntity Shop { get; set; }
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
    }
}