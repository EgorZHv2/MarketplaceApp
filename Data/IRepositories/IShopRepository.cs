using Data.DTO.Filters;
using Data.DTO;
using Data.Entities;
using System.Linq.Expressions;

namespace Data.IRepositories
{
    public interface IShopRepository : IBaseRepository<ShopEntity>
    {
        public Task<PageModelDTO<ShopEntity>> GetPage(IQueryable<ShopEntity> qeryable, PaginationDTO pagination, ShopFilterDTO filter);
    }
}