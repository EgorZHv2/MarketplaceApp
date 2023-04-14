namespace Data.Entities
{
    public class ShopPaymentMethodEntity : BaseShopDictionaryEntity
    {
        public Guid PaymentMethodId { get; set; }
        public PaymentMethodEntity PaymentMethod { get; set; }
        public double? Сommission { get; set; }
    }
}