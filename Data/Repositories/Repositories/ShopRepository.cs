using Data.DTO;
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

        public ShopRepository(ApplicationDbContext context, ICategoryRepository categoryRepository)
            : base(context)
        {
            _categoryRepository = categoryRepository;
        }

        public override async Task<Guid> Create(
            Guid userId,
            ShopEntity entity
            
        )
        {
            entity.CreateDateTime = DateTime.UtcNow;
            entity.CreatorId = userId;
            entity.UpdateDateTime = DateTime.UtcNow;
            entity.UpdatorId = userId;
            entity.IsActive = true;
            entity.IsDeleted = false;
            entity.Id = Guid.NewGuid();
            entity.SellerId = userId;
            await _dbset.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<PageModelDTO<ShopEntity>> GetPage(
            IQueryable<ShopEntity> queryable,
            PaginationDTO pagination,
            ShopFilterDTO filter
            
        )
        {
            queryable = queryable
                .Include(e => e.Categories)
                .Include(e => e.DeliveryTypes)
                .Include(e => e.PaymentMethods)
                .Include(e => e.Types)
                .AsNoTracking();
            queryable = filter.SearchQuery is not null
                ? queryable.Where(e => e.Title.Contains(filter.SearchQuery) || e.Description.Contains(filter.SearchQuery))
                : queryable;
          
            queryable = filter.Id is not null ? queryable.Where(e => e.Id == filter.Id) : queryable;

            queryable = filter.DeliveryTypeId is not null
                ? queryable.Where(e => e.DeliveryTypes.Any(e => e.Id == filter.DeliveryTypeId))
                : queryable;
            queryable = filter.PaymentMethodId is not null
                ? queryable.Where(e => e.PaymentMethods.Any(e => e.Id == filter.PaymentMethodId))
                : queryable;
            queryable = filter.TypeId is not null
                ? queryable.Where(e => e.Types.Any(e => e.Id == filter.TypeId))
            : queryable;

            if(filter.CategoryId is not null)
            {
                List<Guid> guids = await FillCategoriesGuidList(filter.CategoryId.Value);
                queryable = queryable.Where(e => e.Categories.Any(e => guids.Contains(e.Id)));
               
            }
           
            
            

            var result = await queryable.ToPageModelAsync(
                   pagination.PageNumber,
                   pagination.PageSize
                   
               );
            return result;          
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
