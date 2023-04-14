namespace Data.Entities
{
    public class ShopCategoryEntity : BaseShopDictionaryEntity
    {
        public Guid CategoryId { get; set; }
        public CategoryEntity Category { get; set; }
    }
}