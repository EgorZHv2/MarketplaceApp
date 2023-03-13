using Data.DTO;
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
            catch (ApiException ex)
            {
                string logmessage = $"Date: {DateTime.UtcNow} | Exception: {ex.GetType().Name} | Code: {ex.StatusCode} | Message: {ex.LogMessage}";
                _logger.LogError(logmessage, ex);
                await ResponseError(context, ex.UserMessage, ex.StatusCode);
            }
            catch(Exception ex)
            {
                string logmessage = $"Date: {DateTime.UtcNow} | Exception: {ex.GetType().Name} | Message: {ex.Message}";
                _logger.LogError(logmessage, ex);
                await ResponseError(context, ex.Message);
            }
        }

        public async Task ResponseError(HttpContext context, string message, HttpStatusCode code = HttpStatusCode.InternalServerError)
        {
            context.Response.StatusCode = (int)code;
            await context.Response.WriteAsJsonAsync(new ErrorResponseDTO()
            {
                Message = message,
                StatusCode = context.Response.StatusCode
            }.ToJson());
        }
    }
}