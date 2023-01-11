
using AutoMapper;
using Data.Entities;
using Data.IRepositories;
using WebAPi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Logic.Exceptions;
using WebAPi.Exceptions;
using System.Security.Claims;

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
               throw new NotFoundException("Review not found");
            }
            return Ok(entity);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetReviewsByShopId([FromQuery] Guid Id)
        {
            var user = _repository.Users.GetUserByEmail(User.Identity.Name); 
            var list = _repository.Reviews.GetAll().Where(e => (e.ShopId == Id) && (e.IsActive || e.BuyerId == user.Id || user.Role == Data.Enums.Role.Admin)).ToList();
            return Ok(list);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllReviews()
        {
             var user = _repository.Users.GetUserByEmail(User.Identity.Name); 
             var list = _repository.Reviews.GetAll().Where(e => (e.IsActive || e.BuyerId == user.Id || user.Role == Data.Enums.Role.Admin)).ToList();
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
                 throw new MappingException("Ошибка при маппинге",this.GetType().ToString());
            }
            var userid = new Guid(User.Claims.ToArray()[2].Value);
           
            _repository.Reviews.Create(review,userid);
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
                 throw new MappingException("Ошибка при маппинге",this.GetType().ToString());
            }
             var userid = new Guid(User.Claims.ToArray()[2].Value);
           
            _repository.Reviews.Update(review,userid);
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
               throw new NotFoundException("Review id not found");
            }
             var userid = new Guid(User.Claims.ToArray()[2].Value);
            
            _repository.Reviews.Delete(Id,userid);
            _repository.Save();
            return Ok();
        }
    }
}
