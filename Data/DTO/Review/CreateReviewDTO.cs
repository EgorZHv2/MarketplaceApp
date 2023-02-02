using System.ComponentModel.DataAnnotations;

namespace Data.DTO.Review
{
    public class CreateReviewDTO : BaseDTO
    {
        [Required]
        public string ReviewText { get; set; }

        [Required]
        public int Score { get; set; }

        [Required]
        public Guid ShopId { get; set; }
    }
}