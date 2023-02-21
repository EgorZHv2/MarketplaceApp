using Data.DTO.Filters;
using Data.DTO;
using Data.Entities;
using Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore;
using Data.Extensions;

namespace Data.Repositories.Repositories
{
    public class ProductRepository:BaseRepository<ProductEntity>,IProductRepository
    {
        ICategoryRepository _categoryRepository;
        public ProductRepository(ApplicationDbContext context, ICategoryRepository categoryRepository):base(context)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<PageModelDTO<ProductEntity>> GetPage(
            IQueryable<ProductEntity> queryable,
            PaginationDTO pagination,
            ProductFilterDTO filter)
        {

            queryable = queryable.Include(e => e.Category).AsNoTracking();
            queryable = filter.SearchQuery is not null ? queryable.Where(e=>
            e.Name.Contains(filter.SearchQuery) || 
            e.Description.Contains(filter.SearchQuery) ||
            e.Category.Name.Contains(filter.SearchQuery)) : queryable;
            queryable = filter.Country is not null ? queryable.Where(e=>e.Country == filter.Country): queryable;

            queryable = queryable.Where(e=> e.Weight >= filter.MinWeight && e.Weight <= filter.MaxWeight);
            queryable = queryable.Where(e => e.Width >= filter.MinWidth && e.Width <= filter.MaxWidth);
            queryable = queryable.Where(e => e.Height >= filter.MinHeight && e.Height <= filter.MaxHeight);
            queryable = queryable.Where(e => e.Depth >= filter.MinDepth && e.Depth <= filter.MaxDepth);

            var result = await queryable.ToPageModelAsync(pagination.PageNumber, pagination.PageSize);

            return result;
        }
    }
}
