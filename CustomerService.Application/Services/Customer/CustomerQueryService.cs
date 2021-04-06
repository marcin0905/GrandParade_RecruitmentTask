using System;
using System.Threading.Tasks;
using CustomerService.Application.Contracts.Services.Customer;
using CustomerService.Application.Dto.Customer;
using CustomerService.Domain.Customer.Repository;

namespace CustomerService.Application.Services.Customer
{
    public class CustomerQueryService : ICustomerQueryService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerResponseFactory _customerResponseFactory;

        public CustomerQueryService(
            ICustomerRepository customerRepository,
            ICustomerResponseFactory customerResponseFactory)
        {
            _customerRepository = customerRepository;
            _customerResponseFactory = customerResponseFactory;
        }

        public async Task<BaseCustomerDto[]> GetCustomers()
        {
            var customers = await _customerRepository.GetAll();

            return _customerResponseFactory.Create(customers);
        }

        public async Task<BaseCustomerDto> GetCustomer(Guid customerId)
        {
            var customer = await _customerRepository.GetById(customerId);

            return null == customer
                ? default
                : _customerResponseFactory.Create(customer);
        }
    }
}