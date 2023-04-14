using Data.Entities;
using Data.IRepositories;

namespace Data.Repositories.ShopDictionaryRepositories
{
    public class ShopTypeRepository : BaseShopDictionaryRepository<ShopTypeEntity>, IShopTypeRepository
    {
        public ShopTypeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}