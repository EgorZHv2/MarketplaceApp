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
        
        public string? SearchQuery { get; set; }
        public decimal? MinPrice { get; set; } = 0;
        public decimal? MaxPrice { get; set; } = decimal.MaxValue;
        public Guid? ProductId { get; set; }
        public List<Guid> CategoriesIds { get; set; } = new List<Guid>();
        public List<Guid> TypesIds { get; set; } = new List<Guid>();
        public List<Guid> DeliveryTypesIds { get; set; } = new List<Guid>();
        public List<Guid> PaymentMethodsIds { get; set; }= new List<Guid>();
    }
}
