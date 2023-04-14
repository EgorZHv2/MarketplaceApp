using Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Product
{
    public class ExcelProductModel
    {
        public Queue<string> CategoriesNames { get; set; } = new Queue<string>();
        public int? PartNumber { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Weight { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public double? Depth { get; set; }
        public string? Country { get; set; }
        public List<string> Photos { get; set; } = new List<string>();

    }
}
