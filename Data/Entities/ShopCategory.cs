namespace Data.Entities
{
    public class ShopCategory : BaseShopDictionaryEntity
    {
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}