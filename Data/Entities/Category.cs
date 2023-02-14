namespace Data.Entities
{
    public class Category : BaseDictionaryEntity
    {
        public int Tier { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public List<Shop> Shops { get; set; } = new List<Shop>();
        public Category? ParentCategory { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<ShopCategory> ShopCategories { get; set; } = new List<ShopCategory>();
    }
}