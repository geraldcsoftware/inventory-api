using FastEndpoints;

namespace Inventory.Api.Endpoints.Categories;

public class GetCategoriesEndpoint : EndpointWithoutRequest<IReadOnlyCollection<InventoryCategory>>
{
    public override void Configure()
    {
        Get("categories");
        Options(x =>
        {
            x.WithName("GetCategories")
             .WithDisplayName("Get Categories")
             .WithDescription("Get inventory categories")
             .WithTags("Categories");
            x.WithOpenApi();
            x.CacheOutput(p => p.Expire(TimeSpan.FromMinutes(5)));
        });
        AllowAnonymous();
        ResponseCache(300);
    }

    public override Task<IReadOnlyCollection<InventoryCategory>> ExecuteAsync(CancellationToken ct)
    {
        return Task.FromResult<IReadOnlyCollection<InventoryCategory>>(new List<InventoryCategory>
        {
            new() { Id = 1, Name = "Women's Clothing", Description = "Women's Clothing - all ranges" },
            new() { Id = 2, Name = "Men's Clothing", Description = "Men's Clothing - all ranges" },
            new() { Id = 3, Name = "Children's Clothing", Description = "Children's Clothing - all ranges" },
            new() { Id = 4, Name = "Toys", Description = "Toys - all ranges" },
            new() { Id = 5, Name = "Electronics", Description = "Electronics - all ranges" },
            new() { Id = 6, Name = "Home & Garden", Description = "Home & Garden - all ranges" },
            new() { Id = 7, Name = "Sports & Leisure", Description = "Sports & Leisure - all ranges" },
            new() { Id = 8, Name = "Health & Beauty", Description = "Health & Beauty - all ranges" },
            new() { Id = 9, Name = "Motoring", Description = "Motoring - all ranges" },
        });
    }
}