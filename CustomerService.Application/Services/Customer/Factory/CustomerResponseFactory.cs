using System;
using System.Collections.Generic;
using System.Linq;
using CustomerService.Application.Contracts.Services.Customer;
using CustomerService.Application.Dto.Customer;
using CustomerService.Domain.Customer;

namespace CustomerService.Application.Services.Customer.Factory
{
    public class CustomerResponseFactory : ICustomerResponseFactory
    {
        public BaseCustomerDto[] Create(IEnumerable<Domain.Customer.Customer> customers)
        {
            return customers.Select(GetCustomer).ToArray();
        }

        public BaseCustomerDto Create(Domain.Customer.Customer customer)
        {
            return GetCustomer(customer);
        }

        private static BaseCustomerDto GetCustomer(Domain.Customer.Customer customer)
        {
            var address = customer.Address;

            var addressDto = new CustomerAddressDto
            {
                Street = address.Street,
                City = address.City,
                ZipCode = address.ZipCode,
                Number = address.Number
            };

            return customer switch
            {
                MrGreenCustomer mrGreenCustomer => new MrGreenCustomerDto
                {
                    Id = mrGreenCustomer.Id,
                    FirstName = mrGreenCustomer.FirstName,
                    LastName = mrGreenCustomer.LastName,
                    PersonalNumber = mrGreenCustomer.PersonalNumber,
                    Address = addressDto
                },
                RedBetCustomer redBetCustomer => new RedBetCustomerDto
                {
                    Id = redBetCustomer.Id,
                    FirstName = redBetCustomer.FirstName,
                    LastName = redBetCustomer.LastName,
                    FavoriteFootballTeam = redBetCustomer.FavoriteFootballClub,
                    Address = addressDto
                },
                _ => throw new InvalidOperationException("Customer Type is not supported")
            };
        }
    }
}