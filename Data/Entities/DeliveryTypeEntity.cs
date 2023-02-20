namespace Data.Entities
{
    public class DeliveryTypeEntity : BaseDictionaryEntity
    {
        public bool CanBeFree { get; set; }
        public List<ShopEntity> Shops { get; set; } = new List<ShopEntity>();
        public List<ShopDeliveryTypeEntity> ShopDeliveryTypes { get; set; } = new List<ShopDeliveryTypeEntity>();
    }
}