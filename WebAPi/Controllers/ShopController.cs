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
using WebAPi.Exceptions;
using System.Security.Claims;
using Data.DTO;

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
            var user = _repository.Users.GetUserByEmail(User.Identity.Name); 
            var list = _repository.Shops.GetAll().Where(e => (e.IsActive || user.Id == e.SellerId || user.Role == Data.Enums.Role.Admin)).AsQueryable();
            List<ShopDTO> result = new List<ShopDTO>();
            try
            {
                result = _mapper.ProjectTo<ShopDTO>(list).ToList();
            }
            catch
            {
                throw new MappingException(this.GetType().ToString());
            }
            return Ok(result);        
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetShopById([FromQuery] Guid Id)
        {
            var entity = _repository.Shops.GetById(Id);
            if(entity == null)
            {
               throw new NotFoundException("Магазин не найден","Shop not found");
            }
            ShopDTO result = new ShopDTO();
            try
            {
                result = _mapper.Map<ShopDTO>(entity);
            }
            catch
            {
                throw new MappingException(this.GetType().ToString());
            }
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
            if(_iNNService.CheckINN("770303580308"))
            {
                throw new NotFoundException("INN не найден", "INN not valid");
            }
        
            Shop shop = new Shop();
            try
            {
                shop = _mapper.Map<Shop>(model);
            }
            catch
            {
                  throw new MappingException(this.GetType().ToString());
            }
            _logger.LogError(User.Claims.ToArray()[2].Value);
  
          var userid = new Guid(User.Claims.ToArray()[2].Value);
            _repository.Shops.Create(shop,userid);
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
               throw new NotFoundException("INN не найден", "INN not valid");
            }
            Shop shop = new Shop();
            try
            {
                shop = _mapper.Map<Shop>(model);
            }
            catch
            {
                   throw new MappingException(this.GetType().ToString());
            }
            var userid = new Guid(User.Claims.ToArray()[2].Value);
            _repository.Shops.Update(shop,userid);
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
                throw new NotFoundException("Магазин не найден","Shop not found");
            }
           var userid = new Guid(User.Claims.ToArray()[2].Value);
            _repository.Shops.Delete(Id, userid);
            _repository.Save();
            return Ok();
        }
    }
}
