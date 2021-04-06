using CustomerService.Domain.Customer;
using FluentNHibernate.Mapping;

namespace CustomerService.Infrastructure.DataAccess.Map
{
    public class MrGreenCustomerMap : SubclassMap<MrGreenCustomer>
    {
        public MrGreenCustomerMap()
        {
            Table(Tables.MrGreenCustomers);

            Abstract();

            Component(customer =>
                customer.PersonalNumber, pn => pn.Map(x => x.IdentificationNumber));
        }
    }

    public class RedBetCustomerMap : SubclassMap<RedBetCustomer>
    {
        public RedBetCustomerMap()
        {
            Table(Tables.RedBetCustomers);

            Abstract();

            Component(customer =>
                customer.FavoriteFootballClub, club => club.Map(x => x.Club));
        }
    }

    public class CustomerMap : ClassMap<Domain.Customer.Customer>
    {
        public CustomerMap()
        {
            UseUnionSubclassForInheritanceMapping();

            Id(customer => customer.Id).GeneratedBy.Assigned();
            Map(customer => customer.FirstName);
            Map(customer => customer.LastName);
            Component(comp => comp.Address, address =>
            {
                address.Map(x => x.Street);
                address.Map(x => x.Number);
                address.Map(x => x.ZipCode);
                address.Map(x => x.City);
            });
        }
    }
}