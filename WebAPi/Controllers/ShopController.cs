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
            var user = _repository.Users.GetUserByEmail(User.Identity.Name).Result;
            var list = _repository.Shops.GetAll().Where(e => (e.IsActive || user.Id == e.SellerId || user.Role == Data.Enums.Role.Admin)).ToList();
            List<ShopDTO> result = new List<ShopDTO>();
            string basepath = _configuration.GetSection("BaseImagePath").Value;
            try
            {
                foreach(var item in list )
                {
                    ShopDTO dto = new ShopDTO();
                    var fileinfo = _repository.StaticFileInfos.GetByParentId(item.Id).Result;
                    dto = _mapper.Map<ShopDTO>(item);
                    dto.ImagePath = basepath + fileinfo.ParentEntityId.ToString() + "/" + fileinfo.Name + "." + fileinfo.Extension; 
                    result.Add(dto);
                }
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
            var entity = _repository.Shops.GetById(Id).Result;
            if(entity == null)
            {
               throw new NotFoundException("Магазин не найден","Shop not found");
            }
            ShopDTO result = new ShopDTO();
            string basepath = _configuration.GetSection("BaseImagePath").Value;
            try
            { 
                var fileinfo = _repository.StaticFileInfos.GetByParentId(entity.Id).Result;
                result = _mapper.Map<ShopDTO>(entity);
                result.ImagePath = basepath + fileinfo.ParentEntityId.ToString() + "/" + fileinfo.Name + "." + fileinfo.Extension; 
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
            var result = await _shopService.Create(UserId,model);  
            return Ok(result);
            
        }

        [HttpPut]
        [Authorize(Roles = "Seller, Admin")]
        public async Task<IActionResult> UpdateShop([FromForm] UpdateShopModel model)
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
            Shop shop = _repository.Shops.GetById(Id).Result;
            if(shop == null)
            {
                throw new NotFoundException("Магазин не найден","Shop not found");
            }
           var userid = new Guid(User.Claims.ToArray()[2].Value);
            _repository.Shops.Delete(Id, userid);
            _repository.Save();
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
