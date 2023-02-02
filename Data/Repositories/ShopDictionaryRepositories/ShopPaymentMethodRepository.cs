using Data.Entities;
using Data.IRepositories;

namespace Data.Repositories.ShopDictionaryRepositories
{
    public class ShopPaymentMethodRepository : BaseShopDictionaryRepository<ShopPaymentMethod>, IShopPaymentMethodRepository
    {
        public ShopPaymentMethodRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}