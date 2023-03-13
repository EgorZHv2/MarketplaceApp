using Data.DTO;
using Data.Entities;

namespace Data.IRepositories
{
    public interface IUsersFavoriteShopsRepository
    {
        public Task<ICollection<ShopEntity>> GetFavoriteShopsByUserId(Guid userId);

        public Task<UserFavoriteShopEntity?> GetFavByShopAndUserId(Guid userId, Guid shopId);

        public Task Delete(UserFavoriteShopEntity entity);

        public Task<PageModelDTO<ShopEntity>> GetFavsPageByUserId(Guid userId, PaginationDTO pagination);
    }
}