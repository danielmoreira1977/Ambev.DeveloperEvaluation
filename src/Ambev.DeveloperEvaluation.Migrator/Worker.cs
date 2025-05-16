using Ambev.DeveloperEvaluation.Common.Primitives;
using Ambev.DeveloperEvaluation.Domain.Entities.Products;
using Ambev.DeveloperEvaluation.Domain.Entities.Users;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.ORM;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics;

namespace Ambev.DeveloperEvaluation.Migrator;

public class Worker(
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    public const string ActivitySourceName = "Migrations";
    private static readonly bool mustExecute = false;

    private static readonly ActivitySource s_activitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        if (!mustExecute)
        {
            hostApplicationLifetime.StopApplication();
            return;
        }

        using var activity = s_activitySource.StartActivity("Migrating database", ActivityKind.Client);

        var contextInterfaces = new[]
        {
            typeof(IDefaultContext)
        };

        try
        {
            using var scope = serviceProvider.CreateScope();

            foreach (var interfaceType in contextInterfaces)
            {
                var service = scope.ServiceProvider.GetRequiredService(interfaceType);

                if (service is not DbContext dbContext)
                {
                    throw new InvalidOperationException($"O serviço {interfaceType.Name} não é um DbContext.");
                }

                await EnsureDatabaseAsync(dbContext, cancellationToken);
                await RunMigrationAsync(dbContext, cancellationToken);
                await SeedDataAsync(dbContext, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            activity?.AddEvent(new ActivityEvent("Migration failed"));
            activity?.SetTag("exception", ex.ToString());
            throw;
        }

        hostApplicationLifetime.StopApplication();
    }

    private static List<Product> CreateProducts()
    {
        var ProductsCategory = new[] { "HomeOffice", "Office", "Garden", "Kitchen", "Bathroom" };

        var products = new List<Product>();

        var faker = new Faker("pt_BR");

        for (int i = 0; i < 10_000; i++)
        {
            var price = Math.Round(faker.Random.Decimal(1, 2000), 2, MidpointRounding.AwayFromZero);

            var product = new Product
                (
                    faker.PickRandom(ProductsCategory),
                    faker.Commerce.ProductDescription(),
                    new Price(price),
                    new Rating(faker.Random.Int(1, 100), faker.Random.Double(1, 100)),
                    faker.Commerce.ProductName(),
                    faker.Image.PicsumUrl()
                );

            products.Add(product);
        }

        return products;
    }

    private static List<User> CreateUsers()
    {
        var users = new List<User>();

        var faker = new Faker("pt_BR");

        UserRole[] userRoles = UserRole.List.ToArray();

        for (int i = 0; i < 10_000; i++)
        {
            var zipCode = new ZipCode(faker.Address.ZipCode());
            var geolocation = new Geolocation(faker.Address.Latitude(), faker.Address.Longitude());

            var address = new Address(faker.Address.City(), geolocation, faker.Address.BuildingNumber(), faker.Address.StreetAddress(), zipCode);
            var email = new Email(faker.Internet.Email());
            var name = new Name(faker.Name.FirstName(), faker.Name.LastName());
            var password = new Password(faker.Internet.Password());
            var phone = new Phone(faker.Phone.PhoneNumber());
            var role = faker.PickRandom(userRoles);
            var username = new Username(faker.Internet.UserName());

            var user = new User
                (
                    address,
                    email,
                    name,
                    password,
                    phone,
                    role,
                    username
                );

            users.Add(user);
        }

        return users;
    }

    private static async Task EnsureDatabaseAsync(DbContext dbContext, CancellationToken cancellationToken)
    {
        try
        {
            var dbCreator = dbContext.GetService<IRelationalDatabaseCreator>();

            var strategy = dbContext.Database.CreateExecutionStrategy();
            await
            strategy.ExecuteAsync(async () =>
            {
                var exists = await dbCreator.ExistsAsync(cancellationToken);

                if (!exists)
                {
                    await dbCreator.CreateAsync(cancellationToken);
                }
            });
        }
        catch (Exception ex)
        {
            var err = ex.Message;
            throw;
        }
    }

    private static async Task InsertProducts(DbContext dbContext, CancellationToken cancellationToken)
    {
        if (!await ((DefaultContext)dbContext).Products.AnyAsync(cancellationToken))
        {
            var productList = CreateProducts();

            ArgumentNullException.ThrowIfNull(productList);

            await ((DefaultContext)dbContext).AddRangeAsync(productList, cancellationToken);
        }
    }

    private static async Task InsertUsers(DbContext dbContext, CancellationToken cancellationToken)
    {
        if (!await ((DefaultContext)dbContext).Users.AnyAsync(cancellationToken))
        {
            var userList = CreateUsers();

            ArgumentNullException.ThrowIfNull(userList);

            await ((DefaultContext)dbContext).AddRangeAsync(userList, cancellationToken);
        }
    }

    private static async Task RunMigrationAsync(DbContext dbContext, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await dbContext.Database.MigrateAsync(cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        });
    }

    private static async Task SeedDataAsync(DbContext dbContext, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

            await InsertUsers(dbContext, cancellationToken);
            await InsertProducts(dbContext, cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        });
    }
}
