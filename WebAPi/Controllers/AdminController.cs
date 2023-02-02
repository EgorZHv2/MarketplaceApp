using AutoMapper;
using Data.Entities;
using Data.IRepositories;
using Data.Models;
using Logic.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private ILogger<AdminController> _logger;
        private IRepositoryWrapper _repositoryWrapper;
        private IMapper _mapper;

        public AdminController(
            ILogger<AdminController> logger,
            IRepositoryWrapper repositoryWrapper,
            IMapper mapper
        )
        {
            _logger = logger;
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAdmin(CreateAdminModel model)
        {
            var userid = new Guid(User.Claims.ToArray()[2].Value);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User user = new User();
            try
            {
                user = _mapper.Map<User>(model);
            }
            catch
            {
                throw new MappingException(this.GetType().ToString());
            }
            user.Role = Data.Enums.Role.Admin;
            _repositoryWrapper.Users.Create(userid, user);
            _repositoryWrapper.Save();
            return Ok(user.Id);
        }
    }
}