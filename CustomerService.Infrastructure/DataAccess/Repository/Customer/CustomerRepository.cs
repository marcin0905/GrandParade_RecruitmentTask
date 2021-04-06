using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Common.Infrastructure.DataAccess.Contracts;
using CustomerService.Domain.Customer.Repository;

namespace CustomerService.Infrastructure.DataAccess.Repository.Customer
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Register(Domain.Customer.Customer customer)
        {
            await _unitOfWork.Save(customer);
        }

        public Task<ImmutableList<Domain.Customer.Customer>> GetAll()
        {
            return Task.FromResult(_unitOfWork.Session.Query<Domain.Customer.Customer>().ToImmutableList());
        }

        public Task<Domain.Customer.Customer> GetById(Guid customerId)
        {
            return Task.FromResult(
                _unitOfWork.Session.Query<Domain.Customer.Customer>().SingleOrDefault(c => c.Id == customerId));
        }
    }
}