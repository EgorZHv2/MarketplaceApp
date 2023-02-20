namespace Data.Entities
{
    public class ShopDeliveryTypeEntity : BaseShopDictionaryEntity
    {
        public Guid DeliveryTypeId { get; set; }
        public DeliveryTypeEntity DeliveryType { get; set; }
        public decimal? FreeDeliveryThreshold { get; set; }
    }
}