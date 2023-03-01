using Data.DTO;
using Data.DTO.Review;
using Data.Entities;
using Data.IRepositories;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : BaseGenericController<ReviewEntity, ReviewDTO, CreateReviewDTO, UpdateReviewDTO, IReviewRepository, IReviewService>
    {
        public ReviewController(IReviewService reviewService) : base(reviewService)
        {
        }
        /// <summary>
        /// Получить отзыв по id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetReviewById([FromQuery] Guid id)
        {
            var result = await _service.GetById(id);
            return Ok(result);
        }
        /// <summary>
        /// Получить отзывы по айди магазина
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        [HttpGet("get-by-shop-id")]
        [Authorize]
        public async Task<IActionResult> GetReviewsByShopId([FromQuery] Guid shopId)
        {
            var result = await _service.GetReviewsByShopId(UserId, shopId);
            return Ok(result);
        }
        /// <summary>
        /// Создать отзыв
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = $"{nameof(Data.Enums.Role.Admin)},{nameof(Data.Enums.Role.Buyer)}")]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.Create(UserId, model);
            return Ok(result);
        }
        /// <summary>
        /// Изменить отзыв
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = $"{nameof(Data.Enums.Role.Admin)},{nameof(Data.Enums.Role.Buyer)}")]
        public async Task<IActionResult> UpdateReview([FromBody] UpdateReviewDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.Update(UserId, model);
            return Ok(result);
        }
        /// <summary>
        /// Удалить отзыв
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       [HttpDelete]
       [Authorize(Roles = nameof(Data.Enums.Role.Admin))]
        public async Task<IActionResult> DeleteReview([FromQuery] Guid id)
        {
            await _service.Delete(UserId, id);
            return Ok();
        }
        /// <summary>
        /// Получить страницу с отзывами
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetPage([FromQuery] PaginationDTO model)
        {
            var result = await _service.GetPage(UserId, model);
            return Ok(result);
        }
    }
}