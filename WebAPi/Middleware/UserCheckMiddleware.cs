using Data.IRepositories;
using Logic.Exceptions;
using Logic.Interfaces;
using System.Net;
using System.Security.Claims;
using WebAPi.Interfaces;
using Data;
using System.Globalization;

namespace WebAPi.Middleware
{
    public class UserCheckMiddleware
    {
        private RequestDelegate _next;
        private ILogger<UserCheckMiddleware> _logger;

        public UserCheckMiddleware(RequestDelegate next, ILogger<UserCheckMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, IUserRepository userRepository, ITokenService tokenService,IUserData userData)
        {
            string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (!string.IsNullOrEmpty(token))
            {
                string useremail = tokenService.DecryptToken(token).FirstOrDefault(e => e.Type == ClaimTypes.Name).Value;
                var user = await userRepository.GetUserByEmail(useremail);
                if (user == null)
                {
                    throw new IncorrectJWTException();
                }
                if (!user.IsActive)
                {
                    throw new BlockedUserException();
                }
                
                userData.Name = user.FirstName;
                userData.Id = user.Id;
                userData.Role = user.Role;
               
            }
            await _next.Invoke(context);
        }
    }
}