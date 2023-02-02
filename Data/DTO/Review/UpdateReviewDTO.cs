using System.ComponentModel.DataAnnotations;

namespace Data.DTO.Review
{
    public class UpdateReviewDTO : BaseUpdateDTO
    {
        [Required]
        public string ReviewText { get; set; }

        [Required]
        public int Score { get; set; }
    }
}