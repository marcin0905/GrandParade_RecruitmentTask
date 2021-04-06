using CustomerService.Application.Middleware.Error;
using Microsoft.AspNetCore.Builder;

namespace CustomerService.Application.Middleware.Extensions
{
    public static class CustomErrorHandling
    {
        public static void UseCustomErrorHandling(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomErrorHandlingMiddleware>();
        }
    }
}