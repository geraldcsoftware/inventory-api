using FastEndpoints;

namespace Inventory.Api.Endpoints.Categories;

public class InventoryCategoryMapper : ResponseMapper<InventoryCategory, Data.Entities.InventoryCategory>
{
    public override InventoryCategory FromEntity(Data.Entities.InventoryCategory e)
    {
        return new()
        {
            Description = e.Description,
            Id = e.Id,
            Name = e.Name
        };
    }
}