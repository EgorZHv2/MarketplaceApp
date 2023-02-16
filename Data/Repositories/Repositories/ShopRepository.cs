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
    public class ShopRepository : BaseRepository<Shop>, IShopRepository
    {
        private ICategoryRepository _categoryRepository;

        public ShopRepository(ApplicationDbContext context, ICategoryRepository categoryRepository)
            : base(context)
        {
            _categoryRepository = categoryRepository;
        }

        public override async Task<Guid> Create(
            Guid userId,
            Shop entity,
            CancellationToken cancellationToken = default
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
            await _dbset.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }

        public async Task<PageModelDTO<Shop>> GetPage(
            Expression<Func<Shop, bool>> predicate,
            PaginationDTO pagination,
            ShopFilterDTO filter,
            CancellationToken cancellationToken = default
        )
        {
            var queryable = _dbset
                .Where(predicate)
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
                List<Guid> guids = await FillCategoriesGuidList(filter.CategoryId.Value, cancellationToken);
                queryable = queryable.Where(e => e.Categories.Any(e => guids.Contains(e.Id)));
               
            }
           
            
            

            var result = await queryable.ToPageModelAsync(
                   pagination.PageNumber,
                   pagination.PageSize,
                   cancellationToken
               );
            return result;          
        }
        public async Task<List<Guid>> FillCategoriesGuidList(Guid filtercategoryId,CancellationToken cancellationToken)
        {
            List<Guid> guids = new List<Guid>();
            var childcategories = await _categoryRepository.GetCategoriesByParentId(filtercategoryId);
            if (childcategories is not null)
            {
                await InnerRecursive(childcategories,cancellationToken);
                async Task InnerRecursive(IEnumerable<Category> categories, CancellationToken cancellationToken)
                {
                    foreach(var item in categories)
                    {
                        guids.Add(item.Id);
                        var childcategories = await _categoryRepository.GetCategoriesByParentId(item.Id);
                        if (childcategories is not null)
                        {
                           await InnerRecursive(childcategories, cancellationToken);
                        }
                    }
                }
            }
            guids.Add(filtercategoryId);
            return guids;
        }
    }
}
