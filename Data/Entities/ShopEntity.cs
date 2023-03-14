namespace Data.Entities
{
    public class ShopEntity : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string INN { get; set; }
        public bool Blocked { get; set; }
        public Guid SellerId { get; set; }
        public UserEntity Seller { get; set; }
        public Guid? ImageId { get; set; }
        
        public List<ReviewEntity> Reviews { get; set; } = new List<ReviewEntity>();
        public List<UserEntity> Users { get; set; } = new List<UserEntity>();
        public List<ProductEntity> Products { get; set; } = new List<ProductEntity>();
        public List<UserFavoriteShopEntity> UserFavoriteShops { get; set; } = new List<UserFavoriteShopEntity>();
        public List<CategoryEntity> Categories { get; set; } = new List<CategoryEntity>();
        public List<DeliveryTypeEntity> DeliveryTypes { get; set; } = new List<DeliveryTypeEntity>();
        public List<TypeEntity> Types { get; set; } = new List<TypeEntity>();
        public List<PaymentMethodEntity> PaymentMethods { get; set; } = new List<PaymentMethodEntity>();
        public List<ShopDeliveryTypeEntity> ShopDeliveryTypes { get; set; } = new List<ShopDeliveryTypeEntity>();
        public List<ShopPaymentMethodEntity> ShopPaymentMethods { get; set; } = new List<ShopPaymentMethodEntity>();
        public List<ShopCategoryEntity> ShopCategories { get; set; } = new List<ShopCategoryEntity>();
        public List<ShopTypeEntity> ShopTypes { get; set; } = new List<ShopTypeEntity>();
        public List<ShopProductEntity> ShopProducts { get; set; } = new List<ShopProductEntity>();
    }
}