namespace Data.Entities
{
    public class PaymentMethodEntity : BaseDictionaryEntity
    {
        public bool HasCommission { get; set; }
        public List<ShopEntity> Shops { get; set; } = new List<ShopEntity>();
        public List<ShopPaymentMethodEntity> ShopPaymentMethods { get; set; } = new List<ShopPaymentMethodEntity>();
    }
}