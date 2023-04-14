namespace Data.Entities
{
    public class ReviewEntity : BaseEntity
    {
        public string ReviewText { get; set; }
        public int Score { get; set; }
        public Guid BuyerId { get; set; }
        public Guid ShopId { get; set; }
        public ShopEntity Shop { get; set; }
        public UserEntity Buyer { get; set; }
    }
}