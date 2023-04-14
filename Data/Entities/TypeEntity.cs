namespace Data.Entities
{
    public class TypeEntity : BaseDictionaryEntity
    {
        public string Description { get; set; }
        public List<ShopEntity> Shops { get; set; } = new List<ShopEntity>();
        public List<ShopTypeEntity> ShopTypes { get; set; } = new List<ShopTypeEntity>();
    }
}