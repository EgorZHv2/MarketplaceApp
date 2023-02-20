using Data.Entities;
using Data.IRepositories;

namespace Data.Repositories.ShopDictionaryRepositories
{
    public class ShopPaymentMethodRepository : BaseShopDictionaryRepository<ShopPaymentMethodEntity>, IShopPaymentMethodRepository
    {
        public ShopPaymentMethodRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}