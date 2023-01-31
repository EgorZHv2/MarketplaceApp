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
        public string GetToken(User user);
        public List<Claim> DecryptToken(string token);
     }
}
