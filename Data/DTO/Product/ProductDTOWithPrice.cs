using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Product
{
    public class ProductDTOWithPrice:ProductDTO
    {
        public decimal Price { get; set; }   
    }
}
