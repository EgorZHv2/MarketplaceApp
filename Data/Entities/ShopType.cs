namespace Data.Entities
{
    public class ShopType : BaseShopDictionaryEntity
    {
        public Guid TypeId { get; set; }
        public Data.Entities.Type Type { get; set; }
    }
}