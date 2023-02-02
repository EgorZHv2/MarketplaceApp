﻿using AutoMapper;
using Data.DTO.Review;
using Data.Entities;
using Data.IRepositories;
using Logic.Exceptions;
using Logic.Interfaces;

namespace Logic.Services
{
    public class ReviewService : BaseService<Review, ReviewDTO, CreateReviewDTO, UpdateReviewDTO, IReviewRepository>, IReviewService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public ReviewService(IReviewRepository repository, IMapper mapper,
            IRepositoryWrapper repositoryWrapper) : base(repository, mapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<ReviewDTO>> GetReviewsByShopId(Guid userId, Guid shopId, CancellationToken cancellationToken = default)
        {
            var user = await _repositoryWrapper.Users.GetById(userId, cancellationToken);
            var list = _repository
                .GetReviewsByShopId(shopId)
                .Where(e => (e.IsActive || user.Id == e.BuyerId || user.Role == Data.Enums.Role.Admin)).ToList();
            List<ReviewDTO> result = new List<ReviewDTO>();
            try
            {
                result = _mapper.Map<List<ReviewDTO>>(list);
            }
            catch
            {
                throw new MappingException(this);
            }
            return result;
        }

        public async Task<List<ReviewDTO>> GetAll(
            Guid userid,
            CancellationToken cancellationToken = default
        )
        {
            var user = await _repositoryWrapper.Users.GetById(userid, cancellationToken);
            var list = _repository
                .GetAll()
                .Where(e => (e.IsActive || user.Id == e.BuyerId || user.Role == Data.Enums.Role.Admin)).ToList();
            List<ReviewDTO> result = new List<ReviewDTO>();
            try
            {
                result = _mapper.Map<List<ReviewDTO>>(list);
            }
            catch
            {
                throw new MappingException(this);
            }
            return result;
        }

        public override async Task<Guid> Create(Guid userId, CreateReviewDTO createDTO, CancellationToken cancellationToken = default)
        {
            Review entity = new Review();
            try
            {
                entity = _mapper.Map<Review>(createDTO);
            }
            catch
            {
                throw new MappingException(this);
            }
            entity.BuyerId = userId;
            var result = await _repository.Create(userId, entity, cancellationToken);
            return result;
        }
    }
}