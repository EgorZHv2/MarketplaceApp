using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Shop
{
    public class UpdateShopDTO:UpdateDTO
    {
      
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? INN { get; set; }
        public IFormFile? Image { get; set; }
        public List<Guid> CategoriesId { get; set; } = new List<Guid>();
        public List<Guid> TypesId { get; set; }= new List<Guid>();
        public List<CreateShopDeliveryTypeDTO> ShopDeliveryTypes { get; set; }= new List<CreateShopDeliveryTypeDTO>();
        public List<CreateShopPaymentMethodDTO> ShopPaymentMethods { get; set; }= new List<CreateShopPaymentMethodDTO>();
    }
}
