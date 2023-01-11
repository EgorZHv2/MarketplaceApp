using Data.Entities;
using WebAPi.Configuration;
using WebAPi.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebAPi.Services
{
    public class TokenService:ITokenService
    {
        public async Task<string> GetTokenAsync(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = JwtAuthOptions.GetKey();
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: JwtAuthOptions.GetCredentials()
            );
           

           
            return tokenHandler.WriteToken(token);
        }
        public async Task<List<Claim>> DecryptToken(string token)
        {
            List<Claim> result = new List<Claim>();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = JwtAuthOptions.GetKey();
            var jwt = tokenHandler.ReadJwtToken(token);
            result = jwt.Claims.ToList();
            return result;
        }
    }
}
