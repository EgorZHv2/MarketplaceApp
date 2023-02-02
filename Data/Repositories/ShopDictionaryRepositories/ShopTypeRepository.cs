using Data.Entities;
using Data.IRepositories;

namespace Data.Repositories.ShopDictionaryRepositories
{
    public class ShopTypeRepository : BaseShopDictionaryRepository<ShopType>, IShopTypeRepository
    {
        public ShopTypeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}