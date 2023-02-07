using Data.DTO.BaseDTOs;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Data.DTO.Shop
{
    public class CreateShopDTO : BaseCreateDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string INN { get; set; }
        public IFormFile? Image { get; set; }
        public List<Guid> CategoriesId { get; set; } = new List<Guid>();
        public List<Guid> TypesId { get; set; } = new List<Guid>();
        public List<CreateShopDeliveryTypeDTO> ShopDeliveryTypes { get; set; } = new List<CreateShopDeliveryTypeDTO>();
        public List<CreateShopPaymentMethodDTO> ShopPaymentMethods { get; set; } = new List<CreateShopPaymentMethodDTO>();
    }
}