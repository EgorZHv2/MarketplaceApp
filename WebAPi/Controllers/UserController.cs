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
using Data.DTO.User;
using Microsoft.EntityFrameworkCore;

namespace WebAPi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController:BaseController
    {
        private IRepositoryWrapper _repositoryWrapper;
        private IImageService _imageService;
        private IUserService _userService;
      
        public UserController(
            IRepositoryWrapper repositoryWrapper,
            IImageService imageService,
            IUserService userService
            
            
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _imageService = imageService;
            _userService = userService;
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromForm] UpdateUserDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.Update(UserId, model);
            return Ok(result);  
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
            _repositoryWrapper.Users.Update(user.Id,entity);
            _repositoryWrapper.Save();
            return Ok();
        }
    }
}
