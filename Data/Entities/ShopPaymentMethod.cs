namespace Data.Entities
{
    public class ShopPaymentMethod : BaseShopDictionaryEntity
    {
        public Guid PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public double? Сommission { get; set; }
    }
}