using AutoMapper;
using Data.DTO;
using Data.DTO.Review;
using Data.Entities;
using Data.IRepositories;
using Logic.Exceptions;
using Logic.Interfaces;

namespace Logic.Services
{
    public class ReviewService
        : BaseService<ReviewEntity, ReviewDTO, CreateReviewDTO, UpdateReviewDTO, IReviewRepository>,
            IReviewService
    {
        private IUserRepository _userRepository;
        private IShopRepository _shopRepository;

        public ReviewService(
            IReviewRepository repository,
            IMapper mapper,
            IUserRepository userRepository,
            IShopRepository shopRepository
        ) : base(repository, mapper)
        {
            _userRepository = userRepository;
            _shopRepository = shopRepository;
        }

        public async Task<List<ReviewDTO>> GetReviewsByShopId(Guid userId, Guid shopId)
        {
            var user = await _userRepository.GetById(userId);
            var list = _repository
                .GetReviewsByShopId(shopId)
                .Where(
                    e => (e.IsActive || user.Id == e.BuyerId || user.Role == Data.Enums.Role.Admin)
                )
                .ToList();

            var result = _mapper.Map<List<ReviewDTO>>(list);
            return result;
        }

        public override async Task<Guid> Create(Guid userId, CreateReviewDTO createDTO)
        {
            var shop = _shopRepository.GetById(createDTO.ShopId);
            if (shop == null)
            {
                throw new ShopNotFoundException();
            }

            var entity = _mapper.Map<ReviewEntity>(createDTO);
            entity.BuyerId = userId;
            var result = await _repository.Create(userId, entity);
            return result;
        }

        public async Task<PageModelDTO<ReviewDTO>> GetPage(Guid userId, PaginationDTO pagingModel)
        {
            var user = await _userRepository.GetById(userId);

            var pages = await _repository.GetPage(user, pagingModel);
            var result = _mapper.Map<PageModelDTO<ReviewDTO>>(pages);
            return result;
        }
    }
}
