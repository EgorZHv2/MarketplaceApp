using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class ShopProductEntity:BaseShopDictionaryEntity
    {
        public Guid ProductId { get; set; }
        public ProductEntity Product { get; set; }
        public double? Quantity { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }

    }
}
