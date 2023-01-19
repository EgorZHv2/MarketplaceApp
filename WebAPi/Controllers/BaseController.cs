using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        internal Guid UserId => !User.Identity.IsAuthenticated ? Guid.Empty : new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
    }
}
