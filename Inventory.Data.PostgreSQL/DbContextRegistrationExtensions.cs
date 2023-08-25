using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Data.PostgreSQL;

public static class DbContextRegistrationExtensions
{
    public static void AddInventoryPostgresDbContext(this IServiceCollection services)
    {
        services.AddDbContext<InventoryDbContext, InventoryPostgresDbContext>((serviceProvider, options) =>
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString(Connections.InventoryDbConnection);
            var dbOptions = configuration.GetSection(nameof(DbOptions)).Get<DbOptions>();

            options.UseNpgsql(connectionString,
                              optionsBuilder =>
                              {
                                  optionsBuilder.MigrationsAssembly(typeof(InventoryPostgresDbContext).Assembly.FullName);
                              });

            options.EnableSensitiveDataLogging(dbOptions?.LogSensitiveData ?? false);
            options.EnableServiceProviderCaching();
        });
    }
}