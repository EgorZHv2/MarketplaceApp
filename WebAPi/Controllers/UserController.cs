using Data.DTO;
using Data.DTO.User;
using Data.Entities;
using Data.IRepositories;
using Data.Models;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : BaseGenericController<User, UserDTO, CreateUserDTO, UpdateUserDTO, IUserRepository, IUserService>
    {
        public UserController(IUserService userService) : base(userService)
        {
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromForm] UpdateUserDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.Update(UserId, model);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> CreateAdmin([FromBody] CreateAdminDTO model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.CreateAdmin(UserId, model);
            return Ok(result);
        }
      
    }
}