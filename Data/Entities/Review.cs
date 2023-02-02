namespace Data.Entities
{
    public class Review : BaseEntity
    {
        public string ReviewText { get; set; }
        public int Score { get; set; }
        public Guid BuyerId { get; set; }
        public Guid ShopId { get; set; }
        public Shop Shop { get; set; }
        public User Buyer { get; set; }
    }
}