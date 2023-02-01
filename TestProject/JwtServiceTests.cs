using Data;
using Data.Entities;
using Data.IRepositories;
using Data.Repositories;
using WebAPi.Configuration;
using WebAPi.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TestProject
{
    public class JwtServiceTests
    {
        private TokenService _tokenService = new TokenService();
       
        public void TokenServiceTest()
        {
            User user = new User
            {
                Email = "admin@mail.ru",
                Password = "12345678",
                Role = Data.Enums.Role.Admin

            };
            var result =  _tokenService.GetToken(user);

            Assert.NotNull(result);

        }
    }
}