using Common.Domain;
using System;

namespace CustomerService.Domain.Customer
{
    public class Address : ValueObject<Address>
    {
        public string Street { get; }
        public int Number { get; }
        public string ZipCode { get; }
        public string City { get; }

        protected Address()
        { }

        private Address(string street, int number, string zipCode, string city)
        {
            Street = street;
            Number = number;
            ZipCode = zipCode;
            City = city;
        }

        protected override bool EqualsCore(Address other)
        {
            return Street == other.Street &&
                   Number == other.Number &&
                   ZipCode == other.ZipCode &&
                   City == other.City;
        }

        protected override int GetHashCodeCore()
        {
            return HashCode.Combine(Street, Number, ZipCode, City);
        }

        public static Address Create(string street, int? number, string zipCode, string city)
        {
            if (string.IsNullOrWhiteSpace(street))
            {
                throw new DomainException($"{nameof(street)} - cannot be empty");
            }

            if (!number.HasValue)
            {
                throw new DomainException($"{nameof(number)} - cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(zipCode))
            {
                throw new DomainException($"{nameof(zipCode)} - cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(city))
            {
                throw new DomainException($"{nameof(city)} - cannot be empty");
            }

            return new Address(street, number.Value, zipCode, city);
        }
    }
}