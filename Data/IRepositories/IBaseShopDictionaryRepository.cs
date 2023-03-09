using Data.Entities;

namespace Data.IRepositories
{
    public interface IBaseShopDictionaryRepository<TEntity>
    {
        public Task CreateRange(params TEntity[] entities);

        public Task DeleteAllByShop(ShopEntity shop);
        public Task UpdateRange(params TEntity[] entities);
    }
}