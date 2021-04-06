using System;
using Common.Domain;

namespace CustomerService.Domain.Customer
{
    public abstract class Customer : BaseEntity
    {
        public virtual string FirstName { get; }
        public virtual string LastName { get; }
        public virtual Address Address { get; protected set; }

        protected Customer() {}

        protected Customer(string firstName, string lastName, Address address)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                throw new ArgumentNullException(nameof(firstName));
            }

            if (string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentNullException(nameof(lastName));
            }

            FirstName = firstName;
            LastName = lastName;
            Address = address;
        }

        public virtual void ChangeAddress(string street, int number, string zipCode, string city)
        {
            Address = Address.Create(street, number, zipCode, city);
        }
    }
}