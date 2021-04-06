using System;
using System.Threading.Tasks;
using CustomerService.Application.Dto.Customer;

namespace CustomerService.Application.Contracts.Services.Customer
{
    public interface ICustomerQueryService
    {
        Task<BaseCustomerDto[]> GetCustomers();

        Task<BaseCustomerDto> GetCustomer(Guid customerId);
    }
}