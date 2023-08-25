using Inventory.Data.Entities;

namespace Inventory.Data;

public interface IInventoryCategoriesRepository
{
    Task<IEnumerable<InventoryCategory>> GetCategories(CancellationToken cancellationToken = default);
    Task<InventoryCategory?> GetCategory(int id,CancellationToken cancellationToken = default);
    Task<InventoryCategory> AddCategory(InventoryCategory category,CancellationToken cancellationToken = default);
    Task<InventoryCategory?> UpdateCategory(InventoryCategory category,CancellationToken cancellationToken = default);
    Task DeleteCategory(int id,CancellationToken cancellationToken = default);
}