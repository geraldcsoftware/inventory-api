using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Data;

public static class ServiceCollectionExtensions
{
    public static void AddInventoryRepositories(this IServiceCollection services)
    {
        services.AddScoped<IInventoryCategoriesRepository, InventoryCategoriesRepository>();
    }
}