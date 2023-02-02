using Data.DTO.Review;
using Data.Entities;
using Data.IRepositories;

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

        public Task<List<ReviewDTO>> GetAll(
            Guid userid,
            CancellationToken cancellationToken = default
        );
    }
}