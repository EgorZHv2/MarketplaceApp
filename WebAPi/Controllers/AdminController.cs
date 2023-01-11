using AutoMapper;
using Data.Entities;
using Data.IRepositories;
using Logic.Exceptions;
using Logic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]/[action]")]
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
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> CreateAdmin(CreateAdminModel model)
        {
            var dbuser = _repositoryWrapper.Users.GetAll().FirstOrDefault(e => e.Email == User.Identity.Name);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User user = new User();
            try
            {
                user = _mapper.Map<User>(model);
            }
            catch
            {
                 throw new MappingException("Ошибка при маппинге",this.GetType().ToString());
            }
            user.Id = Guid.NewGuid();
            user.Role = Data.Enums.Role.Admin;
            user.CreateDateTime= DateTime.UtcNow;
            user.UpdateDateTime = DateTime.UtcNow;
            user.CreatorId = dbuser.Id;
            user.UpdatorId = dbuser.Id;
            _repositoryWrapper.Users.Create(user);
            _repositoryWrapper.Save();
            return Ok(user.Id);
        }
        

        
    }
}
