using Data.DTO;
using Data.Entities;
using Data.DTO.Review;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
