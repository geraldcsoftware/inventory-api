using FastEndpoints;

namespace Inventory.Api.Endpoints.Categories;

public class CreateInventoryCategoryMapper : Mapper<CreateInventoryCategoryRequest, InventoryCategory, Data.Entities.InventoryCategory>
{
    public override  Data.Entities.InventoryCategory ToEntity(CreateInventoryCategoryRequest request) =>
        new()
        {
            Name = request.Name,
            Description = request.Description
        };
    
    
    
}