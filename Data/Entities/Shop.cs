namespace Data.Entities
{
    public class Shop : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string INN { get; set; }
        public bool Blocked { get; set; }
        public Guid SellerId { get; set; }
        public User Seller { get; set; }
        public List<Review> Reviews { get; set; }
        public List<User> Users { get; set; } = new List<User>();
        public List<UserFavoriteShop> UserFavoriteShops { get; set; } = new List<UserFavoriteShop>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<DeliveryType> DeliveryTypes { get; set; } = new List<DeliveryType>();
        public List<Type> Types { get; set; } = new List<Type>();
        public List<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();
        public List<ShopDeliveryType> ShopDeliveryTypes { get; set; } = new List<ShopDeliveryType>();
        public List<ShopPaymentMethod> ShopPaymentMethods { get; set; } = new List<ShopPaymentMethod>();
        public List<ShopCategory> ShopCategories { get; set; } = new List<ShopCategory>();
        public List<ShopType> ShopTypes { get; set; } = new List<ShopType>();
    }
}