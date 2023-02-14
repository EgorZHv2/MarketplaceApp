using Data.DTO;
using Data.DTO.Filters;
using Data.Entities;
using Data.Extensions;
using Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories.Repositories
{
    public class ShopRepository : BaseRepository<Shop>, IShopRepository
    {
        private IShopCategoryRepository _shopCategory;
        private IShopDeliveryTypeRepository _shopDeliveryType;
        private IShopPaymentMethodRepository _shopPaymentMethod;
        private IShopTypeRepository _shopType;

        public ShopRepository(
            ApplicationDbContext context,
            IShopCategoryRepository shopCategory,
            IShopDeliveryTypeRepository shopDeliveryType,
            IShopPaymentMethodRepository shopPaymentMethod,
            IShopTypeRepository shopType
        ) : base(context)
        {
            _shopCategory = shopCategory;
            _shopDeliveryType = shopDeliveryType;
            _shopPaymentMethod = shopPaymentMethod;
            _shopType = shopType;
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
            queryable = filter.CategoryId is not null
                ? queryable.Where(e => e.Categories.Any(e => e.Id == filter.CategoryId))
                : queryable;
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
            return result;
        }
    }
}
