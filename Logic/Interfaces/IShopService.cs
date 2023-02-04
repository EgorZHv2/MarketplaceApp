using Data.DTO.Shop;
using Data.Entities;
using Data.IRepositories;
using Data.Models;

namespace Logic.Interfaces
{
    public interface IShopService : IBaseService<Shop, ShopDTO, CreateShopDTO, UpdateShopDTO, IShopRepository>
    {
        public Task<List<ShopDTO>> GetAll(Guid userid, CancellationToken cancellationToken = default);

        public Task AddShopToFavorites(Guid userId, Guid shopId, CancellationToken cancellationToken = default);

        public Task<List<ShopDTO>> ShowUserFavoriteShops(Guid userId, CancellationToken cancellationToken = default);

        public Task DeleteShopFromFavorites(Guid userId, Guid shopId, CancellationToken cancellationToken = default);
        public Task<PageModel<ShopDTO>> GetPage(Guid userId, FilterPagingModel pagingModel, CancellationToken cancellationToken = default);
    }
}