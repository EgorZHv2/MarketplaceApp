using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class ReviewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string ReviewText { get; set; }

        [Required]
        public int Score { get; set; }

        [Required]
        public Guid BuyerId { get; set; }

        [Required]
        public Guid ShopId { get; set; }
    }
}