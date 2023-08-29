using FastEndpoints;
using Inventory.Data;
using Microsoft.OpenApi.Models;

namespace Inventory.Api.Endpoints.Categories;

public class GetCategoryByIdEndpoint(IInventoryCategoriesRepository repository) :
    EndpointWithoutRequest<InventoryCategory, InventoryCategoryMapper>
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
            x.WithOpenApi(openApiOperation =>
            {
                openApiOperation.Parameters.Add(new OpenApiParameter
                {
                    Name = "id",
                    In = ParameterLocation.Path,
                    Description = "Category id",
                    Required = true,
                    Schema = new OpenApiSchema
                    {
                        Type = "integer",
                        Minimum = 1
                    }
                });
                return openApiOperation;
            });
            x.CacheOutput(p => p.Expire(TimeSpan.FromMinutes(5)));
        });
        AllowAnonymous();
        ResponseCache(300);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<int>("id");
        if (id <= 0)
        {
            AddError("Invalid category id");
        }

        ThrowIfAnyErrors();

        var category = await repository.GetCategory(id, ct);
        if (category is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var result = Map.FromEntity(category);
        await SendOkAsync(result, ct);
    }
}