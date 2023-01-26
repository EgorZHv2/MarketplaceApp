using AutoMapper;
using Data.Entities;
using Data.IRepositories;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dadata;
using Dadata.Model;
using WebAPi.Interfaces;
using Logic.Exceptions;
using System.Security.Claims;
using Data.DTO;
using Logic.Interfaces;

namespace WebAPi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShopController:BaseController
    {
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private ILogger<ShopController> _logger;
        private IINNService _iNNService;
        private IImageService _imageService;
        private IConfiguration _configuration;
        private IShopService _shopService;
        public ShopController(
            IRepositoryWrapper repository,
            IMapper mapper,
            ILogger<ShopController> logger,
            IINNService iNNService,
            IImageService imageService,
            IConfiguration configuration,
            IShopService shopService
            
        )
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _iNNService = iNNService;
            _imageService = imageService;
            _configuration = configuration;
            _shopService = shopService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllShops()
        {
            var result = await _shopService.GetAll(UserId);
            return Ok(result);        
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetShopById([FromQuery] Guid Id)
        {
            var result = await _shopService.GetById(Id);
            return Ok(result);
           
        }

        [HttpPost]
        [Authorize(Roles = "Seller, Admin")]
        public async Task<IActionResult> CreateShop([FromBody] ShopModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _shopService.Create(UserId,model);  
            return Ok(result);
            
        }

        [HttpPut]
        [Authorize(Roles = "Seller, Admin")]
        public async Task<IActionResult> UpdateShop([FromBody] UpdateShopModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _shopService.Update(UserId, model);
        
            return Ok(result);
        }

        [HttpDelete]
        [Authorize(Roles = "Seller, Admin")]
        public async Task<IActionResult> DeleteShop([FromQuery] Guid Id)
        {
            await _shopService.Delete(UserId,Id);
            return Ok();
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeShopActivity([FromBody] EntityActivityModel model)
        {
            var user = _repository.Users.GetUserByEmail(User.Identity.Name).Result;
            var entity = _repository.Shops.GetById(model.Id).Result;
            if(entity == null)
            {
                throw new NotFoundException("Магазин не найден","Shop not found");
            }
            entity.IsActive = model.IsActive;
            _repository.Shops.Update(entity);
            _repository.Save();
            return Ok();
        }
    }
}
