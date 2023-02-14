using Data.DTO.Filters;
using Data.DTO;
using Data.Entities;
using System.Linq.Expressions;

namespace Data.IRepositories
{
    public interface IShopRepository : IBaseRepository<Shop>
    {
        public Task<PageModelDTO<Shop>> GetPage(Expression<Func<Shop, bool>> predicate, PaginationDTO pagination, ShopFilterDTO filter, CancellationToken cancellationToken = default);
    }
}