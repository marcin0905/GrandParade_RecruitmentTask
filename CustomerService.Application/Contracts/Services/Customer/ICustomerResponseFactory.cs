using System.Collections.Generic;
using CustomerService.Application.Dto.Customer;

namespace CustomerService.Application.Contracts.Services.Customer
{
    public interface ICustomerResponseFactory 
    {
        BaseCustomerDto[] Create(IEnumerable<Domain.Customer.Customer> customers);

        BaseCustomerDto Create(Domain.Customer.Customer customer);
    }
}