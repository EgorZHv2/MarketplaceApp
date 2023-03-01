using Data.DTO.Filters;
using Data.DTO;
using Data.Entities;
using System.Linq.Expressions;

namespace Data.IRepositories
{
    public interface IShopRepository : IBaseRepository<ShopEntity>
    {
        Task<PageModelDTO<ShopEntity>> GetPage(UserEntity user,  PaginationDTO pagination,
            ShopFilterDTO filter);

    }
}