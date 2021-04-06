using MediatR;

namespace CustomerService.Application.Mediator.Command.Customer
{
    public class RegisterMrGreenCustomerCommand : BaseRegisterCustomer, IRequest
    {
        public string PersonalNumber { get; }

        public RegisterMrGreenCustomerCommand(
            string firstName,
            string lastName,
            string street,
            int number,
            string zipCode,
            string city,
            string personalNumber) : base(firstName, lastName, street, number, zipCode, city)
        {
            PersonalNumber = personalNumber;
        }
    }
}