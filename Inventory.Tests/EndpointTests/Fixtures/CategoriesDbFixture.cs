using Bogus;
using DotNet.Testcontainers.Builders;
using Inventory.Data;
using Inventory.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Testcontainers.PostgreSql;

namespace Inventory.Tests.EndpointTests.Fixtures;

public class CategoriesDbFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer _testContainer;

    public CategoriesDbFixture()
    {
        var waitStrategy = Wait.ForUnixContainer()
                               .UntilOperationIsSucceeded(CheckDatabaseAvailability, 10);

        _testContainer = new PostgreSqlBuilder()
                        .WithDatabase("InventoryApi.Tests.Categories")
                        .WithUsername("postgres")
                        .WithPassword("postgres")
                        .WithWaitStrategy(waitStrategy)
                        .Build();
    }

    public string ConnectionString => _testContainer.GetConnectionString();

    public async Task InitializeAsync()
    {
        await _testContainer.StartAsync();
        var dbContextOptions = new DbContextOptionsBuilder<InventoryDbContext>()
                              .UseNpgsql(_testContainer.GetConnectionString())
                              .Options;
        await using var dbContext = new InventoryDbContext(dbContextOptions);
        if ((await dbContext.Database.GetPendingMigrationsAsync()).Any())
            await dbContext.Database.MigrateAsync();

        var faker = new Faker<InventoryCategory>()
                   .RuleFor(x => x.Created, f =>  new DateTimeOffset(DateTime.SpecifyKind(f.Date.Past(), DateTimeKind.Utc), TimeSpan.Zero))
                   .RuleFor(x => x.Name, f => f.Commerce.Categories(1).First())
                   .RuleFor(x => x.Description, f => f.Commerce.ProductDescription())
                   .RuleFor(x => x.Metadata!, () => new Dictionary<string, string>
                    {
                        { "key1", "value1" },
                        { "key2", "value2" },
                        { "key3", "value3" }
                    });

        var demoCategories = faker.Generate(10);
        dbContext.AddRange(demoCategories);
        await dbContext.SaveChangesAsync();
    }

    Task IAsyncLifetime.DisposeAsync()
    {
        return _testContainer.DisposeAsync().AsTask();
    }

    private bool CheckDatabaseAvailability()
    {
        try
        {
            using var connection = new NpgsqlConnection(_testContainer.GetConnectionString());
            using var command = new NpgsqlCommand();
            connection.Open();
            command.Connection = connection;
            command.CommandText = "SELECT 1";
            var result = command.ExecuteReader();
            return result.Read();
        }
        catch (Exception)
        {
            return false;
        }
    }
}