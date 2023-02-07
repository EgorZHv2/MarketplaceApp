﻿using Data.DTO;
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

        public Task<PageModelDTO<ReviewDTO>> GetPage(Guid userId, FilterPagingDTO pagingModel, CancellationToken cancellationToken = default);
    }
}