using Data.Entities;
using Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.ShopDictionaryRepositories
{
    public class ShopDeliveryTypeRepository:BaseShopDictionaryRepository<ShopDeliveryType>,IShopDeliveryTypeRepository
    {
        public ShopDeliveryTypeRepository(ApplicationDbContext context) : base(context) { }
    }
}
