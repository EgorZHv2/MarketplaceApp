using AutoMapper;
using Data.Entities;
using Data.IRepositories;
using Logic.Configuration;
using Logic.Interfaces;
using Logic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                 return BadRequest(ModelState);
            }
            var user = _repositoryWrapper.Users.GetAll().FirstOrDefault(e => e.Email == model.Email);
            if(user == null)
            {
                return BadRequest(ModelState);
            }
            var claims = new Claim[] {new Claim(ClaimTypes.Name, user.Email),new Claim(ClaimTypes.Role,user.Role.ToString())};
            
            var token = new JwtSecurityToken
                (
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: JwtAuthOptions.GetCredentials());

            var results = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            };
            return Ok(results);
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
