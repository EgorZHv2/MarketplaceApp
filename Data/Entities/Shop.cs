﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Shop
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Logo { get; set; }
        public string INN { get; set; }
        public bool Blocked { get; set; }
        public Guid SellerId { get; set; }
        public User Seller { get; set; }
        public List<Review> Reviews { get; set; }
             
    }
}
