using Inventory.Data;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Api.Infrastructure;

public class DatabaseInitializer(IServiceProvider serviceProvider, ILogger<DatabaseInitializer> logger) : IAppInitializer
{
    public async ValueTask InitializeAsync(CancellationToken cancellationToken = default)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();
        var name = dbContext.Database.GetDbConnection().Database;
        
        logger.LogInformation("Migrating database '{Name}'...", name);
        await dbContext.Database.MigrateAsync(cancellationToken);
        logger.LogInformation("Database '{Name}' migrated successfully", name);
    }
}