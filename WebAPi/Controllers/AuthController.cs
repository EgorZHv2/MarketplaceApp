using Data.DTO.Auth;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authService.Login(model);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegistrationDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _authService.Register(model);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Verify([FromBody] LoginDTO model)
        {
            await _authService.VerifyEmail(model.Email, model.Password);
            return Ok();
        }

        [HttpPut]
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