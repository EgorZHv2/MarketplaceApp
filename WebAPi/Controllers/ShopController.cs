using AutoMapper;
using Data.Entities;
using Data.IRepositories;
using Logic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dadata;
using Dadata.Model;
using Logic.Interfaces;

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
                _logger.LogError("Review not found");
                return NotFound("Review not found");
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
            if(!_iNNService.CheckINN(model.INN))
            {
                return NotFound("Inn not valid");
            }
        
            Shop shop = new Shop();
            try
            {
                shop = _mapper.Map<Shop>(model);
            }
            catch
            {
                 _logger.LogError("Error while mapping");
                return StatusCode(500);
            }
            shop.Id = Guid.NewGuid();
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
                return NotFound("Inn not valid");
            }
            Shop shop = new Shop();
            try
            {
                shop = _mapper.Map<Shop>(model);
            }
            catch
            {
                 _logger.LogError("Error while mapping");
                return StatusCode(500);
            }
           
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
                _logger.LogError("Shop not found");
                return NotFound("Not found this Id");
            }
            _repository.Shops.Delete(Id);
            _repository.Save();
            return Ok();
        }
    }
}
