using AutoMapper;
using Data.DTO.Shop;
using Data.Entities;
using Data.IRepositories;
using Data.Models;
using Logic.Exceptions;
using Logic.Interfaces;
using Microsoft.Extensions.Configuration;
using WebAPi.Interfaces;

namespace Logic.Services
{
    public class ShopService
        : BaseService<Shop, ShopDTO, CreateShopDTO, UpdateShopDTO, IShopRepository>,
            IShopService
    {
        private IINNService _iNNService;
        private IRepositoryWrapper _repositoryWrapper;
        private IImageService _imageService;
        private IConfiguration _configuration;
        private IUserRepository _userRepository;
        private IUsersFavoriteShopsRepository _usersFavoriteShops;
        private IShopCategoryRepository _shopCategoryRepository;
        private IShopDeliveryTypeRepository _shopDeliveryTypeRepository;
        private IShopPaymentMethodRepository _shopPaymentMethodRepository;
        private IShopTypeRepository _shopTypeRepository;

        public ShopService(
            IShopRepository repository,
            IMapper mapper,
            IINNService iNNService,
            IRepositoryWrapper repositoryWrapper,
            IImageService imageService,
            IConfiguration configuration,
            IUserRepository userRepository,
            IUsersFavoriteShopsRepository usersFavoriteShops,
            IShopCategoryRepository shopCategoryRepository,
            IShopDeliveryTypeRepository shopDeliveryTypeRepository,
            IShopPaymentMethodRepository shopPaymentMethodRepository,
            IShopTypeRepository shopTypeRepository
        ) : base(repository, mapper)
        {
            _iNNService = iNNService;
            _repositoryWrapper = repositoryWrapper;
            _imageService = imageService;
            _configuration = configuration;
            _userRepository = userRepository;
            _usersFavoriteShops = usersFavoriteShops;
            _shopCategoryRepository = shopCategoryRepository;
            _shopDeliveryTypeRepository = shopDeliveryTypeRepository;
            _shopPaymentMethodRepository = shopPaymentMethodRepository;
            _shopTypeRepository = shopTypeRepository;
        }

        public async Task<List<ShopDTO>> GetAll(
            Guid userid,
            CancellationToken cancellationToken = default
        )
        {
            var user = await _repositoryWrapper.Users.GetById(userid, cancellationToken);
            var list = _repository
                .GetAll()
                .Where(
                    e => (e.IsActive || user.Id == e.SellerId || user.Role == Data.Enums.Role.Admin)
                )
                .ToList();
            List<ShopDTO> result = new List<ShopDTO>();
            string basepath = _configuration.GetSection("BaseImagePath").Value;
            try
            {
                foreach (var item in list)
                {
                    ShopDTO dto = new ShopDTO();
                    dto = _mapper.Map<ShopDTO>(item);
                    var fileinfo = _repositoryWrapper.StaticFileInfos.GetByParentId(item.Id).Result;
                    if (fileinfo != null)
                    {
                        dto.ImagePath =
                            basepath
                            + fileinfo.ParentEntityId.ToString()
                            + "/"
                            + fileinfo.Name
                            + "."
                            + fileinfo.Extension;
                    }
                    result.Add(dto);
                }
            }
            catch
            {
                throw new MappingException(this);
            }
            return result;
        }

        public override async Task<Guid> Create(
            Guid userId,
            CreateShopDTO createDTO,
            CancellationToken cancellationToken = default
        )
        {
            if (!_iNNService.CheckINN(createDTO.INN))
            {
                throw new NotFoundException("INN не найден", "INN not valid");
            }
            Shop shop = new Shop();
            try
            {
                shop = _mapper.Map<Shop>(createDTO);
            }
            catch
            {
                throw new MappingException(this);
            }

            var result = await _repository.Create(userId, shop, cancellationToken);
            return result;
        }

        public override async Task<UpdateShopDTO> Update(
            Guid userId,
            UpdateShopDTO UpdateDTO,
            CancellationToken cancellationToken = default
        )
        {
            if (!_iNNService.CheckINN(UpdateDTO.INN))
            {
                throw new NotFoundException("INN не найден", "INN not valid");
            }
            Shop shop = await _repository.GetById(UpdateDTO.Id, cancellationToken);
            try
            {
                _mapper.Map(UpdateDTO, shop);
            }
            catch
            {
                throw new MappingException(this);
            }
            if (UpdateDTO.Image != null)
            {
                await _imageService.CreateImage(UpdateDTO.Image, UpdateDTO.Id, cancellationToken);
            }
            await _repository.Update(userId, shop, cancellationToken);

            await _shopCategoryRepository.DeleteAllByShop(shop);
            await _shopDeliveryTypeRepository.DeleteAllByShop(shop);
            await _shopPaymentMethodRepository.DeleteAllByShop(shop);
            await _shopTypeRepository.DeleteAllByShop(shop);
            if (UpdateDTO.CategoriesId.Count > 0)
            {
                var categories = UpdateDTO.CategoriesId.Select(
                    e => new ShopCategory { CategoryId = e, ShopId = shop.Id }
                ).ToArray();
                await _shopCategoryRepository.CreateRange(cancellationToken, categories);
            }
            if (UpdateDTO.TypesId.Count > 0)
            {
                var types = UpdateDTO.CategoriesId.Select(
                    e => new ShopType { TypeId = e, ShopId = shop.Id }
                ).ToArray();
                await _shopTypeRepository.CreateRange(cancellationToken, types);
            }
            if (UpdateDTO.ShopDeliveryTypes.Count > 0)
            {
                var deliveryTypes = UpdateDTO.ShopDeliveryTypes.Select(
                    e =>
                        new ShopDeliveryType
                        {
                            DeliveryTypeId = e.Id,
                            ShopId = shop.Id,
                            FreeDeliveryThreshold = e.FreeDeliveryThreshold
                        }
                ).ToArray();
                await _shopDeliveryTypeRepository.CreateRange(cancellationToken, deliveryTypes);
            }
            if (UpdateDTO.ShopPaymentMethods.Count > 0)
            {
                var paymentMethods = UpdateDTO.ShopPaymentMethods.Select(e => new ShopPaymentMethod
                {
                    PaymentMethodId = e.Id,
                    ShopId = shop.Id,
                    Сommission = e.Сommission
                }).ToArray();
                await _shopPaymentMethodRepository.CreateRange(cancellationToken, paymentMethods);
            }
            return UpdateDTO;
        }

        public async Task AddShopToFavorites(
            Guid userId,
            Guid shopId,
            CancellationToken cancellationToken = default
        )
        {
            var user = await _userRepository.GetById(userId, cancellationToken);
            var existing = await _usersFavoriteShops.GetFavByShopAndUserId(
                userId,
                shopId,
                cancellationToken
            );
            if (existing != null)
            {
                throw new AlreadyExistsException(
                    "Магазин уже в избранном",
                    "Shop already in favorites table"
                );
            }
            if (user == null)
            {
                throw new NotFoundException("Пользователь не найден", "User not found");
            }
            var shop = await _repository.GetById(shopId, cancellationToken);
            user.FavoriteShops.Add(shop);
            await _userRepository.Update(userId, user, cancellationToken);
        }

        public async Task<List<ShopDTO>> ShowUserFavoriteShops(
            Guid userId,
            CancellationToken cancellationToken = default
        )
        {
            var user = await _userRepository.GetById(userId, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException("Пользователь не найден", "User not found");
            }
            List<ShopDTO> result = new List<ShopDTO>();
            var list = await _usersFavoriteShops.GetFavoriteShopsByUserId(userId);
            try
            {
                result = _mapper.Map<List<ShopDTO>>(list);
            }
            catch
            {
                throw new MappingException(this);
            }
            return result;
        }

        public async Task DeleteShopFromFavorites(
            Guid userId,
            Guid shopId,
            CancellationToken cancellationToken = default
        )
        {
            var existing = await _usersFavoriteShops.GetFavByShopAndUserId(
                userId,
                shopId,
                cancellationToken
            );
            if (existing == null)
            {
                throw new NotFoundException("Избранный магазин не найден", "Wrong shop or user id");
            }
            await _usersFavoriteShops.Delete(existing, cancellationToken);
        }

        public  async Task<PageModel<ShopDTO>> GetPage(Guid userId, FilterPagingModel pagingModel, CancellationToken cancellationToken = default)
        {
            var result = new PageModel<ShopDTO>();
            var user = await _userRepository.GetById(userId);
            var pages = await _repository.GetPage(e => (e.IsActive || e.SellerId == userId || user.Role == Data.Enums.Role.Admin), pagingModel.PageNumber, pagingModel.PageSize, cancellationToken);
            try
            {
                result = _mapper.Map<PageModel<ShopDTO>>(pages);
            }
            catch
            {
                throw new MappingException(this);
            }
            return result;
        }
    }
}
