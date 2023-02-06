using Data.DTO.BaseDTOs;

namespace Data.DTO.Shop
{
    public class ShopDTO : BaseOutputDTO
    {
       
        public string Title { get; set; }
        public string Description { get; set; }
        public string INN { get; set; }
        public bool Blocked { get; set; }
        public Guid SellerId { get; set; }
        public string? ImagePath { get; set; }
    }
}