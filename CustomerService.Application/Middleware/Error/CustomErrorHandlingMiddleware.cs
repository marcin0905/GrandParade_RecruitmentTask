using System;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using CustomerService.Application.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CustomerService.Application.Middleware.Error
{
    public class CustomErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomErrorHandlingMiddleware> _logger;

        public CustomErrorHandlingMiddleware(
            RequestDelegate next, 
            ILogger<CustomErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleException(httpContext, ex);
                _logger.LogError(ex, ex.Message);
            }
        }

        private static Task HandleException(HttpContext context, Exception exception)
        {
            return WriteToResponse(context,
                string.IsNullOrEmpty(exception.Message) ? "Internal Server Error" : exception.Message);
        }

        private static Task WriteToResponse(HttpContext context, string message = null)
        {
            var statusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorDto
            {
                StatusCode = statusCode,
                Message = message
            }));
        }
    }
}