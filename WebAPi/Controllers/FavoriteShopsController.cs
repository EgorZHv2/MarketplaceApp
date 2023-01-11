using AutoMapper;
using Data.Entities;
using Data.IRepositories;
using Logic.Models;
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
            var userid = new Guid(User.Claims.FirstOrDefault(e => e.ValueType == ClaimTypes.NameIdentifier).Value);
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
            var result = _repositoryWrapper.UsersFavShops.GetAll().Where(e=>e.UserId == Id).ToList();
            return Ok(result);
        }
    }
}
