using CustomerService.Domain.Customer;
using FluentMigrator;

namespace CustomerService.Infrastructure.DataAccess.Migrations
{
    [Migration(202104041527)]
    public class InitialMigration : Migration
    {
        public override void Up()
        {
            Create.Table(Tables.MrGreenCustomers)
                .WithColumn(nameof(Customer.Id)).AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
                .WithColumn(nameof(Customer.FirstName)).AsString().NotNullable()
                .WithColumn(nameof(Customer.LastName)).AsString().NotNullable()
                .WithColumn(nameof(PersonalNumber.IdentificationNumber)).AsString().NotNullable()
                .WithColumn(nameof(Address.Street)).AsString().NotNullable()
                .WithColumn(nameof(Address.Number)).AsString().NotNullable()
                .WithColumn(nameof(Address.ZipCode)).AsString().NotNullable()
                .WithColumn(nameof(Address.City)).AsString().NotNullable();

            Create.Table(Tables.RedBetCustomers)
                .WithColumn(nameof(Customer.Id)).AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
                .WithColumn(nameof(Customer.FirstName)).AsString().NotNullable()
                .WithColumn(nameof(Customer.LastName)).AsString().NotNullable()
                .WithColumn(nameof(FavoriteFootballClub.Club)).AsString().NotNullable()
                .WithColumn(nameof(Address.Street)).AsString().NotNullable()
                .WithColumn(nameof(Address.Number)).AsString().NotNullable()
                .WithColumn(nameof(Address.ZipCode)).AsString().NotNullable()
                .WithColumn(nameof(Address.City)).AsString().NotNullable();
        }

        public override void Down()
        {
            Delete.Table(Tables.MrGreenCustomers);
            Delete.Table(Tables.RedBetCustomers);
        }
    }
}