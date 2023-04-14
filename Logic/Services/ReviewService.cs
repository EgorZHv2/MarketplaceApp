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

        public async Task<List<ReviewDTO>> GetReviewsByShopId(Guid shopId)
        {
          
            var list = _repository
                .GetReviewsByShopId(shopId);

            var result = _mapper.Map<List<ReviewDTO>>(list);
            return result;
        }

        public override async Task<Guid> Create(CreateReviewDTO createDTO)
        {
            var shop = _shopRepository.GetById(createDTO.ShopId);
            if (shop == null)
            {
                throw new ShopNotFoundException();
            }

            var entity = _mapper.Map<ReviewEntity>(createDTO);

            var result = await _repository.Create(entity);
            return result;
        }

        public async Task<PageModelDTO<ReviewDTO>> GetPage(PaginationDTO pagingModel)
        {
            var pages = await _repository.GetPage(pagingModel);
            var result = _mapper.Map<PageModelDTO<ReviewDTO>>(pages);
            return result;
        }
    }
}
