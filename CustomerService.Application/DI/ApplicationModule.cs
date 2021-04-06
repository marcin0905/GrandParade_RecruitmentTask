using Autofac;
using CustomerService.Application.Contracts.Services.Customer;
using CustomerService.Application.Enum;
using CustomerService.Application.Mediator.Behavior;
using CustomerService.Application.Services.Customer;
using CustomerService.Application.Services.Customer.Factory;
using CustomerService.Application.Services.Customer.Proxy;
using CustomerService.Infrastructure.DI;
using MediatR;

namespace CustomerService.Application.DI
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<InfrastructureModule>();
            builder.RegisterType<CustomerResponseFactory>().As<ICustomerResponseFactory>();
            builder.RegisterType<CustomerQueryService>().Keyed<ICustomerQueryService>(ServiceType.NoCache);
            builder.RegisterType<CustomerQueryServiceProxy>().Keyed<ICustomerQueryService>(ServiceType.Cache);

            builder.RegisterGeneric(typeof(TransactionBehavior<,>))
                .As(typeof(IPipelineBehavior<,>));
        }
    }
}