using WebAPi.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebAPi.Models;
using Logic.Exceptions;
using System.Net;

namespace WebAPi.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private RequestDelegate _next;
        private ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch(ApiException ex)
            {
                string logmessage = $"Date: {ex.DateTime} | Exception: {ex.GetType().Name} | Code: {ex.StatusCode} | Message: {ex.LogMessage} | ";
                if(ex is AuthException authException)
                {
                    logmessage += "User login: " + (string.IsNullOrEmpty(authException.UserLogin)?"none":authException.UserLogin);
                }
                if(ex is MappingException mappingException)
                {
                    logmessage += $"ExceptionClassName: {mappingException.ExceptionClass}";
                }
                _logger.LogError(logmessage, ex);
                ResponseError(context, ex.UserMessage, ex.StatusCode);
            }
           
        }
        public async Task ResponseError(HttpContext context,string message, HttpStatusCode code)
        {
            context.Response.StatusCode = (int)code;
                context.Response.WriteAsJsonAsync(new ErrorModel()
                {
                    Message = message,
                    StatusCode = context.Response.StatusCode
                }.ToJson());
        }
    }
}
