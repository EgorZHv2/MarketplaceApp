using Data.DTO;
using Data.DTO.Shop;
using Data.Entities;
using Data.IRepositories;

namespace Logic.Interfaces
{
    public interface IShopService : IBaseService<Shop, ShopDTO, CreateShopDTO, UpdateShopDTO, IShopRepository>
    {
        public Task AddShopToFavorites(Guid userId, Guid shopId, CancellationToken cancellationToken = default);

        public Task<PageModelDTO<ShopDTO>> ShowUserFavoriteShops(Guid userId, FilterPagingDTO filterPaging, CancellationToken cancellationToken = default);

        public Task DeleteShopFromFavorites(Guid userId, Guid shopId, CancellationToken cancellationToken = default);

        public Task<PageModelDTO<ShopDTO>> GetPage(Guid userId, FilterPagingDTO pagingModel, CancellationToken cancellationToken = default);
    }
}