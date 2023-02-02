using Data.Entities;

namespace Data.IRepositories
{
    public interface IBaseShopDictionaryRepository<TEntity>
    {
        public Task CreateRange(CancellationToken cancellationToken = default, params TEntity[] entities);

        public Task DeleteAllByShop(Shop shop, CancellationToken cancellationToken = default);
    }
}