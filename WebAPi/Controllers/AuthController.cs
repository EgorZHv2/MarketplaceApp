using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Object model)
        {
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Object model)
        {
            return Ok();
        }
    }
}
