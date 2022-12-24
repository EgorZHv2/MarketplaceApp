
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetReviewById([FromBody] Guid Id)
        {
            
            return Ok();
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetReviewsByShopId([FromBody] Guid Id)
        {
            
            return Ok();
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllReviews()
        {
             return Ok();
        }
        [HttpPost]
        [Authorize(Roles = "Buyer,Admin")]
        public async Task<IActionResult> CreateReview([FromBody] Object model)
        {
            return Ok();
        }
        [HttpPut]
        [Authorize(Roles = "Buyer,Admin")]
        public async Task<IActionResult> UpdateReview([FromBody] Object model)
        {
            return Ok();
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteReview([FromBody] Guid Id)
        {
            return Ok();
        }
    }
}
