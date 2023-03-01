using Data.DTO;
using Data.Entities;
using Data.Enums;
using Data.IRepositories;

namespace Data.Repositories.Repositories
{
    public class ReviewRepository : BaseRepository<ReviewEntity>, IReviewRepository
    {
        public ReviewRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<ReviewEntity> GetReviewsByShopId(Guid ShopId)
        {
            return _context.Reviews.Where(e => e.ShopId == ShopId);
        }
        public async Task<PageModelDTO<ReviewEntity>> GetPage(UserEntity user,PaginationDTO pagination)
        {
            var qeryable = _dbset.Where(e => e.IsActive || user.Role == Role.Admin || e.BuyerId == user.Id);
            return  await GetPage(pagination,qeryable);
        }
    }
}