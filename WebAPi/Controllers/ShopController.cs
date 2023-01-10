﻿using AutoMapper;
using Data.Entities;
using Data.IRepositories;
using WebAPi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dadata;
using Dadata.Model;
using WebAPi.Interfaces;
using Logic.Exceptions;
using WebAPi.Exceptions;

namespace WebAPi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private ILogger<ShopController> _logger;
        private IINNService _iNNService;
        public ShopController(
            IRepositoryWrapper repository,
            IMapper mapper,
            ILogger<ShopController> logger,
            IINNService iNNService
        )
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _iNNService = iNNService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllShops()
        {
            var list = _repository.Shops.GetAll().ToList();
            return Ok(list);

            
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetShopById([FromQuery] Guid Id)
        {
            var entity = _repository.Shops.GetById(Id);
            if(entity == null)
            {
               throw new NotFoundException("Shop not found");
            }
            return Ok(entity);
           
        }

        [HttpPost]
        [Authorize(Roles = "Seller, Admin")]
        public async Task<IActionResult> CreateShop([FromBody] ShopModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(_iNNService.CheckINN("770303580308"))
            {
                throw new NotFoundException("INN not valid");
            }
        
            Shop shop = new Shop();
            try
            {
                shop = _mapper.Map<Shop>(model);
            }
            catch
            {
                 throw new MappingException("Ошибка при маппинге",this.GetType().ToString());
            }
            shop.Id = Guid.NewGuid();
            var user = _repository.Users.GetAll().FirstOrDefault(e => e.Email == User.Identity.Name);
            shop.CreatorId = user.Id;
            shop.UpdatorId = user.Id;
            _repository.Shops.Create(shop);
            _repository.Save();
            return Ok(shop.Id);
            
        }

        [HttpPut]
        [Authorize(Roles = "Seller, Admin")]
        public async Task<IActionResult> UpdateShop([FromBody] ShopModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(!_iNNService.CheckINN(model.INN))
            {
                throw new NotFoundException("INN not valid");
            }
            Shop shop = new Shop();
            try
            {
                shop = _mapper.Map<Shop>(model);
            }
            catch
            {
                  throw new MappingException("Ошибка при маппинге",this.GetType().ToString());
            }
             var user = _repository.Users.GetAll().FirstOrDefault(e => e.Email == User.Identity.Name);
            shop.UpdatorId = user.Id;
            _repository.Shops.Update(shop);
            _repository.Save();
        
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Seller, Admin")]
        public async Task<IActionResult> DeleteShop([FromQuery] Guid Id)
        {
            Shop shop = _repository.Shops.GetById(Id);
            if(shop == null)
            {
                throw new NotFoundException("Shop Id not found");
            }
            var user = _repository.Users.GetAll().FirstOrDefault(e => e.Email == User.Identity.Name);
            shop.DeletorId = user.Id;
            _repository.Shops.Delete(Id);
            _repository.Save();
            return Ok();
        }
    }
}
