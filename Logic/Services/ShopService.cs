﻿using AutoMapper;
using Data.DTO;
using Data.DTO.Filters;
using Data.DTO.ProductXmlModels;
using Data.DTO.Shop;
using Data.Entities;
using Data.IRepositories;
using Data.Options;
using Logic.Exceptions;
using Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;
using WebAPi.Interfaces;

namespace Logic.Services
{
    public class ShopService
        : BaseService<ShopEntity, ShopDTO, CreateShopDTO, UpdateShopDTO, IShopRepository>,
            IShopService
    {
        private IINNService _iNNService;
        private IImageService _imageService;
        private ApplicationOptions _options;
        private IUserRepository _userRepository;
        private IUsersFavoriteShopsRepository _usersFavoriteShops;
        private IShopCategoryRepository _shopCategoryRepository;
        private IShopDeliveryTypeRepository _shopDeliveryTypeRepository;
        private IShopPaymentMethodRepository _shopPaymentMethodRepository;
        private IShopTypeRepository _shopTypeRepository;
        private IStaticFileInfoRepository _staticFileInfoRepository;
        private ICategoryRepository _categoryRepository;
        private ITypeRepository _typeRepository;
        private IDeliveryTypeRepository _deliveryTypeRepository;
        private IPaymentMethodRepository _paymentMethodRepository;
        private IProductRepository _productRepository;
        private IShopProductRepository _shopProductRepository;

        public ShopService(
            IShopRepository repository,
            IMapper mapper,
            IINNService iNNService,
            IImageService imageService,
            IOptions<ApplicationOptions> options,
            IUserRepository userRepository,
            IUsersFavoriteShopsRepository usersFavoriteShops,
            IShopCategoryRepository shopCategoryRepository,
            IShopDeliveryTypeRepository shopDeliveryTypeRepository,
            IShopPaymentMethodRepository shopPaymentMethodRepository,
            IShopTypeRepository shopTypeRepository,
            IStaticFileInfoRepository staticFileInfoRepository,
            ICategoryRepository categoryRepository,
            ITypeRepository typeRepository,
            IDeliveryTypeRepository deliveryTypeRepository,
            IPaymentMethodRepository paymentMethodRepository,
            IProductRepository productRepository
,
            IShopProductRepository shopProductRepository

        ) : base(repository, mapper)
        {
            _iNNService = iNNService;
            _imageService = imageService;
            _options = options.Value;
            _userRepository = userRepository;
            _usersFavoriteShops = usersFavoriteShops;
            _shopCategoryRepository = shopCategoryRepository;
            _shopDeliveryTypeRepository = shopDeliveryTypeRepository;
            _shopPaymentMethodRepository = shopPaymentMethodRepository;
            _shopTypeRepository = shopTypeRepository;
            _staticFileInfoRepository = staticFileInfoRepository;
            _categoryRepository = categoryRepository;
            _typeRepository = typeRepository;
            _deliveryTypeRepository = deliveryTypeRepository;
            _paymentMethodRepository = paymentMethodRepository;
            _productRepository = productRepository;
            _shopProductRepository = shopProductRepository;
        }

        public override async Task<Guid> Create(Guid userId, CreateShopDTO createDTO)
        {
            if (!_iNNService.CheckINN(createDTO.INN))
            {
                throw new WrongINNException();
            }

            var shop = _mapper.Map<ShopEntity>(createDTO);

            var result = await _repository.Create(userId, shop);

            if (createDTO.CategoriesId.Any())
            {
                if (
                    createDTO.CategoriesId.Count()
                    != _categoryRepository.GetManyByIds(createDTO.CategoriesId.ToArray()).Count()
                )
                {
                    throw new CategoryNotFoundException();
                }
                var categories = createDTO.CategoriesId
                    .Select(e => new ShopCategoryEntity { CategoryId = e, ShopId = shop.Id })
                    .ToArray();

                await _shopCategoryRepository.CreateRange(categories);
            }
            if (createDTO.TypesId.Any())
            {
                if (
                    createDTO.TypesId.Count()
                    != _typeRepository.GetManyByIds(createDTO.TypesId.ToArray()).Count()
                )
                {
                    throw new TypeNotFoundException();
                }
                var types = createDTO.CategoriesId
                    .Select(e => new ShopTypeEntity { TypeId = e, ShopId = shop.Id })
                    .ToArray();

                await _shopTypeRepository.CreateRange(types);
            }
            if (createDTO.ShopDeliveryTypes.Any())
            {
                if (
                    createDTO.ShopDeliveryTypes.Count()
                    != _deliveryTypeRepository
                        .GetManyByIds(createDTO.ShopDeliveryTypes.Select(e => e.Id).ToArray())
                        .Count()
                )
                {
                    throw new DeliveryTypeNotFoundException();
                }
                var deliveryTypes = createDTO.ShopDeliveryTypes
                    .Select(
                        e =>
                            new ShopDeliveryTypeEntity
                            {
                                DeliveryTypeId = e.Id,
                                ShopId = shop.Id,
                                FreeDeliveryThreshold = e.FreeDeliveryThreshold
                            }
                    )
                    .ToArray();

                await _shopDeliveryTypeRepository.CreateRange(deliveryTypes);
            }
            if (createDTO.ShopPaymentMethods.Any())
            {
                if (
                    createDTO.ShopPaymentMethods.Count()
                    != _paymentMethodRepository
                        .GetManyByIds(createDTO.ShopPaymentMethods.Select(e => e.Id).ToArray())
                        .Count()
                )
                {
                    throw new PaymentMethodNotFoundException();
                }
                var paymentMethods = createDTO.ShopPaymentMethods
                    .Select(
                        e =>
                            new ShopPaymentMethodEntity
                            {
                                PaymentMethodId = e.Id,
                                ShopId = shop.Id,
                                Сommission = e.Сommission
                            }
                    )
                    .ToArray();

                await _shopPaymentMethodRepository.CreateRange(paymentMethods);
            }
            return result;
        }

        public override async Task<UpdateShopDTO> Update(Guid userId, UpdateShopDTO updateDTO)
        {
            if (!_iNNService.CheckINN(updateDTO.INN))
            {
                throw new WrongINNException();
            }
            var shop = await _repository.GetById(updateDTO.Id);
            if (shop == null)
            {
                throw new ShopNotFoundException();
            }

            _mapper.Map(updateDTO, shop);

            if (updateDTO.Image != null)
            {
                await _imageService.CreateImage(userId, updateDTO.Image, updateDTO.Id);
            }
            await _repository.Update(userId, shop);

            await _shopCategoryRepository.DeleteAllByShop(shop);
            await _shopDeliveryTypeRepository.DeleteAllByShop(shop);
            await _shopPaymentMethodRepository.DeleteAllByShop(shop);
            await _shopTypeRepository.DeleteAllByShop(shop);
            if (updateDTO.CategoriesId.Any())
            {
                if (
                    updateDTO.CategoriesId.Count()
                    != _categoryRepository.GetManyByIds(updateDTO.CategoriesId.ToArray()).Count()
                )
                {
                    throw new CategoryNotFoundException();
                }
                var categories = updateDTO.CategoriesId
                    .Select(e => new ShopCategoryEntity { CategoryId = e, ShopId = shop.Id })
                    .ToArray();

                await _shopCategoryRepository.CreateRange(categories);
            }
            if (updateDTO.TypesId.Any())
            {
                if (
                    updateDTO.TypesId.Count()
                    != _typeRepository.GetManyByIds(updateDTO.TypesId.ToArray()).Count()
                )
                {
                    throw new TypeNotFoundException();
                }
                var types = updateDTO.CategoriesId
                    .Select(e => new ShopTypeEntity { TypeId = e, ShopId = shop.Id })
                    .ToArray();
                await _shopTypeRepository.CreateRange(types);
            }
            if (updateDTO.ShopDeliveryTypes.Any())
            {
                if (
                    updateDTO.ShopDeliveryTypes.Count()
                    != _deliveryTypeRepository
                        .GetManyByIds(updateDTO.ShopDeliveryTypes.Select(e => e.Id).ToArray())
                        .Count()
                )
                {
                    throw new DeliveryTypeNotFoundException();
                }
                var deliveryTypes = updateDTO.ShopDeliveryTypes
                    .Select(
                        e =>
                            new ShopDeliveryTypeEntity
                            {
                                DeliveryTypeId = e.Id,
                                ShopId = shop.Id,
                                FreeDeliveryThreshold = e.FreeDeliveryThreshold
                            }
                    )
                    .ToArray();

                await _shopDeliveryTypeRepository.CreateRange(deliveryTypes);
            }
            if (updateDTO.ShopPaymentMethods.Any())
            {
                if (
                    updateDTO.ShopPaymentMethods.Count()
                    != _paymentMethodRepository
                        .GetManyByIds(updateDTO.ShopPaymentMethods.Select(e => e.Id).ToArray())
                        .Count()
                )
                {
                    throw new PaymentMethodNotFoundException();
                }
                var paymentMethods = updateDTO.ShopPaymentMethods
                    .Select(
                        e =>
                            new ShopPaymentMethodEntity
                            {
                                PaymentMethodId = e.Id,
                                ShopId = shop.Id,
                                Сommission = e.Сommission
                            }
                    )
                    .ToArray();

                await _shopPaymentMethodRepository.CreateRange(paymentMethods);
            }
            return updateDTO;
        }

        public async Task AddShopToFavorites(Guid userId, Guid shopId)
        {
            var user = await _userRepository.GetById(userId);
            var existing = await _usersFavoriteShops.GetFavByShopAndUserId(userId, shopId);
            if (existing != null)
            {
                throw new AlreadyExistsException(
                    "Магазин уже в избранном",
                    "Shop already in favorites table"
                );
            }
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            var shop = await _repository.GetById(shopId);
            if (shop == null)
            {
                throw new ShopNotFoundException();
            }
            user.FavoriteShops.Add(shop);
            await _userRepository.Update(userId, user);
        }

        public async Task<PageModelDTO<ShopDTO>> ShowUserFavoriteShops(
            Guid userId,
            PaginationDTO filterPaging
        )
        {
            var user = await _userRepository.GetById(userId);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var list = await _usersFavoriteShops.GetFavsPageByUserId(
                userId,
                filterPaging.PageNumber,
                filterPaging.PageSize
            );
            var result = _mapper.Map<PageModelDTO<ShopDTO>>(list);
            return result;
        }

        public async Task DeleteShopFromFavorites(Guid userId, Guid shopId)
        {
            var existing = await _usersFavoriteShops.GetFavByShopAndUserId(userId, shopId);
            if (existing == null)
            {
                throw new ShopNotFoundException();
            }
            await _usersFavoriteShops.Delete(existing);
        }

        public async Task<PageModelDTO<ShopDTO>> GetPage(
            Guid userId,
            PaginationDTO pagingModel,
            ShopFilterDTO filter
        )
        {
            var user = await _userRepository.GetById(userId);
            var pages = await _repository.GetPage(user, pagingModel, filter);
            string basepath = _options.BaseImagePath;
            var result = _mapper.Map<PageModelDTO<ShopDTO>>(pages);
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

        public async Task AddProductsToShopFromXML(Guid shopId,IFormFile xmlFile)
        {
            var shop = _repository.GetById(shopId); 
            if (shop == null) 
            {
                throw new ShopNotFoundException();
            }
            var shopProducts = new List<ShopProductEntity>();
            var list = new List<Offer>();
            var settings = new XmlReaderSettings();
            settings.ProhibitDtd = false;
            using (var reader = XmlReader.Create(xmlFile.OpenReadStream(), settings))
            {
                var xmlSerializer = new XmlSerializer(typeof(Shop));
                list.AddRange((xmlSerializer.Deserialize(reader) as Shop).offers);
            }
            foreach (var item in list)
            {
                var product = await _productRepository.GetByPartNumber(item.vendorCode);
                if (product == null)
                {
                    throw new ProductNotFoundException();
                }
                shopProducts.Add(new ShopProductEntity
                {
                    ShopId = shopId,
                    ProductId = product.Id,
                    Quantity = item.count,
                    Price = item.price,
                    Description = item.description
                });
            }
            await _shopProductRepository.CreateRange(shopProducts.ToArray());
          
            
        }
    }
}
