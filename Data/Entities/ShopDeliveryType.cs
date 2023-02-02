namespace Data.Entities
{
    public class ShopDeliveryType : BaseShopDictionaryEntity
    {
        public Guid DeliveryTypeId { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public decimal? FreeDeliveryThreshold { get; set; }
    }
}