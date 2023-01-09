
using AutoMapper;
using Data.Entities;
using Data.IRepositories;
using WebAPi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private ILogger<ReviewController> _logger;

        public ReviewController(IRepositoryWrapper repository,
            IMapper mapper,
            ILogger<ReviewController> logger)
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
            if(entity == null)
            {
                _logger.LogError("Review not found");
                return NotFound("Review not found");
            }
            return Ok(entity);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetReviewsByShopId([FromQuery] Guid Id)
        {
            var list = _repository.Reviews.GetAll().Where(e => e.ShopId == Id).ToList();
            return Ok(list);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllReviews()
        {
             var list = _repository.Reviews.GetAll().ToList();
             return Ok(list);
        }
        [HttpPost]
        [Authorize(Roles = "Buyer,Admin")]
        public async Task<IActionResult> CreateReview([FromBody] ReviewModel model)
        {
            if(!ModelState.IsValid)
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
                 _logger.LogError("Error while mapping");
                return StatusCode(500);
            }
            review.Id = Guid.NewGuid();
            var user = _repository.Users.GetAll().FirstOrDefault(e => e.Email == User.Identity.Name);
            review.CreatorId = user.Id;
            review.UpdatorId = user.Id;
            _repository.Reviews.Create(review);
            _repository.Save();
            return Ok(review.Id);
        }
        [HttpPut]
        [Authorize(Roles = "Buyer,Admin")]
        public async Task<IActionResult> UpdateReview([FromBody] ReviewModel model)
        {
            if(!ModelState.IsValid)
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
                 _logger.LogError("Error while mapping");
                return StatusCode(500);
            }
            var user = _repository.Users.GetAll().FirstOrDefault(e => e.Email == User.Identity.Name);
            review.UpdatorId = user.Id;
            _repository.Reviews.Update(review);
            _repository.Save();
            return Ok();
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteReview([FromQuery] Guid Id)
        {
            Review review = _repository.Reviews.GetById(Id);
            if(review == null)
            {
                _logger.LogError("Review not found");
                return NotFound("Not found this Id");
            }
            var user = _repository.Users.GetAll().FirstOrDefault(e => e.Email == User.Identity.Name);
            review.DeletorId = user.Id;
            _repository.Reviews.Delete(Id);
            _repository.Save();
            return Ok();
        }
    }
}
