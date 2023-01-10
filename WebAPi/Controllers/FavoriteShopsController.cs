using AutoMapper;
using Data.Entities;
using Data.IRepositories;
using Logic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            UsersFavShops entity = new UsersFavShops();
            entity.UserId = model.UserId;
            entity.ShopId = model.ShopId;
            entity.CreateDateTime = DateTime.UtcNow;
            entity.UpdateDateTime = DateTime.UtcNow;
            entity.CreatorId = model.UserId;
            entity.UpdatorId = model.UserId;
            _repositoryWrapper.UsersFavShops.Create(entity);
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
            var result = _repositoryWrapper.UsersFavShops.GetAll().Where(e=>e.UserId == Id).ToList();
            return Ok(result);
        }
    }
}
