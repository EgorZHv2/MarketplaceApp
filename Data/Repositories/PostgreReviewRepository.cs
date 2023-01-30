﻿using Data.Entities;
using Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class PostgreReviewRepository:BaseRepository<Review>,IReviewRepository
    {
        public PostgreReviewRepository(ApplicationDbContext context):base(context) 
        {
          
        }
        public IEnumerable<Review> GetReviewsByShopId(Guid ShopId)
        {
            return _context.Reviews.Where(e=>e.ShopId == ShopId);
        } 
    }
}
