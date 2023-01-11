using Data.IRepositories;
using Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPi.Exceptions;

namespace Logic.Services
{
    public class AuthService:IAuthService
    {
        private IRepositoryWrapper _repositoryWrapper;
        private IHashService _hashService;
        public AuthService(IRepositoryWrapper repositoryWrapper,IHashService hashService) 
        {
            _repositoryWrapper = repositoryWrapper;
            _hashService = hashService;
        }
        public async Task VerifyEmail(string email,string code)
        {
            var user = _repositoryWrapper.Users.GetUserByEmail(email);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            if (!_hashService.ComparePasswordWithHash(code, user.EmailConfirmationCode).Result)
            {
                throw new AuthException("Wrong code", System.Net.HttpStatusCode.Unauthorized, user.Email);
            }
            user.IsEmailConfirmed = true;
            _repositoryWrapper.Users.Update(user, user.Id);
            _repositoryWrapper.Save();
            
        }
    }
}
