using Data.DTO;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteShopsController : ControllerBase
    {
        private IShopService _service;
        public FavoriteShopsController(IShopService service)
        {
            _service = service;
        }
        /// <summary>
        /// Добавить магазин в избранное
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> AddShopToFavorites(Guid id)
        {
            await _service.AddShopToFavorites(id);
            return Ok();
        }
        /// <summary>
        /// Удалить магазин из избранного
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavoriteShop(Guid id)
        {
            await _service.DeleteShopFromFavorites(id);
            return Ok();
        }
        /// <summary>
        /// Показать избранные магазины пользователя
        /// </summary>
        /// <param name="filterPaging">Модель пагинации</param>
        /// <returns>Страница магазинов</returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ShowUserFavoriteShops([FromQuery] PaginationDTO filterPaging)
        {
            var result = await _service.ShowUserFavoriteShops(filterPaging);
            return Ok(result);
        }
    }
}
