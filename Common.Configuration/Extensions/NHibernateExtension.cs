using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlClient;
using System.Reflection;
using Common.Configuration.Options;
using Common.Infrastructure.DataAccess.Connectors;
using FluentMigrator.Runner;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Common.Infrastructure.DataAccess.Contracts;
using Microsoft.Extensions.Options;

namespace Common.Configuration.Extensions
{
    public static class NHibernateExtension
    {
        public static IServiceCollection AddNHibernate(
            this IServiceCollection services, Assembly mappingAssembly)
        {
            var serviceProvider = services.BuildServiceProvider(false);
            using var scope = serviceProvider.CreateScope();
            var databaseConnectionOptions = serviceProvider.GetRequiredService<IOptions<DatabaseConnectionOptions>>().Value;

            var configuration = Fluently
                .Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(c =>
                    c.Server(databaseConnectionOptions.Server)
                     .Database(databaseConnectionOptions.Database)
                     .Username(databaseConnectionOptions.User)
                     .Password(databaseConnectionOptions.Password)
                ))
                .Mappings(c => c.FluentMappings.AddFromAssembly(mappingAssembly))
                .BuildConfiguration();

            var sessionFactory = configuration.BuildSessionFactory();

            services.AddSingleton(sessionFactory);
            services.AddScoped(factory => sessionFactory.OpenSession());
            services.AddScoped<IUnitOfWork, NHibernateSessionFactory>();

            return services;
        }

        public static IServiceCollection RunMigrationDatabase(
            this IServiceCollection services, Assembly assembly, DatabaseConnectionOptions databaseConnectionOptions)
        {
            using (var connection = new SqlConnection(GetConnectionString(databaseConnectionOptions, "master")))
            {
                var command = new SqlCommand($@"
                    IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = '{databaseConnectionOptions.Database}')
                    BEGIN

                        CREATE DATABASE [{databaseConnectionOptions.Database}]
                    END", connection);
                connection.Open();
                command.ExecuteNonQuery();
            }

            services.AddFluentMigratorCore();
            services.ConfigureRunner(rb =>
            {
                rb
                    .AddSqlServer()
                    .WithGlobalConnectionString(GetConnectionString(databaseConnectionOptions))
                    .ScanIn(assembly).For.Migrations();
            });

            var serviceProvider = services.BuildServiceProvider(false);
            using var scope = serviceProvider.CreateScope();
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            
            runner.MigrateUp();

            return services;
        }

        private static string GetConnectionString(DatabaseConnectionOptions options, string databaseName = null)
        {
            databaseName = string.IsNullOrEmpty(databaseName) ? options.Database : databaseName;

            return $"Server={options.Server};Database={databaseName}" +
                $";User={options.User};Password={options.Password}";
        }
    }
}