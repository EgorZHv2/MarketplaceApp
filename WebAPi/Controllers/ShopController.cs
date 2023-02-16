using Data.DTO;
using Data.DTO.Filters;
using Data.DTO.Shop;
using Data.Entities;
using Data.IRepositories;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShopController : BaseGenericController<Shop, ShopDTO, CreateShopDTO, UpdateShopDTO, IShopRepository, IShopService>
    {
        public ShopController(IShopService shopService) : base(shopService)
        {
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetShopById([FromQuery] Guid Id)
        {
            var result = await _service.GetById(Id);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = $"{nameof(Data.Enums.Role.Admin)},{nameof(Data.Enums.Role.Seller)}")]
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
        [Authorize(Roles = $"{nameof(Data.Enums.Role.Admin)},{nameof(Data.Enums.Role.Seller)}")]
        public async Task<IActionResult> UpdateShop([FromForm] UpdateShopDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.Update(UserId, model);

            return Ok(result);
        }

        [HttpDelete]
        [Authorize(Roles = $"{nameof(Data.Enums.Role.Admin)},{nameof(Data.Enums.Role.Seller)}")]
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
        public async Task<IActionResult> ShowUserFavoriteShops([FromQuery] PaginationDTO filterPaging)
        {
            var result = await _service.ShowUserFavoriteShops(UserId, filterPaging);
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetPage([FromQuery] PaginationDTO model, [FromQuery] ShopFilterDTO filter)
        {
            var result = await _service.GetPage(UserId, model,filter);
            return Ok(result);
        }
    }
}