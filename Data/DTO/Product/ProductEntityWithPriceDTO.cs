using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Product
{
    public class ProductEntityWithPriceDTO
    {
        public ProductEntity Product { get; set; }
        public decimal Price { get; set; }
       
    }
}
