using Data.DTO;
using Data.DTO.Filters;
using Data.Entities;
using Data.Extensions;
using Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Linq.Expressions;

namespace Data.Repositories.Repositories
{
    public class ShopRepository : BaseRepository<Shop>, IShopRepository
    {

        private ICategoryRepository _categoryRepository;
        public ShopRepository(
            ApplicationDbContext context,
            ICategoryRepository categoryRepository
            
        ) : base(context)
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
            queryable = filter.Title is not null
                ? queryable.Where(e => e.Title.Contains(filter.Title))
                : queryable;
            queryable = filter.Description is not null
                ? queryable.Where(e => e.Description.Contains(filter.Description))
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

            var result = await queryable.ToPageModelAsync(
                pagination.PageNumber,
                pagination.PageSize,
                cancellationToken
            );
            if (filter.CategoryId is not null)
            {
                List<Shop> afthercategoryfilter = new List<Shop>();
                foreach (var shop in result.Values)
                {
                    foreach (var cat in shop.Categories)
                    {
                        if (cat.Id == (Guid)filter.CategoryId)
                        {
                            afthercategoryfilter.Add(shop);
                            break;
                        }
                        else
                        {
                            Category category = cat;
                            while (category.ParentCategoryId != null)
                            {
                                category = await _categoryRepository.GetById((Guid)category.ParentCategoryId);
                                if (category.Id == (Guid)filter.CategoryId)
                                {
                                    afthercategoryfilter.Add(shop);
                                    break;
                                }
                            }
                        }
                    }
                }
                result.Values = afthercategoryfilter;
            }
            
          
            return result;
        }
        
    }
}
