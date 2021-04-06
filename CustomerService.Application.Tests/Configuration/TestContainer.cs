using Autofac;

namespace CustomerService.Application.Tests.Configuration
{
    public abstract class TestContainer
    {
        protected IContainer Container { get; private set; }


        protected TestContainer()

        {

            Initialize();

        }


        public void Initialize()

        {

            var builder = new ContainerBuilder();


            RegisterTypes(builder);


            Container = builder.Build();

        }


        private static void RegisterTypes(ContainerBuilder builder)
        {

        }
    }
}