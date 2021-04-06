using CustomerService.Application.Dto.Customer;
using CustomerService.Application.Mediator.Command.Customer;

namespace CustomerService.Application.Mediator.Command
{
    public static class CommandMapper
    {
        public static RegisterMrGreenCustomerCommand CreateCommand(MrGreenCustomerDto customerDto)
        {
            if (customerDto == null) return default;

            var address = customerDto.Address;

            return new RegisterMrGreenCustomerCommand(
                customerDto.FirstName,
                customerDto.LastName,
                address.Street,
                address.Number,
                address.ZipCode,
                address.City,
                customerDto.PersonalNumber
            );
        }

        public static RegisterRedBetCustomerCommand CreateCommand(RedBetCustomerDto customerDto)
        {
            if (customerDto == null) return default;

            var address = customerDto.Address;

            return new RegisterRedBetCustomerCommand(
                customerDto.FirstName,
                customerDto.LastName,
                address.Street,
                address.Number,
                address.ZipCode,
                address.City,
                customerDto.FavoriteFootballTeam
            );
        }
    }
}
