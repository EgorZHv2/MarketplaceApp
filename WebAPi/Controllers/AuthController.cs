using AutoMapper;
using Data.Entities;
using Data.IRepositories;
using WebAPi.Configuration;
using WebAPi.Exceptions;
using WebAPi.Interfaces;
using WebAPi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Logic.Exceptions;

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
        private ITokenService _tokenService;

        public AuthController(
            IRepositoryWrapper repositoryWrapper,
            IMapper mapper,
            IPasswordGeneratorService passwordGeneratorService,
            IEmailService emailService,
            ILogger<AuthController> logger,
            ITokenService tokenService
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _passwordGeneratorService = passwordGeneratorService;
            _emailService = emailService;
            _logger = logger;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = _repositoryWrapper.Users
                .GetAll()
                .FirstOrDefault(e => e.Email == model.Email);

            if (user == null)
            {
              throw new NotFoundException("Пользователь с данным email не найден");
            }
            if (model.Password != user.Password)
            {
                throw new AuthException("Неверный пароль", System.Net.HttpStatusCode.Unauthorized,DateTime.UtcNow,user.Email);
            }
            var results = _tokenService.GetTokenAsync(user);
            
            return Ok(results.Result);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegistrationModel model)
        {
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
                    
                    throw new MappingException("Ошибка при маппинге",this.GetType().ToString());
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
    }
}
