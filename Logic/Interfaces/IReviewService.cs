using Data.DTO.Review;
using Data.Entities;
using Data.IRepositories;
using Data.Models;

namespace Logic.Interfaces
{
    public interface IReviewService
        : IBaseService<Review, ReviewDTO, CreateReviewDTO, UpdateReviewDTO, IReviewRepository>
    {
        public Task<List<ReviewDTO>> GetReviewsByShopId(
            Guid userId,
            Guid shopId,
            CancellationToken cancellationToken = default
        );

      
        public Task<PageModel<ReviewDTO>> GetPage(Guid userId, FilterPagingModel pagingModel, CancellationToken cancellationToken = default);
    }
}