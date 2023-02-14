using AutoMapper;
using Data.DTO;
using Data.DTO.Review;
using Data.Entities;
using Data.IRepositories;
using Logic.Exceptions;
using Logic.Interfaces;

namespace Logic.Services
{
    public class ReviewService : BaseService<Review, ReviewDTO, CreateReviewDTO, UpdateReviewDTO, IReviewRepository>, IReviewService
    {
        private IUserRepository _userRepository;
        private IShopRepository _shopRepository;

        public ReviewService(IReviewRepository repository, IMapper mapper,
            IUserRepository userRepository,
            IShopRepository shopRepository) : base(repository, mapper)
        {
            _userRepository = userRepository;
            _shopRepository = shopRepository;
        }

        public async Task<List<ReviewDTO>> GetReviewsByShopId(Guid userId, Guid shopId, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetById(userId, cancellationToken);
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
            var shop = _shopRepository.GetById(createDTO.ShopId);
            if (shop == null)
            {
                throw new NotFoundException("Магазин не найден", "Parent shop not found");
            }
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

        public async Task<PageModelDTO<ReviewDTO>> GetPage(Guid userId, PaginationDTO pagingModel, CancellationToken cancellationToken = default)
        {
            var result = new PageModelDTO<ReviewDTO>();
            var user = await _userRepository.GetById(userId);
            var pages = await _repository.GetPage(e => (e.IsActive || e.BuyerId == userId || user.Role == Data.Enums.Role.Admin), pagingModel, cancellationToken);
            try
            {
                result = _mapper.Map<PageModelDTO<ReviewDTO>>(pages);
            }
            catch
            {
                throw new MappingException(this);
            }
            return result;
        }
    }
}