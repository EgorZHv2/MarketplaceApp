using Data.Enums;

namespace Data.Entities
{
    public class UserEntity : BaseEntity
    {
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string EmailConfirmationCode { get; set; }
        public Role Role { get; set; }
        public List<ShopEntity> Shops { get; set; }
        public List<ReviewEntity> Reviews { get; set; }
        public List<ShopEntity> FavoriteShops { get; set; } = new List<ShopEntity>();
        public List<UserFavoriteShopEntity> UsersFavoriteShops { get; set; } = new List<UserFavoriteShopEntity>();
    }
}