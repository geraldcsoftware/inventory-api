using FastEndpoints;
using Inventory.Data;

namespace Inventory.Api.Endpoints.Categories;

public class GetCategoriesEndpoint(IInventoryCategoriesRepository repository) : 
    EndpointWithoutRequest<IReadOnlyCollection<InventoryCategory>, InventoryCategoryMapper>
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

    public override async Task<IReadOnlyCollection<InventoryCategory>> ExecuteAsync(CancellationToken ct)
    {
        var categories = await repository.GetCategories(ct);
        return categories.Select(c => Mapper.FromEntity(c)).ToList();
    }
}