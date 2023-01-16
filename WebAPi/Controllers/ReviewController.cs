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

namespace WebAPi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private ILogger<ReviewController> _logger;

        public ReviewController(
            IRepositoryWrapper repository,
            IMapper mapper,
            ILogger<ReviewController> logger
        )
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetReviewById([FromQuery] Guid Id)
        {
            var entity = _repository.Reviews.GetById(Id);
            ReviewDTO result = new ReviewDTO();
            if (entity == null)
            {
                throw new NotFoundException("Отзыв не найден", "Review not found");
            }
            try
            {
                result = _mapper.Map<ReviewDTO>(entity);
            }
            catch
            {
                throw new MappingException(this.GetType().ToString());
            }
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetReviewsByShopId([FromQuery] Guid Id)
        {
            var user = _repository.Users.GetUserByEmail(User.Identity.Name).Result;
            var list = _repository.Reviews
                .GetAll()
                .Where(
                    e =>
                        (e.ShopId == Id)
                        && (
                            e.IsActive || e.BuyerId == user.Id || user.Role == Data.Enums.Role.Admin
                        )
                )
                .AsQueryable();
            List<ReviewDTO> result = new List<ReviewDTO>();
            try
            {
                result = _mapper.ProjectTo<ReviewDTO>(list).ToList();
            }
            catch
            {
                throw new MappingException(this.GetType().ToString());
            }
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllReviews()
        {
            var user = _repository.Users.GetUserByEmail(User.Identity.Name).Result;
            var list = _repository.Reviews
                .GetAll()
                .Where(
                    e => (e.IsActive || e.BuyerId == user.Id || user.Role == Data.Enums.Role.Admin)
                )
                .AsQueryable();
            List<ReviewDTO> result = new List<ReviewDTO>();
            try
            {
                result = _mapper.ProjectTo<ReviewDTO>(list).ToList();
            }
            catch
            {
                throw new MappingException(this.GetType().ToString());
            }
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Buyer,Admin")]
        public async Task<IActionResult> CreateReview([FromBody] ReviewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Review review = new Review();
            try
            {
                review = _mapper.Map<Review>(model);
            }
            catch
            {
                throw new MappingException(this.GetType().ToString());
            }
            var userid = new Guid(User.Claims.ToArray()[2].Value);

            _repository.Reviews.Create(review, userid);
            _repository.Save();
            return Ok(review.Id);
        }

        [HttpPut]
        [Authorize(Roles = "Buyer,Admin")]
        public async Task<IActionResult> UpdateReview([FromBody] ReviewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Review review = new Review();
            try
            {
                review = _mapper.Map<Review>(model);
            }
            catch
            {
                throw new MappingException(this.GetType().ToString());
            }
            var userid = new Guid(User.Claims.ToArray()[2].Value);

            _repository.Reviews.Update(review, userid);
            _repository.Save();
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteReview([FromQuery] Guid Id)
        {
            Review review = _repository.Reviews.GetById(Id).Result;
            if (review == null)
            {
                throw new NotFoundException("Email не найден", "User email not found");
            }
            var userid = new Guid(User.Claims.ToArray()[2].Value);

            _repository.Reviews.Delete(Id, userid);
            _repository.Save();
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
            _repository.Reviews.Update(entity,user.Id);
            _repository.Save();
            return Ok();
        }
    }
}
