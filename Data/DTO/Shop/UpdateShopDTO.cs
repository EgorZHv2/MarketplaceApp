using Data.DTO.BaseDTOs;
using Microsoft.AspNetCore.Http;

namespace Data.DTO.Shop
{
    public class UpdateShopDTO : BaseUpdateDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? INN { get; set; }
        public IFormFile? Image { get; set; }
        public List<Guid> CategoriesId { get; set; } = new List<Guid>();
        public List<Guid> TypesId { get; set; } = new List<Guid>();
        public List<CreateShopDeliveryTypeDTO> ShopDeliveryTypes { get; set; } = new List<CreateShopDeliveryTypeDTO>();
        public List<CreateShopPaymentMethodDTO> ShopPaymentMethods { get; set; } = new List<CreateShopPaymentMethodDTO>();
    }
}