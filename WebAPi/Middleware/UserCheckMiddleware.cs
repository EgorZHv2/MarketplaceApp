
using Data.Entities;
using Data.IRepositories;
using WebAPi.Exceptions;
using System.Net;
using Microsoft.Extensions.Logging;
using WebAPi.Interfaces;

namespace WebAPi.Middleware
{
    public class UserCheckMiddleware
    {
        private RequestDelegate _next;
        private ILogger<UserCheckMiddleware> _logger;
     
        public UserCheckMiddleware(RequestDelegate next,ILogger<UserCheckMiddleware> logger)
        {
            _next = next;
            _logger = logger;

        
        }
        public async Task InvokeAsync(HttpContext context,IRepositoryWrapper repositoryWrapper, ITokenService tokenService)
        {

            string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (!string.IsNullOrEmpty(token))
            {
                var user = repositoryWrapper.Users.GetAll().FirstOrDefault(e=>e.Email == tokenService.DecryptToken(token).Result[0].Value);
                _logger.LogError(user.IsActive.ToString());
                if(user == null) 
                {
                    throw new AuthException("Некорректный токен", HttpStatusCode.Forbidden, DateTime.UtcNow);
                }
                if(!user.IsActive)
                {
                    throw new AuthException("Пользователь заблокирован", HttpStatusCode.Forbidden, DateTime.UtcNow,user.Email);
                    
                }
     
            
           }
           await _next.Invoke(context);
        }
    }
}
