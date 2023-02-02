using Data.Entities;
using System.Security.Claims;

namespace WebAPi.Interfaces
{
    public interface ITokenService
    {
        public string GetToken(User user);

        public List<Claim> DecryptToken(string token);
    }
}