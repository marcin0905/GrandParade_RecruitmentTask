using System;
using System.Text.RegularExpressions;
using Common.Domain;

namespace CustomerService.Domain.Customer
{
    public sealed class PersonalNumber : ValueObject<PersonalNumber>
    {
        public string IdentificationNumber { get; }

        protected PersonalNumber() {}

        private PersonalNumber(string identificationNumber)
        {
            IdentificationNumber = identificationNumber;
        }

        protected override bool EqualsCore(PersonalNumber other)
        {
            return IdentificationNumber == other.IdentificationNumber;
        }

        protected override int GetHashCodeCore()
        {
            return HashCode.Combine(IdentificationNumber);
        }

        public static PersonalNumber Create(string personalNumber)
        {
            if (!Regex.IsMatch(personalNumber, @"^\d{5}-\d{3}$"))
            {
                throw new DomainException("Personal identificationNumber has incorrect format");
            }

            return new PersonalNumber(personalNumber);
        }

        public static implicit operator string(PersonalNumber personalNumber) => personalNumber.IdentificationNumber;
        public static explicit operator PersonalNumber(string personalNumber) => Create(personalNumber);
    }
}