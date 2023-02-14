using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Data.DTO.Filters
{
    public class ShopFilterDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Guid? Id { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? TypeId { get; set; }
        public Guid? DeliveryTypeId { get; set; }
        public Guid? PaymentMethodId { get; set; }
    }
}
