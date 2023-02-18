using Data.DTO;
using Data.Entities;

namespace Data.IRepositories
{
    public interface IUsersFavoriteShopsRepository
    {
        public Task<ICollection<Shop>> GetFavoriteShopsByUserId(Guid userId);

        public Task<UserFavoriteShop?> GetFavByShopAndUserId(Guid userId, Guid shopId);

        public Task Delete(UserFavoriteShop entity);

        public Task<PageModelDTO<Shop>> GetFavsPageByUserId(Guid userId, int pageNumber, int pageSize);
    }
}