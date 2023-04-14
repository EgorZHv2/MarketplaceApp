using Data.DTO;
using Data.Entities;
using Data.Enums;
using Data.Extensions;
using Data.IRepositories;

namespace Data.Repositories.Repositories
{
    public class ReviewRepository : BaseRepository<ReviewEntity>, IReviewRepository
    {
        public ReviewRepository(ApplicationDbContext context,IUserData userData) : base(context,userData)
        {
        }

        public IEnumerable<ReviewEntity> GetReviewsByShopId(Guid ShopId)
        {
            return _context.Reviews.Where(e => e.ShopId == ShopId || _userData.Id == e.BuyerId || _userData.Role == Role.Admin);
        }
        public async Task<PageModelDTO<ReviewEntity>> GetPage(PaginationDTO pagination)
        {
            var qeryable = _dbset.Where(e=> _userData.Role == Role.Admin || e.BuyerId == _userData.Id);
            return  await qeryable.ToPageModelAsync(pagination);
        }
    }
}