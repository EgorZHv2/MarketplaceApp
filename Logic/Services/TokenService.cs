using Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPi.Interfaces;

namespace WebAPi.Services
{
    public class TokenService : ITokenService
    {
        private IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtAuthKey").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );
            return tokenHandler.WriteToken(token);
        }

        public List<Claim> DecryptToken(string token)
        {
            List<Claim> result = new List<Claim>();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtAuthKey").Value));
            var jwt = tokenHandler.ReadJwtToken(token);
            result = jwt.Claims.ToList();
            return result;
        }
    }
}