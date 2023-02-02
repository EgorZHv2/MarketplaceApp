namespace Data.Entities
{
    public class UserFavoriteShop
    {
        public Guid ShopId { get; set; }
        public Shop Shop { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}