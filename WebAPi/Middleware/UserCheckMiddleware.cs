
using Data.Entities;
using Data.IRepositories;
using Logic.Exceptions;
using System.Net;
using Microsoft.Extensions.Logging;
using WebAPi.Interfaces;
using System.Security.Claims;

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
                string useremail = tokenService.DecryptToken(token).FirstOrDefault(e=>e.Type == ClaimTypes.Name).Value;
                var user = repositoryWrapper.Users.GetAll().FirstOrDefault(e=>e.Email == useremail);
                if(user == null) 
                {
                    throw new AuthException("Некорректный токен авторизации","Uncorrect jwttoken", HttpStatusCode.Forbidden);
                }
                if(!user.IsActive)
                {
                    throw new AuthException("Пользователь заблокирован","User not active", HttpStatusCode.Forbidden,user.Email);
                    
                }
     
            
           }
           await _next.Invoke(context);
        }
    }
}
