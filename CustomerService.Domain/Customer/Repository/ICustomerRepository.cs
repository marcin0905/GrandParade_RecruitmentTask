using System;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace CustomerService.Domain.Customer.Repository
{
    public interface ICustomerRepository
    {
        Task Register(Customer customer);

        Task<ImmutableList<Customer>> GetAll();
        
        Task<Customer> GetById(Guid customerId);
    }
}