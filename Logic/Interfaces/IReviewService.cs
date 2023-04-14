using Data.DTO;
using Data.DTO.Review;
using Data.Entities;
using Data.IRepositories;

namespace Logic.Interfaces
{
    public interface IReviewService
        : IBaseService<ReviewEntity, ReviewDTO, CreateReviewDTO, UpdateReviewDTO, IReviewRepository>
    {
        public Task<List<ReviewDTO>> GetReviewsByShopId(Guid shopId);
        public Task<PageModelDTO<ReviewDTO>> GetPage(PaginationDTO pagingModel);
    }
}