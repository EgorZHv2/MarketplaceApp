using AutoMapper;
using Data.IRepositories;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Logic.Exceptions;
using System.Web;
using System.Net;
using System.Security.Claims;
using Logic.Interfaces;
using Data.DTO;
using Data.Entities;
using Data;
using Microsoft.EntityFrameworkCore;

namespace WebAPi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ILogger<UserController> _logger;
        private IRepositoryWrapper _repositoryWrapper;
        private IMapper _mapper;
        private IImageService _imageService;
        private ApplicationDbContext _context;
        public UserController(
            ILogger<UserController> logger,
            IRepositoryWrapper repositoryWrapper,
            IMapper mapper,
            IImageService imageService,
            ApplicationDbContext context
        )
        {
            _logger = logger;
            _repositoryWrapper = repositoryWrapper;
            _imageService = imageService;
            _mapper = mapper;
            _context = context;
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromForm] UpdateUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _repositoryWrapper.Users.GetUserByEmail(User.Identity.Name).Result;
            if (user == null)
            {
                throw new NotFoundException("Пользователь не найден", "User not found");
            }
            if (!string.IsNullOrEmpty(model.FirstName))
            {
                user.FirstName = model.FirstName;
            }
            if (model.Photo != null)
            {
                await _imageService.CreateImage(model.Photo, user.Id);
            }
            _repositoryWrapper.Users.Update(user, user.Id);
            _repositoryWrapper.Save();
            return Ok();
        }
        [Authorize] 
        [HttpPut]

        public async Task<IActionResult> AddFavoriteShop([FromBody] FavoriteShopsModel model)
        {
           var userid = new Guid(User.Claims.ToArray()[2].Value);
           var user = _repositoryWrapper.Users.GetById(userid).Result;
           foreach(var Id in model.ShopIds)
           {
              user.FavoriteShops.Add(_repositoryWrapper.Shops.GetById(Id).Result);
           }
           _repositoryWrapper.Users.Update(user,userid);
           _repositoryWrapper.Save();
           return Ok();
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> ShowUserFavoriteShops()
        {
            List<ShopDTO> result = new List<ShopDTO>();
            var userid = new Guid(User.Claims.ToArray()[2].Value);
            var shops = _repositoryWrapper.Users.GetFavoriteShopsByUserId(userid).Result;
            try
            {
                result = _mapper.ProjectTo<ShopDTO>(shops).ToList();
            }
            catch
            {
                throw new MappingException(this.GetType().ToString());
            }


            return Ok(result);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> ChangeUserPassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = _repositoryWrapper.Users.GetUserByEmail(User.Identity.Name).Result;
            if (user == null)
            {
                throw new NotFoundException("Пользователь не найден", "User not found");
            }
            if (model.OldPassword != user.Password)
            {
                throw new AuthException(
                    "Старый пароль неверный",
                    "Wrong old password",
                    HttpStatusCode.Unauthorized,
                    user.Email
                );
            }

            user.Password = model.Password;
            _repositoryWrapper.Users.Update(user, user.Id);
            _repositoryWrapper.Save();
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeUserActivity([FromBody] EntityActivityModel model)
        {
            var user = _repositoryWrapper.Users.GetUserByEmail(User.Identity.Name).Result;
            var entity = _repositoryWrapper.Users.GetById(model.Id).Result;
            if (entity == null)
            {
                throw new NotFoundException("Пользователь не найден", "User not found");
            }
            entity.IsActive = model.IsActive;
            _repositoryWrapper.Users.Update(entity, user.Id);
            _repositoryWrapper.Save();
            return Ok();
        }
    }
}
