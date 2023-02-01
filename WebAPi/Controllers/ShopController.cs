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
using Data.DTO.Shop;
using Data.DTO.Review;

namespace WebAPi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShopController : BaseGenericController<Shop,ShopDTO,CreateShopDTO,UpdateShopDTO,IShopRepository,IShopService>
    {

        public ShopController(IShopService shopService) : base(shopService) { }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllShops()
        {
            var result = await _service.GetAll(UserId);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetShopById([FromQuery] Guid Id)
        {
            var result = await _service.GetById(Id);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Seller, Admin")]
        public async Task<IActionResult> CreateShop([FromBody] CreateShopDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.Create(UserId, model);
            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "Seller, Admin")]
        public async Task<IActionResult> UpdateShop([FromBody] UpdateShopDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.Update(UserId, model);

            return Ok(result);
        }

        [HttpDelete]
        [Authorize(Roles = "Seller, Admin")]
        public async Task<IActionResult> DeleteShop([FromQuery] Guid Id)
        {
            await _service.Delete(UserId, Id);
            return Ok();
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> AddShopToFavorites([FromBody] Guid shopId)
        {
            await _service.AddShopToFavorites(UserId, shopId);
            return Ok();
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteFavoriteShop([FromBody] Guid shopId)
        {
            await _service.DeleteShopFromFavorites(UserId, shopId);
            return Ok();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ShowUserFavoriteShops([FromQuery] Guid userId)
        {
            var result = await _service.ShowUserFavoriteShops(userId);
            return Ok(result);
        }
    }
}
