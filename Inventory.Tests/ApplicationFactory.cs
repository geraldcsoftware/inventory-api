using Inventory.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace Inventory.Tests;

public class ApplicationFactory : WebApplicationFactory<Api.Endpoints.Categories.GetCategoriesEndpoint>

{
    public WebApplicationFactory<Api.Endpoints.Categories.GetCategoriesEndpoint> WithConnectionString(string connectionString)
    {
        const string key = $"ConnectionStrings:{Connections.InventoryDbConnection}";
        return WithWebHostBuilder(builder =>
        {
            builder.ConfigureAppConfiguration((ctx, configurationBuilder) =>
            {
                configurationBuilder.AddInMemoryCollection(new Dictionary<string, string?>
                {
                    { key, connectionString }
                });
            });
        });
    }
}