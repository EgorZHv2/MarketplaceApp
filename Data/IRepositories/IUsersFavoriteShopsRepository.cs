using Data.Entities;
using Data.Models;

namespace Data.IRepositories
{
    public interface IUsersFavoriteShopsRepository
    {
        public Task<ICollection<Shop>> GetFavoriteShopsByUserId(Guid userId, CancellationToken cancellationToken = default);

        public Task<UserFavoriteShop> GetFavByShopAndUserId(Guid userId, Guid shopId, CancellationToken cancellationToken = default);

        public Task Delete(UserFavoriteShop entity, CancellationToken cancellationToken = default);
        public  Task<PageModel<Shop>> GetFavsPageByUserId(Guid userId, int pagenumber, int pagesize, CancellationToken cancellationToken = default);
    }
}