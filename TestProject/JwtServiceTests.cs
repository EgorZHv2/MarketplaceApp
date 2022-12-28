using Data;
using Data.Entities;
using Data.IRepositories;
using Data.Repositories;
using Logic.Configuration;
using Logic.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TestProject
{
    public class JwtServiceTests
    {
        private TokenService _tokenService = new TokenService();
        [Fact]
        public void JwtAuthOptionsTest()
        {
           SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TQvgjeABMPOwCycOqah5EQu5yyVjpmVG"));

            Assert.Equal(key.Key, JwtAuthOptions.GetKey().Key);
         
        }
        [Fact]
        public void TokenServiceTest()
        {
            User user = new User
            {
                Email = "admin@mail.ru",
                Password = "12345678",
                Role = Data.Enums.Role.Admin

            };
            var result =  _tokenService.GetTokenAsync(user);

            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.NotEmpty(result.Result);
            Assert.True(result.IsCompleted);
            Assert.True(result.IsCompletedSuccessfully);
            Assert.False(result.IsCanceled);
            Assert.False(result.IsFaulted);


        }
    }
}