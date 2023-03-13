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
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : BaseGenericController<ShopEntity, ShopDTO, CreateShopDTO, UpdateShopDTO, IShopRepository, IShopService>
    {
        public ShopController(IShopService shopService) : base(shopService)
        {
        }
        /// <summary>
        /// Получить магазин по айди
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetShopById(Guid id)
        {
            var result = await _service.GetById(id);
            return Ok(result);
        }
        /// <summary>
        /// Создать магазин
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = $"{nameof(Data.Enums.Role.Admin)},{nameof(Data.Enums.Role.Seller)}")]
        public async Task<IActionResult> CreateShop([FromBody] CreateShopDTO model)
        {
            var result = await _service.Create(model);
            return Ok(result);
        }
        /// <summary>
        /// Изменить магазин
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = $"{nameof(Data.Enums.Role.Admin)},{nameof(Data.Enums.Role.Seller)}")]
        public async Task<IActionResult> UpdateShop([FromForm] UpdateShopDTO model)
        {
            await _service.Update(model);
            return Ok();
        }

        /// <summary>
        /// Удалить магазин
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = $"{nameof(Data.Enums.Role.Admin)},{nameof(Data.Enums.Role.Seller)}")]
        public async Task<IActionResult> DeleteShop( Guid id)
        {
            await _service.Delete(id);
            return Ok();
        }

       
        /// <summary>
        /// Получить страницу магазинов
        /// </summary>
        /// <param name="model"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetPage([FromQuery] PaginationDTO model, [FromQuery] ShopFilterDTO filter)
        {
            var result = await _service.GetPage(model,filter);
            return Ok(result);
        }


        [HttpPost("add-products-from-xml")]
        [Authorize(Roles = nameof(Data.Enums.Role.Admin))]
        public async Task<IActionResult> UploadFromExcel(Guid shopId,[FromForm]UploadFileDTO dto)
        {
            await _service.AddProductsToShopFromXML(shopId, dto.File);
            return Ok();
        }
    }
}