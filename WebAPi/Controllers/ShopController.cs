using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllShops()
        {
            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetShopById([FromBody] Guid Id)
        {
            return Ok();
        }
        
        [HttpPost]
        [Authorize(Roles = "Seller, Admin")]
        public async Task<IActionResult> CreateShop([FromBody] Object model)
        {
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Seller, Admin")]
        public async Task<IActionResult> UpdateShop([FromBody] Object model)
        {
            return Ok();
        } 

        [HttpDelete]
        [Authorize(Roles = "Seller, Admin")]
        public async Task<IActionResult> DeleteShop([FromBody] Guid Id)
        {
            return Ok();
        }
    }
}
