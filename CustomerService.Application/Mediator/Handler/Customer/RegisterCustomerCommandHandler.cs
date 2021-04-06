using System.Threading;
using System.Threading.Tasks;
using CustomerService.Application.Mediator.Command.Customer;
using CustomerService.Domain.Customer;
using CustomerService.Domain.Customer.Repository;
using MediatR;

namespace CustomerService.Application.Mediator.Handler.Customer
{
    public class RegisterCustomerCommandHandler : 
        IRequestHandler<RegisterMrGreenCustomerCommand>,
        IRequestHandler<RegisterRedBetCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;

        public RegisterCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        
        public async Task<Unit> Handle(RegisterMrGreenCustomerCommand request, CancellationToken cancellationToken)
        {
            // Any other logic for MrGreen Customer
            var address = Address.Create(request.Street, request.Number, request.ZipCode, request.City);
            var customer = new MrGreenCustomer(
                request.FirstName,
                request.LastName,
                (PersonalNumber)request.PersonalNumber,
                address);

            await _customerRepository.Register(customer);

            return Unit.Value;
        }

        public async Task<Unit> Handle(RegisterRedBetCustomerCommand request, CancellationToken cancellationToken)
        {
            // Any other logic for RedBet Customer
            var address = Address.Create(request.Street, request.Number, request.ZipCode, request.City);
            var customer = new RedBetCustomer(
                request.FirstName,
                request.LastName,
                request.FavoriteFootballClub,
                address);

            await _customerRepository.Register(customer);

            return Unit.Value;
        }
    }
}