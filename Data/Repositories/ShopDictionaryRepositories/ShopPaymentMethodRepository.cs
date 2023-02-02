using Data.Entities;
using Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.ShopDictionaryRepositories
{
    public class ShopPaymentMethodRepository:BaseShopDictionaryRepository<ShopPaymentMethod>,IShopPaymentMethodRepository
    {
        public ShopPaymentMethodRepository(ApplicationDbContext context) : base(context) { }
    }
}
