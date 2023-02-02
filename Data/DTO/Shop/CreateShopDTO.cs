using System.ComponentModel.DataAnnotations;

namespace Data.DTO.Shop
{
    public class CreateShopDTO : BaseDTO
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string INN { get; set; }
    }
}