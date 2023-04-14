﻿using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.IRepositories
{
    public interface IShopProductRepository:IBaseShopDictionaryRepository<ShopProductEntity>
    {
         Task<ShopProductEntity?> GetByShopAndProductIds(Guid shopId, Guid productId);
    }
}
