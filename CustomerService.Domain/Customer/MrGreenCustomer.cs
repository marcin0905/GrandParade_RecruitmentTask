namespace CustomerService.Domain.Customer
{
    public class MrGreenCustomer : global::CustomerService.Domain.Customer.Customer
    {
        protected MrGreenCustomer() {}

        public MrGreenCustomer(
            string firstName,
            string lastName,
            string personalNumber,
            Address address) : base(firstName, lastName, address)
        {
            PersonalNumber = (PersonalNumber) personalNumber;
        }

        public virtual PersonalNumber PersonalNumber { get; }
    }
}