using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;

namespace CustomerService.Application.Filters
{
    public class InvalidateCacheActionFilter : IAsyncActionFilter
    {
        public InvalidateCacheActionFilter(string cacheKey)
        {
            CacheKey = cacheKey;
        }

        public string CacheKey { get; }

        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var distributedCache = context.HttpContext.RequestServices.GetService(typeof(IDistributedCache)) as IDistributedCache;
            distributedCache?.RemoveAsync(CacheKey);

            return next();
        }
    }
}