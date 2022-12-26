using AutoMapper;
using Data.Entities;
using Data.IRepositories;
using Logic.Interfaces;
using Logic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IRepositoryWrapper _repositoryWrapper;
        private IMapper _mapper;
        private IPasswordGeneratorService _passwordGeneratorService;
        private IEmailService _emailService;
        private ILogger<AuthController> _logger;

        public AuthController(
            IRepositoryWrapper repositoryWrapper,
            IMapper mapper,
            IPasswordGeneratorService passwordGeneratorService,
            IEmailService emailService,
            ILogger<AuthController> logger
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _passwordGeneratorService = passwordGeneratorService;
            _emailService = emailService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Object model)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model.Role == Data.Enums.Role.Buyer || model.Role == Data.Enums.Role.Seller)
            {
                User user = new User();
                try
                {
                    user = _mapper.Map<User>(model);
                }
                catch
                {
                    _logger.LogError("Error while mapping");
                }
                user.Password = _passwordGeneratorService.GeneratePassword();
                _emailService.SendEmail(
                    user.Email,
                    "MarketPlaceApp",
                    $"Your password = {user.Password}"
                );
                _repositoryWrapper.Users.Create(user);
                _repositoryWrapper.Save();
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
