using CustomerService.Domain.Customer;

namespace CustomerService.Application.Tests.Fakes.Customer
{
    public class FakeCustomer : Domain.Customer.Customer
    {
        public FakeCustomer(string firstName, string lastName, Address address) : base(firstName, lastName, address)
        {
        }
    }
}