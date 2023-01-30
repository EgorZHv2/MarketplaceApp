﻿using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.IRepositories
{
    public interface IReviewRepository:IBaseRepository<Review>
    {
        public IEnumerable<Review> GetReviewsByShopId(Guid ShopId);
    }
}
