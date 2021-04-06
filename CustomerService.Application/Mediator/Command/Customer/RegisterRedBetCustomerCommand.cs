using MediatR;

namespace CustomerService.Application.Mediator.Command.Customer
{
    public class RegisterRedBetCustomerCommand : BaseRegisterCustomer, IRequest
    {
        public string FavoriteFootballClub { get; }

        public RegisterRedBetCustomerCommand(
            string firstName,
            string lastName,
            string street,
            int number,
            string zipCode,
            string city,
            string favoriteFootballClub) : base(firstName, lastName, street, number, zipCode, city)
        {
            FavoriteFootballClub = favoriteFootballClub;
        }
    }
}