using Data.Entities;

namespace Data.IRepositories
{
    public interface IReviewRepository : IBaseRepository<ReviewEntity>
    {
        public IEnumerable<ReviewEntity> GetReviewsByShopId(Guid ShopId);
    }
}