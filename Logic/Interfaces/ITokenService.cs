using Data.Entities;
using WebAPi.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebAPi.Interfaces
{
    public interface ITokenService
    {
        public  Task<string> GetTokenAsync(User user);
        public Task<List<Claim>> DecryptToken(string token);
     }
}
