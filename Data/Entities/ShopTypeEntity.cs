namespace Data.Entities
{
    public class ShopTypeEntity : BaseShopDictionaryEntity
    {
        public Guid TypeId { get; set; }
        public TypeEntity Type { get; set; }
    }
}