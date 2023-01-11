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
using Logic.Interfaces;
using Data.Repositories;

namespace WebAPi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IRepositoryWrapper _repositoryWrapper;
        private IMapper _mapper;
        private IRandomStringGeneratorService _stringGeneratorService;
        private IEmailService _emailService;
        private ILogger<AuthController> _logger;
        private ITokenService _tokenService;
        private IHashService _hashService;
        private IAuthService _authService;

        public AuthController(
            IRepositoryWrapper repositoryWrapper,
            IMapper mapper,
            IRandomStringGeneratorService stringGeneratorService,
            IEmailService emailService,
            ILogger<AuthController> logger,
            ITokenService tokenService,
            IHashService hashService,
            IAuthService authService
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _stringGeneratorService = stringGeneratorService;
            _emailService = emailService;
            _logger = logger;
            _tokenService = tokenService;
            _hashService = hashService;
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = _repositoryWrapper.Users.GetUserByEmail(model.Email);

            if (user == null)
            {
                throw new NotFoundException("Пользователь с данным email не найден");
            }
            if (!user.IsEmailConfirmed)
            {
                throw new AuthException(
                   "Пароль не подтверждён",
                   System.Net.HttpStatusCode.Forbidden,
                   model.Email);
            }
            if (!_hashService.ComparePasswordWithHash(model.Password, user.Password).Result)
            {
                throw new AuthException(
                    "Неверный пароль",
                    System.Net.HttpStatusCode.Unauthorized,
                    model.Email
                );
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
            var dbuser = _repositoryWrapper.Users.GetUserByEmail(model.Email);
            if(dbuser != null)
            {
                throw new AuthException("This email already in user", System.Net.HttpStatusCode.Unauthorized);
            }
            User user = new User();
            try
            {
                user = _mapper.Map<User>(model);
            }
            catch
            {
                throw new MappingException("Ошибка при маппинге", this.GetType().ToString());
            }
            string code = _stringGeneratorService.Generate(6); 
            user.Password = _hashService.HashPassword(model.Password).Result;
            user.EmailConfirmationCode = _hashService.HashPassword(code).Result;
            _emailService.SendEmail(
                user.Email,
                "MarketPlaceApp",
                $"Your verification code = {code}"
            );
            _repositoryWrapper.Users.Create(user,Guid.Empty);
            _repositoryWrapper.Save();
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Verify([FromBody] LoginModel model)
        {
            _authService.VerifyEmail(model.Email,model.Password);
            return Ok();
        }
    }
}
