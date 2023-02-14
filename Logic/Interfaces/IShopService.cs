using Data.DTO;
using Data.DTO.Filters;
using Data.DTO.Shop;
using Data.Entities;
using Data.IRepositories;

namespace Logic.Interfaces
{
    public interface IShopService : IBaseService<Shop, ShopDTO, CreateShopDTO, UpdateShopDTO, IShopRepository>
    {
        public Task AddShopToFavorites(Guid userId, Guid shopId, CancellationToken cancellationToken = default);

        public Task<PageModelDTO<ShopDTO>> ShowUserFavoriteShops(Guid userId, PaginationDTO filterPaging, CancellationToken cancellationToken = default);

        public Task DeleteShopFromFavorites(Guid userId, Guid shopId, CancellationToken cancellationToken = default);

        public Task<PageModelDTO<ShopDTO>> GetPage(Guid userId, PaginationDTO pagingModel, ShopFilterDTO filter, CancellationToken cancellationToken = default);
    }
}