﻿using Data.DTO;
using Data.DTO.Filters;
using Data.Entities;
using Data.Extensions;
using Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;

namespace Data.Repositories.Repositories
{
    public class ShopRepository : BaseRepository<ShopEntity>, IShopRepository
    {
        private ICategoryRepository _categoryRepository;

        public ShopRepository(ApplicationDbContext context,IUserData userData, ICategoryRepository categoryRepository)
            : base(context,userData)
        {
            _categoryRepository = categoryRepository;
        }

        public override async Task<Guid> Create(
             ShopEntity entity
            
        )
        {
            entity.CreateDateTime = DateTime.UtcNow;
            entity.CreatedBy = _userData.Id;
            entity.IsActive = true;
            entity.DeletedBy = null;
            entity.DeleteDateTime = null;
            entity.Id = Guid.NewGuid();
            entity.SellerId = _userData.Id;
            await _dbset.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<PageModelDTO<ShopEntity>> GetPage(
            PaginationDTO pagination,
            ShopFilterDTO filter)
        {
            var queryable = _dbset.Where(e=>e.IsActive || _userData.Role == Enums.Role.Admin || e.SellerId == _userData.Id)
                .Include(e => e.Categories)
                .Include(e => e.DeliveryTypes)
                .Include(e => e.PaymentMethods)
                .Include(e => e.Types)
                .Include(e=>e.Products)
                .Include(e=>e.ShopProducts)
                .AsNoTracking();
            queryable = filter.SearchQuery is not null
                ? queryable.Where(e => e.Title.Contains(filter.SearchQuery) || e.Description.Contains(filter.SearchQuery) || e.ShopProducts.Any(e=>e.Description.Contains(filter.SearchQuery)))
                : queryable;
            
            queryable = queryable.Where( e=> e.ShopProducts.Any() ? e.ShopProducts.Any(e=>e.Price>= filter.MinPrice && e.Price <= filter.MaxPrice) : true);
            queryable = filter.DeliveryTypesIds.Any() 
                ? queryable.Where(e => e.DeliveryTypes.Any(e => filter.DeliveryTypesIds.Contains(e.Id)))
                : queryable;
            queryable = filter.PaymentMethodsIds.Any()
                ? queryable.Where(e => e.PaymentMethods.Any(e => filter.PaymentMethodsIds.Contains(e.Id)))
                : queryable;
            queryable = filter.TypesIds.Any() 
                ? queryable.Where(e => e.Types.Any(e => filter.TypesIds.Contains(e.Id)))
            : queryable;

           queryable = filter.ProductId is not null ? queryable.Where(e => e.Products.Any(e => e.Id == filter.ProductId)) : queryable;

            if(filter.CategoriesIds.Any())
            {
                var guids = new List<Guid>();
                foreach (var item in filter.CategoriesIds)
                {
                  guids.AddRange(await FillCategoriesGuidList(item));
                }
                guids = guids.Distinct().ToList();
                queryable = queryable.Where(e => e.Categories.Any(e => guids.Contains(e.Id)));
               
            }

            
            return await GetPage(pagination,queryable);          
        }
        public async Task<List<Guid>> FillCategoriesGuidList(Guid filterCategoryId)
        {
            var guids = new List<Guid>();
            var childcategories = await _categoryRepository.GetCategoriesByParentId(filterCategoryId);
            if (childcategories is not null)
            {
                await InnerRecursive(childcategories);
                async Task InnerRecursive(IEnumerable<CategoryEntity> categories )
                {
                    foreach(var item in categories)
                    {
                        guids.Add(item.Id);
                        var childcategories = await _categoryRepository.GetCategoriesByParentId(item.Id);
                        if (childcategories is not null)
                        {
                           await InnerRecursive(childcategories);
                        }
                    }
                }
            }
            guids.Add(filterCategoryId);
            return guids;
        }
    }
}
