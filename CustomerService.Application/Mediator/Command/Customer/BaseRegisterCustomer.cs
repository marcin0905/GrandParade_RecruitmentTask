namespace CustomerService.Application.Mediator.Command.Customer
{
    public abstract class BaseRegisterCustomer
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string Street { get; }
        public int Number { get; }
        public string ZipCode { get; }
        public string City { get; }

        protected BaseRegisterCustomer(
            string firstName, string lastName, string street, int number, string zipCode, string city)
        {
            FirstName = firstName;
            LastName = lastName;
            Street = street;
            Number = number;
            ZipCode = zipCode;
            City = city;
        }
    }
}