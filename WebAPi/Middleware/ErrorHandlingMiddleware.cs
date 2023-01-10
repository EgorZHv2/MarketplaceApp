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
            catch(NotFoundException e)
            {
                _logger.LogError($"NotFoundError. Code: {(int)e.StatusCode} Message: {e.Message}");
                ResponseError(context, e.Message, e.StatusCode);
            }
            catch(AuthException e)
            {
                _logger.LogError($"AuthError. Code: {(int)e.StatusCode} Date: {e.DateTime} User: {e.UserLogin}");
                ResponseError(context, e.Message, e.StatusCode);
            }
            catch(MappingException e)
            {
                _logger.LogError($"Error while mapping. Code: {(int)e.StatusCode} Message: {e.Message} ExceptionClassName: {e.ExceptionClass} ");
                ResponseError(context, e.Message, e.StatusCode);
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
