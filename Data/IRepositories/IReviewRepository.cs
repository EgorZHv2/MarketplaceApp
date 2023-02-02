using Data.Entities;

namespace Data.IRepositories
{
    public interface IReviewRepository : IBaseRepository<Review>
    {
        public IEnumerable<Review> GetReviewsByShopId(Guid ShopId);
    }
}