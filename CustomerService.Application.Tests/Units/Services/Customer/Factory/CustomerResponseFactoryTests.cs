using System;
using CustomerService.Application.Contracts.Services.Customer;
using CustomerService.Application.Dto.Customer;
using CustomerService.Application.Services.Customer.Factory;
using CustomerService.Application.Tests.Configuration;
using CustomerService.Application.Tests.Fakes.Customer;
using CustomerService.Domain.Customer;
using NUnit.Framework;

namespace CustomerService.Application.Tests.Units.Services.Customer.Factory
{
    public class CustomerResponseFactoryTests : TestContainer
    {
        private ICustomerResponseFactory _customerResponseFactory;
        private readonly Address _address = Address.Create("street", 1, "31416", "City");

        [SetUp]
        public void SetUp()
        {
            _customerResponseFactory = new CustomerResponseFactory();
        }

        [Test]
        public void It_should_throw_invalid_operation_exception()
        {
            // Arrange

            var customers = new Domain.Customer.Customer[]
            {
                new FakeCustomer("Fake 1", "Green", _address), 
            };

            // Assert

            Assert.Catch<InvalidOperationException>(() => _customerResponseFactory.Create(customers));
        }

        [Test]
        public void It_should_return_empty_list()
        {
            // Arrange

            var customers = new Domain.Customer.Customer[0];

            // Act

            var result = _customerResponseFactory.Create(customers);

            // Assert

            Assert.IsEmpty(result);
        }

        [Test]
        public void It_should_create_mr_green_customers()
        {
            // Arrange

            var customers = new Domain.Customer.Customer[]
            {
                new MrGreenCustomer("Green 1", "Green", "12345-222", _address),
                new MrGreenCustomer("Green 2", "Red", "12345-222", _address),
            };

            // Act

            var result = _customerResponseFactory.Create(customers);

            // Assert

            Assert.AreEqual(customers.Length, result.Length);

            for (var i = 0; i < customers.Length; i++)
            {
                var customer = customers[i] as MrGreenCustomer;
                var mappedCustomer = result[i] as MrGreenCustomerDto;

                Assert.IsNotNull(customer);
                Assert.IsNotNull(mappedCustomer);
                Assert.IsInstanceOf<MrGreenCustomer>(customer);
                Assert.IsInstanceOf<MrGreenCustomerDto>(mappedCustomer);
                Assert.AreEqual(customer.FirstName, mappedCustomer.FirstName);
                Assert.AreEqual(customer.LastName, mappedCustomer.LastName);
                Assert.AreEqual(customer.Address.Street, mappedCustomer.Address.Street);
                Assert.AreEqual(customer.PersonalNumber.IdentificationNumber, mappedCustomer.PersonalNumber);
            }
        }

        [Test]
        public void It_should_create_red_bet_customers()
        {
            // Arrange

            var customers = new Domain.Customer.Customer[]
            {
                new RedBetCustomer("Red 1", "Red", "MC", _address), 
                new RedBetCustomer("Red 2", "Red", "MU", _address), 
            };

            // Act

            var result = _customerResponseFactory.Create(customers);

            // Assert

            Assert.AreEqual(customers.Length, result.Length);

            for (var i = 0; i < customers.Length; i++)
            {
                var customer = customers[i] as RedBetCustomer;
                var mappedCustomer = result[i] as RedBetCustomerDto;

                Assert.IsNotNull(customer);
                Assert.IsNotNull(mappedCustomer);
                Assert.IsInstanceOf<RedBetCustomer>(customer);
                Assert.IsInstanceOf<RedBetCustomerDto>(mappedCustomer);
                Assert.AreEqual(customer.FirstName, mappedCustomer.FirstName);
                Assert.AreEqual(customer.LastName, mappedCustomer.LastName);
                Assert.AreEqual(customer.Address.Street, mappedCustomer.Address.Street);
                Assert.AreEqual(customer.FavoriteFootballClub.Club, mappedCustomer.FavoriteFootballTeam);
            }
        }
    }
}