﻿using Data.Entities;
using Data.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPi.Interfaces;

namespace WebAPi.Services
{
    public class TokenService : ITokenService
    {
        private ApplicationOptions _options;

        public TokenService(IOptions<ApplicationOptions> options)
        {
            _options = options.Value;
        }

        public string GetToken(UserEntity user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.JwtAuthKey));
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
            var result = new List<Claim>();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.JwtAuthKey));
            var jwt = tokenHandler.ReadJwtToken(token);
            result = jwt.Claims.ToList();
            return result;
        }
    }
}