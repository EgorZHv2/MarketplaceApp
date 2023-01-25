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
        public List<Guid> CategoriesId { get; set; } = new List<Guid>();
        public List<Guid> TypesId { get; set; }= new List<Guid>();
        public List<Guid> DeliveryTypesId { get; set; }= new List<Guid>();
        public List<Guid> PaymentMethodsId { get; set; }= new List<Guid>();
    }
}
