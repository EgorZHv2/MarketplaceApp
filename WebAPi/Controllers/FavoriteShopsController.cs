using AutoMapper;
using Data.DTO;
using Data.Entities;
using Data.IRepositories;
using Data.Models;
using Logic.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography.Xml;

namespace WebAPi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FavoriteShopsController : ControllerBase
    {
        private ILogger<FavoriteShopsController> _logger;
        private IRepositoryWrapper _repositoryWrapper;
        private IMapper _mapper;

        public FavoriteShopsController(
            ILogger<FavoriteShopsController> logger,
            IRepositoryWrapper repositoryWrapper,
            IMapper mapper
        )
        {
            _logger = logger;
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddFavoriteShop([FromBody] FavoriteShopsModel model)
        {
            var userid = new Guid(User.Claims.ToArray()[2].Value);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            UsersFavShops entity = new UsersFavShops();
            entity.UserId = model.UserId;
            entity.ShopId = model.ShopId;
            _repositoryWrapper.UsersFavShops.Create(entity,userid);
            _repositoryWrapper.Save();
            return Ok();
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetFavsByUserId([FromQuery] Guid Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            List<UsersFavShopsDTO> result = new List<UsersFavShopsDTO>();
            var list = _repositoryWrapper.UsersFavShops.GetAll().Where(e=>e.UserId == Id).AsQueryable();
            try
            {
                result = _mapper.ProjectTo<UsersFavShopsDTO>(list).ToList();
            }
            catch
            {
                throw new MappingException(this.GetType().ToString());
            }
            
            
            return Ok(result);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeFavShopActivity([FromBody] EntityActivityModel model)
        {
            var user = _repositoryWrapper.Users.GetUserByEmail(User.Identity.Name).Result;
            var entity = _repositoryWrapper.UsersFavShops.GetById(model.Id).Result;
            if(entity == null)
            {
                throw new NotFoundException("Избранное не найдено", "Users favorite shops entity not found");
            }
            entity.IsActive = model.IsActive;
            _repositoryWrapper.UsersFavShops.Update(entity,user.Id);
            _repositoryWrapper.Save();
            return Ok();
        }
    }
}
