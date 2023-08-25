namespace Inventory.Data.Entities;

public class InventoryCategory
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string? Description { get; set; }
    public DateTimeOffset Created { get; init; }
    public DateTimeOffset? LastModified { get; set; }
    public Dictionary<string, string?> Metadata { get; init; } = new();
}