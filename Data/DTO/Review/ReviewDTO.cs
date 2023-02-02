namespace Data.DTO.Review
{
    public class ReviewDTO : BaseDTO
    {
        public Guid Id { get; set; }
        public string ReviewText { get; set; }
        public int Score { get; set; }
        public Guid BuyerId { get; set; }
        public Guid ShopId { get; set; }
    }
}