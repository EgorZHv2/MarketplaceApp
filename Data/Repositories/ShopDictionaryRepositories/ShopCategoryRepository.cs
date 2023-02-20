using Data.Entities;
using Data.IRepositories;

namespace Data.Repositories.ShopDictionaryRepositories
{
    public class ShopCategoryRepository : BaseShopDictionaryRepository<ShopCategoryEntity>, IShopCategoryRepository
    {
        public ShopCategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}