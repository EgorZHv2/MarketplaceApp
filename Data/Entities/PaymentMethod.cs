namespace Data.Entities
{
    public class PaymentMethod : BaseDictionaryEntity
    {
        public bool HasCommission { get; set; }
        public List<Shop> Shops { get; set; } = new List<Shop>();
        public List<ShopPaymentMethod> ShopPaymentMethods { get; set; } = new List<ShopPaymentMethod>();
    }
}