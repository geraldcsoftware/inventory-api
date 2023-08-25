using Inventory.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Data;

public class InventoryCategoriesRepository(InventoryDbContext dbContext) : IInventoryCategoriesRepository
{
    public async Task<IEnumerable<InventoryCategory>> GetCategories(CancellationToken cancellationToken = default)
    {
        return await dbContext.Categories.ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<InventoryCategory?> GetCategory(int id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Categories.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
    }

    public async Task<InventoryCategory> AddCategory(InventoryCategory category, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(category);
        await dbContext.Categories.AddAsync(category, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return category;
    }

    public async Task<InventoryCategory?> UpdateCategory(InventoryCategory category, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(category);
        dbContext.Categories.Update(category);
        await dbContext.SaveChangesAsync(cancellationToken);
        return category;
    }

    public async Task DeleteCategory(int id, CancellationToken cancellationToken = default)
    {
        await dbContext.Categories.Where(c => c.Id == id).ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }
}