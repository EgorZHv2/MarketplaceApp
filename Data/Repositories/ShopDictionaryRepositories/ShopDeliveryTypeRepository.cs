using Data.Entities;
using Data.IRepositories;

namespace Data.Repositories.ShopDictionaryRepositories
{
    public class ShopDeliveryTypeRepository : BaseShopDictionaryRepository<ShopDeliveryType>, IShopDeliveryTypeRepository
    {
        public ShopDeliveryTypeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}