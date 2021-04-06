using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Configuration.Extensions;
using Common.Configuration.Options;
using CustomerService.Application.DI;
using CustomerService.Application.Filters;
using CustomerService.Application.Mediator.Handler.Customer;
using CustomerService.Application.Middleware.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CustomerService.Web
{
    public class Startup
    {
        private const string DatabaseConnectionOptions = "DatabaseConnectionOptions";
        private const string RedisOptions = "Redis";
        private const string CachingTimeOptions = "CachingTime";

        public ILifetimeScope AutofacContainer { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // @TODO Create secrets or Pass credentials via ENV variables or Create Power Shell script to run Docker-Compose with All ENV Variables

            services.AddOptions();
            services.Configure<DatabaseConnectionOptions>(Configuration.GetSection(DatabaseConnectionOptions));
            services.Configure<RedisOptions>(Configuration.GetSection(RedisOptions));
            services.Configure<CachingTimeOptions>(Configuration.GetSection(CachingTimeOptions));

            services.AddControllers().AddNewtonsoftJson();
            services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(RegisterCustomerCommandHandler).Assembly);

            services.AddStackExchangeRedisCache(options =>
            {
                var redisOptions = Configuration.GetSection(RedisOptions).Get<RedisOptions>();
                options.Configuration = redisOptions.Host;
            });

            services.RunMigrationDatabase(
                Assembly.GetAssembly(typeof(Infrastructure.DataAccess.Migrations.InitialMigration)), 
                Configuration.GetSection(DatabaseConnectionOptions).Get<DatabaseConnectionOptions>());

            services.AddNHibernate(Assembly.GetAssembly(typeof(Infrastructure.DataAccess.Map.CustomerMap)));
            
            services.AddScoped<InvalidateCacheActionFilter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseCustomErrorHandling();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<ApplicationModule>();
        }
    }
}
