using Data.DTO.Filters;
using Data.DTO;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.IRepositories
{
    public interface IProductRepository : IBaseRepository<ProductEntity>
    {
        Task<PageModelDTO<ProductEntity>> GetPage(
            IQueryable<ProductEntity> queryable,
            PaginationDTO pagination,
            ProductFilterDTO filter
        );
    }
}
