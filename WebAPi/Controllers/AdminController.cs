using AutoMapper;
using Data.IRepositories;
using Logic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private ILogger<AdminController> _logger;
        private IRepositoryWrapper _repositoryWrapper;
        private IMapper _mapper;

        public AdminController(
            ILogger<AdminController> logger,
            IRepositoryWrapper repositoryWrapper,
            IMapper mapper
        )
        {
            _logger = logger;
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }
        [HttpPut]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> ChangeEntityActivity(EntityActivityModel model)
        {
            var user = _repositoryWrapper.Users.GetById(model.Id);
            if (user != null) 
            {
                user.IsActive = model.IsActive;
                _repositoryWrapper.Users.Update(user);
            }
            var shop = _repositoryWrapper.Shops.GetById(model.Id);
            if(shop != null)
            {
                shop.IsActive = model.IsActive;
                _repositoryWrapper.Shops.Update(shop);
            }
            var review = _repositoryWrapper.Reviews.GetById(model.Id);
            if(review != null)
            {
                shop.IsActive = model.IsActive;
                _repositoryWrapper.Reviews.Update(review);
            }
            var favs = _repositoryWrapper.UsersFavShops.GetById(model.Id);
            if(favs != null)
            {
                favs.IsActive = model.IsActive;
                _repositoryWrapper.UsersFavShops.Update(favs);
            }
            _repositoryWrapper.Save();
            return Ok();
        }
        

        
    }
}
