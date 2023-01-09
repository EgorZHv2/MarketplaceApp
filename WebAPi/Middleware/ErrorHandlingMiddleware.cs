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
                _logger.LogError($"Error. Code: {(int)e.StatusCode} Message: {e.Message}");
                context.Response.StatusCode = (int)e.StatusCode;
                context.Response.WriteAsJsonAsync(new ErrorModel()
                {
                    Message = e.Message,
                    StatusCode = context.Response.StatusCode
                }.ToJson());
            }
            catch(AuthException e)
            {
                _logger.LogError($"Error. Code: {(int)e.StatusCode} Date: {e.DateTime} User: {e.UserLogin}");
                context.Response.StatusCode = (int)e.StatusCode;
                context.Response.WriteAsJsonAsync(new ErrorModel()
                {
                    Message = e.Message,
                    StatusCode = context.Response.StatusCode
                }.ToJson());
            }
        }
    }
}
