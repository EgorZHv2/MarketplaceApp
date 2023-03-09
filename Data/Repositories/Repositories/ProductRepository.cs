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
using Data.Enums;
using Data.DTO.Product;

namespace Data.Repositories.Repositories
{
    public class ProductRepository:BaseRepository<ProductEntity>,IProductRepository
    {
       
        
        public ProductRepository(ApplicationDbContext context):base(context)
        {
          
        }


        public async Task<PageModelDTO<ProductEntity>> GetPage(UserEntity user, PaginationDTO pagination,
            ProductFilterDTO filter)
        {

            var queryable = _dbset.Where(e=>e.IsActive || user.Role ==Role.Admin)
                .Include(e => e.Category)
                .Include(e=>e.Shops)
                .Include(e=>e.ShopProducts)
                .AsNoTracking();
            queryable = filter.SearchQuery is not null ? queryable.Where(e=>
            e.Name.Contains(filter.SearchQuery) || 
            e.Description.Contains(filter.SearchQuery) ||
            e.Category.Name.Contains(filter.SearchQuery)) : queryable;
            queryable = filter.Country is not null ? queryable.Where(e=>e.Country == filter.Country): queryable;
            queryable = queryable.Where(e=> e.Weight >= filter.MinWeight && e.Weight <= filter.MaxWeight);
            queryable = queryable.Where(e => e.Width >= filter.MinWidth && e.Width <= filter.MaxWidth);
            queryable = queryable.Where(e => e.Height >= filter.MinHeight && e.Height <= filter.MaxHeight);
            queryable = queryable.Where(e => e.Depth >= filter.MinDepth && e.Depth <= filter.MaxDepth);
            queryable = filter.ShopId is not null ? queryable.Where(e => e.Shops.Any(e => e.Id == filter.ShopId)) : queryable;
            return await queryable.ToPageModelAsync(pagination.PageNumber, pagination.PageSize);
        }

        public async Task<PageModelDTO<ProductEntityWithPriceDTO>>  GetProductsInShopsPage(UserEntity user, PaginationDTO pagination,
            ShopProductFilterDTO filter)
        {

            var shopProducts = _context.Set<ShopProductEntity>()
                .Include(e=>e.Product).ThenInclude(e=>e.Category)
                .Include(e=>e.Product).ThenInclude(e=>e.Shops).AsNoTracking();
            shopProducts = shopProducts.Where(e => e.Product.IsActive || user.Role == Role.Admin);
            shopProducts = shopProducts.Where(e=> e.Price >= filter.MinPrice&& e.Price <= filter.MaxPrice);
            shopProducts = filter.SearchQuery is not null ? shopProducts.Where(
                e => e.Product.Name.Contains(filter.SearchQuery) || 
                e.Product.Description.Contains(filter.SearchQuery) ||
                e.Product.Category.Name.Contains(filter.SearchQuery) ||
                e.Description.Contains(filter.SearchQuery)) : shopProducts;
            shopProducts = filter.Country is not null ? shopProducts.Where(e=>e.Product.Country == filter.Country): shopProducts;
            shopProducts = shopProducts.Where(e=> e.Product.Weight >= filter.MinWeight && e.Product.Weight <= filter.MaxWeight);
            shopProducts = shopProducts.Where(e => e.Product.Width >= filter.MinWidth && e.Product.Width <= filter.MaxWidth);
            shopProducts = shopProducts.Where(e => e.Product.Height >= filter.MinHeight && e.Product.Height <= filter.MaxHeight);
            shopProducts = shopProducts.Where(e => e.Product.Depth >= filter.MinDepth && e.Product.Depth <= filter.MaxDepth);
            shopProducts = filter.ShopId is not null ? shopProducts.Where(e => e.ShopId == filter.ShopId): shopProducts;
            
            var qeryable = shopProducts.Select(e => new ProductEntityWithPriceDTO { Product = e.Product,Price = e.Price.Value});
            
           

            return await qeryable.ToPageModelAsync(pagination.PageNumber, pagination.PageSize);
        }
        public async Task<ProductEntity> GetByPartNumber(int partNumber)
        {
            return await _dbset.FirstOrDefaultAsync(e => e.PartNumber == partNumber);
        }
    }
}
