﻿using AutoMapper;
using Data.DTO;
using Data.Entities;
using Data.IRepositories;
using Data.Models;
using Data.Repositories;
using Logic.Exceptions;
using Logic.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPi.Interfaces;

namespace Logic.Services
{
    public class ShopService : BaseService<Shop, ShopDTO, ShopModel, UpdateShopModel>, IShopService
    {
        private IINNService _iNNService;
        private IRepositoryWrapper _repositoryWrapper;
        private IImageService _imageService;
        private IConfiguration _configuration;

        public ShopService(
            IShopRepository repository,
            IMapper mapper,
            ILogger<ShopService> logger,
            IINNService iNNService,
            IRepositoryWrapper repositoryWrapper,
            IImageService imageService,
            IConfiguration configuration
        ) : base(repository, mapper, logger)
        {
            _iNNService = iNNService;
            _repositoryWrapper = repositoryWrapper;
            _imageService = imageService;
            _configuration = configuration;
        }

        public async Task<List<ShopDTO>> GetAll(
            Guid userid,
            CancellationToken cancellationToken = default
        )
        {
            var user = await _repositoryWrapper.Users.GetById(userid, cancellationToken);
            var list = _repository
                .GetAll()
                .Where(e => (e.IsActive || user.Id == e.SellerId || user.Role == Data.Enums.Role.Admin)).ToList();
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

        public override Task<Guid> Create(
            Guid userId,
            ShopModel createDTO,
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
            shop.CreatorId = userId;
            shop.UpdatorId = userId;
            shop.CreateDateTime = DateTime.UtcNow;
            shop.UpdateDateTime = DateTime.UtcNow;
            var result = _repository.Create(shop);
            _repositoryWrapper.Save();
            foreach (var item in createDTO.CategoriesId)
            {
                shop.Categories.Add(_repositoryWrapper.Categories.GetById(item).Result);
            }
            foreach (var item in createDTO.DeliveryTypesId)
            {
                shop.DeliveryTypes.Add(_repositoryWrapper.DeliveryTypes.GetById(item).Result);
            }
            foreach (var item in createDTO.PaymentMethodsId)
            {
                shop.PaymentMethods.Add(_repositoryWrapper.PaymentMethods.GetById(item).Result);
            }
            foreach (var item in createDTO.TypesId)
            {
                shop.Types.Add(_repositoryWrapper.Types.GetById(item).Result);
            }
            _repository.Update(shop);
            _repositoryWrapper.Save();
            return result;
        }

        public override async Task<UpdateShopModel> Update(
            Guid userId,
            UpdateShopModel UpdateDTO,
            CancellationToken cancellationToken = default
        )
        {
            if (!_iNNService.CheckINN(UpdateDTO.INN))
            {
                throw new NotFoundException("INN не найден", "INN not valid");
            }
            Shop shop = await _repository.GetById(UpdateDTO.Id, cancellationToken);
            shop.Title = UpdateDTO.Title;
            shop.Description = UpdateDTO.Description;
            shop.INN = UpdateDTO.INN;
            if (UpdateDTO.Image != null)
            {
                await _imageService.CreateImage(UpdateDTO.Image, UpdateDTO.Id, cancellationToken);
            }
            shop.UpdatorId = userId;
            shop.UpdateDateTime = DateTime.UtcNow;
            _repository.Update(shop);
            _repositoryWrapper.Save();
            foreach (var item in UpdateDTO.CategoriesId)
            {
                shop.Categories.Add(_repositoryWrapper.Categories.GetById(item).Result);
            }
            foreach (var item in UpdateDTO.DeliveryTypesId)
            {
                shop.DeliveryTypes.Add(_repositoryWrapper.DeliveryTypes.GetById(item).Result);
            }
            foreach (var item in UpdateDTO.PaymentMethodsId)
            {
                shop.PaymentMethods.Add(_repositoryWrapper.PaymentMethods.GetById(item).Result);
            }
            foreach (var item in UpdateDTO.TypesId)
            {
                shop.Types.Add(_repositoryWrapper.Types.GetById(item).Result);
            }
            _repository.Update(shop);
            _repositoryWrapper.Save();
            return UpdateDTO;
        }
    }
}