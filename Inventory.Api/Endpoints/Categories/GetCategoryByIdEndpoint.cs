using FastEndpoints;
using Inventory.Data;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Inventory.Api.Endpoints.Categories;

public class GetCategoryByIdEndpoint(IInventoryCategoriesRepository repository) :
    EndpointWithoutRequest<Results<Ok<InventoryCategory>, NotFound>, InventoryCategoryMapper>
{
    public override void Configure()
    {
        Get("categories/{id:int}");
        Options(x =>
        {
            x.WithName("GetCategoryById")
             .WithDisplayName("Get Category By Id")
             .WithDescription("Get inventory category by id")
             .WithTags("Categories");
            x.WithOpenApi();
            x.CacheOutput(p => p.Expire(TimeSpan.FromMinutes(5)));
        });
        AllowAnonymous();
        ResponseCache(300);
    }

    public override async Task<Results<Ok<InventoryCategory>, NotFound>> ExecuteAsync(CancellationToken ct)
    {
        var id = Route<int>("id");
        if (id <= 0)
        {
            return TypedResults.NotFound();
        }
        
        var category = await repository.GetCategory(id, ct);
        return category switch
        {
            null => TypedResults.NotFound(),
            _    => TypedResults.Ok(Map.FromEntity(category))
        };
    }
}