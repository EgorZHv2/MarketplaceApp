using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Filters
{
    public class ShopProductFilterDTO
    {
        public string? SearchQuery { get; set; }
        public Country? Country { get; set; }
        public double? MinWeight { get; set; } = 0;
        public double? MaxWeight { get; set; } = double.MaxValue;
        public double? MinWidth { get; set; } = 0;
        public double? MaxWidth { get; set; } = double.MaxValue;
        public double? MinHeight { get; set; } = 0;
        public double? MaxHeight { get; set; } = double.MaxValue;
        public double? MinDepth { get; set; } = 0;
        public double? MaxDepth { get; set; } = double.MaxValue;
        public Guid? ShopId { get; set; }
        public decimal? MinPrice { get; set; } = 0;
        public decimal? MaxPrice { get; set; } = decimal.MaxValue;
       
    }
}
