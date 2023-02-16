﻿using AutoMapper;
using Data.DTO;
using Data.DTO.Filters;
using Data.DTO.Shop;
using Data.Entities;
using Data.IRepositories;
using Logic.Exceptions;
using Logic.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using WebAPi.Interfaces;

namespace Logic.Services
{
    public class ShopService
        : BaseService<Shop, ShopDTO, CreateShopDTO, UpdateShopDTO, IShopRepository>,
            IShopService
    {
        private IINNService _iNNService;
        private IImageService _imageService;
        private IConfiguration _configuration;
        private IUserRepository _userRepository;
        private IUsersFavoriteShopsRepository _usersFavoriteShops;
        private IShopCategoryRepository _shopCategoryRepository;
        private IShopDeliveryTypeRepository _shopDeliveryTypeRepository;
        private IShopPaymentMethodRepository _shopPaymentMethodRepository;
        private IShopTypeRepository _shopTypeRepository;
        private IStaticFileInfoRepository _staticFileInfoRepository;

        public ShopService(
            IShopRepository repository,
            IMapper mapper,
            IINNService iNNService,
            IImageService imageService,
            IConfiguration configuration,
            IUserRepository userRepository,
            IUsersFavoriteShopsRepository usersFavoriteShops,
            IShopCategoryRepository shopCategoryRepository,
            IShopDeliveryTypeRepository shopDeliveryTypeRepository,
            IShopPaymentMethodRepository shopPaymentMethodRepository,
            IShopTypeRepository shopTypeRepository,
            IStaticFileInfoRepository staticFileInfoRepository
        ) : base(repository, mapper)
        {
            _iNNService = iNNService;
            _imageService = imageService;
            _configuration = configuration;
            _userRepository = userRepository;
            _usersFavoriteShops = usersFavoriteShops;
            _shopCategoryRepository = shopCategoryRepository;
            _shopDeliveryTypeRepository = shopDeliveryTypeRepository;
            _shopPaymentMethodRepository = shopPaymentMethodRepository;
            _shopTypeRepository = shopTypeRepository;
            _staticFileInfoRepository = staticFileInfoRepository;
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

            if (createDTO.CategoriesId.Count > 0)
            {
                var categories = createDTO.CategoriesId.Select(
                    e => new ShopCategory { CategoryId = e, ShopId = shop.Id }
                ).ToArray();
               try
                {
                    await _shopCategoryRepository.CreateRange(cancellationToken, categories);
                }
                catch
                {
                    throw new NotFoundException("Айди категории не найден", "Category id not found");
                }
            }
            if (createDTO.TypesId.Count > 0)
            {
                var types = createDTO.CategoriesId.Select(
                    e => new ShopType { TypeId = e, ShopId = shop.Id }
                ).ToArray();
                try
                {
                    await _shopTypeRepository.CreateRange(cancellationToken, types);
                }
                catch
                {
                    throw new NotFoundException("Айди типа не найден", "Type id not found");
                }
            }
            if (createDTO.ShopDeliveryTypes.Count > 0)
            {
                var deliveryTypes = createDTO.ShopDeliveryTypes.Select(
                    e =>
                        new ShopDeliveryType
                        {
                            DeliveryTypeId = e.Id,
                            ShopId = shop.Id,
                            FreeDeliveryThreshold = e.FreeDeliveryThreshold
                        }
                ).ToArray();
                try
                {
                    await _shopDeliveryTypeRepository.CreateRange(cancellationToken, deliveryTypes);
                }
                catch
                {
                    throw new NotFoundException("Айди типа доставки не найден", "Delivery type id not found");
                }
            }
            if (createDTO.ShopPaymentMethods.Count > 0)
            {
                var paymentMethods = createDTO.ShopPaymentMethods.Select(e => new ShopPaymentMethod
                {
                    PaymentMethodId = e.Id,
                    ShopId = shop.Id,
                    Сommission = e.Сommission
                }).ToArray();
                try
                {
                    await _shopPaymentMethodRepository.CreateRange(cancellationToken, paymentMethods);
                }
                catch
                {
                    throw new NotFoundException("Айди типа оплаты не найден", "Payment method id not found");
                }
            }
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
            if(shop== null)
            {
                throw new NotFoundException("Магазин не найден", "Shop Not Found");
            }
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
                await _imageService.CreateImage(userId, UpdateDTO.Image, UpdateDTO.Id, cancellationToken);
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
                try
                {
                    await _shopCategoryRepository.CreateRange(cancellationToken, categories);
                }
                catch
                {
                    throw new NotFoundException("Айди категории не найден", "Category id not found");
                }
            }
            if (UpdateDTO.TypesId.Count > 0)
            {
                var types = UpdateDTO.CategoriesId.Select(
                    e => new ShopType { TypeId = e, ShopId = shop.Id }
                ).ToArray();
                try
                {
                    await _shopTypeRepository.CreateRange(cancellationToken, types);
                }
                catch
                {
                    throw new NotFoundException("Айди типа не найден", "Type id not found");
                }
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
                try
                {
                    await _shopDeliveryTypeRepository.CreateRange(cancellationToken, deliveryTypes);
                }
                catch
                {
                    throw new NotFoundException("Айди типа доставки не найден", "Delivery type id not found");
                }
            }
            if (UpdateDTO.ShopPaymentMethods.Count > 0)
            {
                var paymentMethods = UpdateDTO.ShopPaymentMethods.Select(e => new ShopPaymentMethod
                {
                    PaymentMethodId = e.Id,
                    ShopId = shop.Id,
                    Сommission = e.Сommission
                }).ToArray();
                try
                {
                    await _shopPaymentMethodRepository.CreateRange(cancellationToken, paymentMethods);
                }
                catch
                {
                    throw new NotFoundException("Айди типа оплаты не найден", "Payment method id not found");
                }
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
            if(shop== null)
            {
                throw new NotFoundException("Магазин не найден", "Shop Not Found");
            }
            user.FavoriteShops.Add(shop);
            await _userRepository.Update(userId, user, cancellationToken);
        }

        public async Task<PageModelDTO<ShopDTO>> ShowUserFavoriteShops(
            Guid userId,
            PaginationDTO filterPaging,
            CancellationToken cancellationToken = default
        )
        {
            var user = await _userRepository.GetById(userId, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException("Пользователь не найден", "User not found");
            }
            PageModelDTO<ShopDTO> result = new PageModelDTO<ShopDTO>();
            var list = await _usersFavoriteShops.GetFavsPageByUserId(userId, filterPaging.PageNumber, filterPaging.PageSize, cancellationToken);
            try
            {
                result = _mapper.Map<PageModelDTO<ShopDTO>>(list);
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

        public async Task<PageModelDTO<ShopDTO>> GetPage(Guid userId, PaginationDTO pagingModel,ShopFilterDTO filter, CancellationToken cancellationToken = default)
        {
            var result = new PageModelDTO<ShopDTO>();
            var user = await _userRepository.GetById(userId);
            var pages = await _repository.GetPage(e => (e.IsActive || e.SellerId == userId || user.Role == Data.Enums.Role.Admin), pagingModel,filter, cancellationToken);
            string basepath = _configuration.GetSection("BaseImagePath").Value;
            try
            {
                result = _mapper.Map<PageModelDTO<ShopDTO>>(pages);
            }
            catch
            {
                throw new MappingException(this);
            }
            foreach (var shop in result.Values)
            {
                var fileinfo = await _staticFileInfoRepository.GetByParentId(shop.Id);
                if (fileinfo != null)
                {
                    shop.ImagePath =
                        basepath
                        + fileinfo.ParentEntityId.ToString()
                        + "/"
                        + fileinfo.Name
                        + "."
                        + fileinfo.Extension;
                }
            }
            return result;
        }
    }
}