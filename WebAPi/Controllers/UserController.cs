using Data.DTO;
using Data.DTO.User;
using Data.Entities;
using Data.IRepositories;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseGenericController<UserEntity, UserDTO, CreateUserDTO, UpdateUserDTO, IUserRepository, IUserService>
    {
        public UserController(IUserService userService) : base(userService)
        {
        }
        /// <summary>
        /// Изменить данные пользователя (не регистрационные)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromForm] UpdateUserDTO model)
        {
            await _service.Update(model);
            return Ok();
        }
        /// <summary>
        /// Создать аккаунт администратора
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("create-admin")]
        [Authorize(Roles = nameof(Data.Enums.Role.Admin))]
        public async Task<IActionResult> CreateAdmin([FromBody] CreateAdminDTO model)
        {
            var result = await _service.CreateAdmin(model);
            return Ok(result);
        }
    }
}