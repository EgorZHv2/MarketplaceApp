using AutoMapper;
using Data.DTO.Review;
using Data.DTO.Shop;
using Data.Entities;
using Data.IRepositories;
using Data.Models;
using Data.Repositories.Repositories;
using Logic.Exceptions;
using Logic.Interfaces;

namespace Logic.Services
{
    public class ReviewService : BaseService<Review, ReviewDTO, CreateReviewDTO, UpdateReviewDTO, IReviewRepository>, IReviewService
    {
        private IRepositoryWrapper _repositoryWrapper;
        private IUserRepository _userRepository;

        public ReviewService(IReviewRepository repository, IMapper mapper,
            IRepositoryWrapper repositoryWrapper, IUserRepository userRepository) : base(repository, mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _userRepository = userRepository;
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

        public  async Task<PageModel<ReviewDTO>> GetPage(Guid userId, FilterPagingModel pagingModel, CancellationToken cancellationToken = default)
        {
            var result = new PageModel<ReviewDTO>();
            var user = await _userRepository.GetById(userId);
            var pages = await _repository.GetPage(e => (e.IsActive || e.BuyerId == userId || user.Role == Data.Enums.Role.Admin), pagingModel.PageNumber, pagingModel.PageSize, cancellationToken);
            try
            {
                result = _mapper.Map<PageModel<ReviewDTO>>(pages);
            }
            catch
            {
                throw new MappingException(this);
            }
            return result;
        }
    }
}