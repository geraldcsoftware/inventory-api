namespace Inventory.Api.Endpoints.Categories;

public class CreateInventoryCategoryRequest {
    public required string Name {get; set;}
    public required string Description {get; set;}
}