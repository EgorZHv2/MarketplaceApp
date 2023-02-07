using Data.Entities;
using Data.IRepositories;

namespace Data.Repositories.Repositories
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<Review> GetReviewsByShopId(Guid ShopId)
        {
            return _context.Reviews.Where(e => e.ShopId == ShopId);
        }
    }
}