﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class UsersFavShops:BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ShopId { get; set; }
        public Shop Shop { get; set; }
    }
}