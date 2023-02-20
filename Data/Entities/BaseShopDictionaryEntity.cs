namespace Data.Entities
{
    public class BaseShopDictionaryEntity
    {
        public Guid ShopId { get; set; }
        public ShopEntity Shop { get; set; }
    }
}