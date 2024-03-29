﻿using Data.DTO.BaseDTOs;
using Data.Entities;
using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Product
{
    public class ProductDTO:BaseOutputDTO
    {
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public int PartNumber { get; set; }
        public string Description { get; set; }
        public double Weight { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Depth { get; set; }
        public Country Country { get; set; }
        public string[]? ImagesLinks { get; set; }
       
    }
}
