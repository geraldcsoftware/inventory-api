using FastEndpoints;
using Inventory.Data;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Inventory.Api.Endpoints.Categories;

public sealed class CreateInventoryCategoryEndpoint(IInventoryCategoriesRepository repository,
                                                    ILogger<CreateInventoryCategoryEndpoint> logger)
    : Endpoint<CreateInventoryCategoryRequest, IResult, CreateInventoryCategoryMapper>
{
    public override void Configure()
    {
        Post("categories");
        Claims("inventory:category:create", "inventory:admin");
    }

    public override async Task<IResult> ExecuteAsync(CreateInventoryCategoryRequest req, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(req);

        logger.LogInformation("Creating inventory category {@Category}", req);
        var category = Map.ToEntity(req);
        category = await repository.AddCategory(category, ct);
        var result = Map.FromEntity(category);

        logger.LogInformation("Inventory category has been successfully created: {@Result}", result);
        return Results.Created(string.Empty, result);
    }
}