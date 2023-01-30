using AutoMapper;
using Data.Entities;
using Data.IRepositories;
using Data.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Logic.Exceptions;
using System.Security.Claims;
using Data.DTO;
using Data.DTO.Review;
using Logic.Interfaces;

namespace WebAPi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReviewController : BaseController
    {
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private ILogger<ReviewController> _logger;
        private IReviewService _reviewService;

        public ReviewController(
            IRepositoryWrapper repository,
            IMapper mapper,
            ILogger<ReviewController> logger,
            IReviewService reviewService
        )
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _reviewService = reviewService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetReviewById([FromQuery] Guid Id)
        {
            var result = await _reviewService.GetById(Id);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetReviewsByShopId([FromQuery] Guid shopId)
        {
            var result = await _reviewService.GetReviewsByShopId(UserId,shopId);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllReviews()
        {
            var result = await _reviewService.GetAll(UserId);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Buyer,Admin")]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _reviewService.Create(UserId,model);
            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "Buyer,Admin")]
        public async Task<IActionResult> UpdateReview([FromBody] UpdateReviewDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _reviewService.Update(UserId, model);
            return Ok(result);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteReview([FromQuery] Guid Id)
        {
            await _reviewService.Delete(UserId, Id);
            return Ok();
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeReviewActivity([FromBody] EntityActivityModel model)
        {
            var user = _repository.Users.GetUserByEmail(User.Identity.Name).Result;
            var entity = _repository.Reviews.GetById(model.Id).Result;
            if(entity == null)
            {
                throw new NotFoundException("Отзыв не найден", "Review not found");
            }
            entity.IsActive = model.IsActive;
            _repository.Reviews.Update(entity);
            _repository.Save();
            return Ok();
        }
    }
}
