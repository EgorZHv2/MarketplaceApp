using Data.DTO;
using Data.DTO.Filters;
using Data.DTO.Shop;
using Data.Entities;
using Data.IRepositories;
using Microsoft.AspNetCore.Http;

namespace Logic.Interfaces
{
    public interface IShopService : IBaseService<ShopEntity, ShopDTO, CreateShopDTO, UpdateShopDTO, IShopRepository>
    {
        public Task AddShopToFavorites(Guid userId, Guid shopId);

        public Task<PageModelDTO<ShopDTO>> ShowUserFavoriteShops(Guid userId, PaginationDTO filterPaging);

        public Task DeleteShopFromFavorites(Guid userId, Guid shopId);

        public Task<PageModelDTO<ShopDTO>> GetPage(Guid userId, PaginationDTO pagingModel, ShopFilterDTO filter);
        public Task AddProductsToShopFromXML(Guid shopId, IFormFile xmlFile);
    }
}