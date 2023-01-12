namespace Data.DTO
{
    public class ReviewDTO
    {
        public string ReviewText { get; set; }
        public int Score { get; set; }
        public Guid BuyerId { get; set; }
        public Guid ShopId { get; set; }
    }
}
