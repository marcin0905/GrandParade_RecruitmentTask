using Autofac;
using CustomerService.Domain.Customer.Repository;
using CustomerService.Infrastructure.DataAccess.Repository.Customer;

namespace CustomerService.Infrastructure.DI
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();
        }
    }
}