using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Features.Indexed;
using Common.Configuration.Options;
using CustomerService.Application.Contants;
using CustomerService.Application.Contracts.Services.Customer;
using CustomerService.Application.Dto.Customer;
using CustomerService.Application.Enum;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CustomerService.Application.Services.Customer.Proxy
{
    public class CustomerQueryServiceProxy : ICustomerQueryService
    {
        private readonly ICustomerQueryService _customerQueryService;
        private readonly IDistributedCache _distributedCache;
        private readonly IOptions<CachingTimeOptions> _optionsCachingTime;

        public CustomerQueryServiceProxy(
            IIndex<ServiceType, ICustomerQueryService> customerQueryServiceTypes,
            IDistributedCache distributedCache,
            IOptions<CachingTimeOptions> optionsCachingTime)
        {
            _customerQueryService = customerQueryServiceTypes[ServiceType.NoCache];
            _distributedCache = distributedCache;
            _optionsCachingTime = optionsCachingTime;
        }

        public async Task<BaseCustomerDto[]> GetCustomers()
        {
            const string cacheKey = CacheKeyConstants.CustomersListCacheKey;
            var cachedCustomersBytes = await _distributedCache.GetAsync(cacheKey);

            if (null == cachedCustomersBytes || !cachedCustomersBytes.Any())
            {
                var customers = await _customerQueryService.GetCustomers();
                _ = _distributedCache.SetAsync(cacheKey, 
                    Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(customers)),
                    new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(_optionsCachingTime.Value.CustomerList)
                });

                return customers;
            }

            return JsonConvert.DeserializeObject<BaseCustomerDto[]>(Encoding.UTF8.GetString(cachedCustomersBytes));
        }

        public Task<BaseCustomerDto> GetCustomer(Guid customerId)
        {
            return _customerQueryService.GetCustomer(customerId);
        }
    }
}