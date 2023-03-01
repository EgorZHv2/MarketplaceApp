using Data.DTO.Auth;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        /// <summary>
        /// Вход в аккаунт
        /// </summary>
        /// <param name="model">Модель логина</param>
        /// <returns>JWT токен</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authService.Login(model);
            return Ok(result);
        }
        /// <summary>
        /// Регистрация
        /// </summary>
        /// <param name="model">Модель регистрации</param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _authService.Register(model);
            return Ok();
        }

        /// <summary>
        /// Подтверждение аккаунта через код пришедший на почту
        /// </summary>
        /// <param name="model">Модель подтверждения кода</param>
        /// <returns></returns>
        [HttpPut("verify-account")]
        public async Task<IActionResult> Verify([FromBody] LoginDTO model)
        {
            await _authService.VerifyEmail(model.Email, model.Password);
            return Ok();
        }
        /// <summary>
        /// Смена пароля
        /// </summary>
        /// <param name="model">Модель смены пароля</param>
        /// <returns></returns>
        [HttpPatch("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _authService.ChangePassword(UserId, model);
            return Ok();
        }
    }
}