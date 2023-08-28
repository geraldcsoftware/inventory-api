using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Endpoints.Categories;

public class GetInventoryCategoryByIdRequest
{
    [FromRoute(Name = "id")]
    public int Id { get; set; }
}