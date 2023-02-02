namespace Data.Entities
{
    public class DeliveryType : BaseDictionaryEntity
    {
        public bool CanBeFree { get; set; }
        public List<Shop> Shops { get; set; } = new List<Shop>();
        public List<ShopDeliveryType> ShopDeliveryTypes { get; set; } = new List<ShopDeliveryType>();
    }
}