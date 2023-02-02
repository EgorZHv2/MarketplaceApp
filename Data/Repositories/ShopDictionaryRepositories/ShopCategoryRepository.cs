using Data.Entities;
using Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.ShopDictionaryRepositories
{
    public class ShopCategoryRepository:BaseShopDictionaryRepository<ShopCategory>,IShopCategoryRepository
    {
        public ShopCategoryRepository(ApplicationDbContext context) : base(context) { }
    }
}
