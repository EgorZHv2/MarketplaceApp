namespace Data.Entities
{
    public class CategoryEntity : BaseDictionaryEntity
    {
        public int Tier { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public List<ShopEntity> Shops { get; set; } = new List<ShopEntity>();
        public CategoryEntity? ParentCategory { get; set; }
        public List<CategoryEntity> Categories { get; set; } = new List<CategoryEntity>();
        public List<ShopCategoryEntity> ShopCategories { get; set; } = new List<ShopCategoryEntity>();
    }
}