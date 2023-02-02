using Data.Entities;
using Data.IRepositories;

namespace Data.Repositories.ShopDictionaryRepositories
{
    public class ShopCategoryRepository : BaseShopDictionaryRepository<ShopCategory>, IShopCategoryRepository
    {
        public ShopCategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}