using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class UpdateShopModel
    {
        [Required]
        public Guid Id { get; set; }       
        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? INN { get; set; }

        public IFormFile? Image { get; set; }

         public Guid[] CategoriesId { get; set; } 
        public Guid[] TypesId { get; set; }
        public Guid[] DeliveryTypesId { get; set; }
        public Guid[] PaymentMethodsId { get; set; }
    }
}
