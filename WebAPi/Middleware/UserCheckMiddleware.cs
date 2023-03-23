using Data.IRepositories;
using Logic.Exceptions;
using Logic.Interfaces;
using System.Net;
using System.Security.Claims;
using WebAPi.Interfaces;
using Data;
using Data.Localizations;
using System.Globalization;
using Microsoft.Extensions.Localization;


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
            string language = context.Request.Headers["Accept-Language"].FirstOrDefault()?.Trim().ToLower();
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
           
            if (language == "ru")
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("ru");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru");
            }
            else if(language == "en")
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            }
            
                        
            await _next.Invoke(context);
        }
    }
}