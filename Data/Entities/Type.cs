namespace Data.Entities
{
    public class Type : BaseDictionaryEntity
    {
        public string Description { get; set; }
        public List<Shop> Shops { get; set; } = new List<Shop>();
        public List<ShopType> ShopTypes { get; set; } = new List<ShopType>();
    }
}