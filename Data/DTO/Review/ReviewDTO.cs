using Data.DTO.BaseDTOs;

namespace Data.DTO.Review
{
    public class ReviewDTO : BaseOutputDTO
    {
        public string ReviewText { get; set; }
        public int Score { get; set; }
        public Guid BuyerId { get; set; }
        public Guid ShopId { get; set; }
    }
}