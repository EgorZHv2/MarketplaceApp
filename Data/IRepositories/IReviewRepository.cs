using Data.DTO;
using Data.Entities;

namespace Data.IRepositories
{
    public interface IReviewRepository : IBaseRepository<ReviewEntity>
    {
        public IEnumerable<ReviewEntity> GetReviewsByShopId(Guid ShopId);
        public  Task<PageModelDTO<ReviewEntity>> GetPage(PaginationDTO pagination);
    }
}