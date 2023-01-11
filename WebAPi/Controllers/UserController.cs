using AutoMapper;
using Data.IRepositories;
using Logic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPi.Exceptions;
using System.Web;
using System.Net;
using System.Security.Claims;

namespace WebAPi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ILogger<UserController> _logger;
        private IRepositoryWrapper _repositoryWrapper;
        private IMapper _mapper;

        public UserController(
            ILogger<UserController> logger,
            IRepositoryWrapper repositoryWrapper,
            IMapper mapper
        )
        {
            _logger = logger;
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUserModel model)
        {
            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
         
            var user = _repositoryWrapper.Users.GetUserByEmail(User.Identity.Name);
            if(user == null) 
            {
                throw new NotFoundException("Пользователь не найден");
            }
            if(model.OldPassword != user.Password)
            {
                throw new AuthException("Старый пароль неверный", HttpStatusCode.Unauthorized,user.Email);
            }
            
            user.Password = model.Password;
            user.FirstName = model.FirstName;

            _repositoryWrapper.Users.Update(user,user.Id);
            _repositoryWrapper.Save();
            return Ok();

        }
    }
}
