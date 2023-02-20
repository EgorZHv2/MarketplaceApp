using Data.DTO;
using Data.DTO.Review;
using Data.Entities;
using Data.IRepositories;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReviewController : BaseGenericController<ReviewEntity, ReviewDTO, CreateReviewDTO, UpdateReviewDTO, IReviewRepository, IReviewService>
    {
        public ReviewController(IReviewService reviewService) : base(reviewService)
        {
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetReviewById([FromQuery] Guid Id)
        {
            var result = await _service.GetById(Id);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetReviewsByShopId([FromQuery] Guid shopId)
        {
            var result = await _service.GetReviewsByShopId(UserId, shopId);
            return Ok(result);
        }

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

        [HttpDelete]
       [Authorize(Roles = nameof(Data.Enums.Role.Admin))]
        public async Task<IActionResult> DeleteReview([FromQuery] Guid Id)
        {
            await _service.Delete(UserId, Id);
            return Ok();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetPage([FromQuery] PaginationDTO model)
        {
            var result = await _service.GetPage(UserId, model);
            return Ok(result);
        }
    }
}