using Data.Entities;
using Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.ShopDictionaryRepositories
{
    public class ShopProductRepository:BaseShopDictionaryRepository<ShopProductEntity>,IShopProductRepository
    {
        public ShopProductRepository(ApplicationDbContext context) : base(context) { }

        public async Task<ShopProductEntity> GetByShopAndProductIds(Guid shopId,Guid productId)
        {
            return await _dbSet.FirstOrDefaultAsync(e=> e.ShopId == shopId && e.ProductId == productId);
        }
    }
}
