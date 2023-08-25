using Inventory.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Data;

public class InventoryDbContext(DbContextOptions<InventoryDbContext> options) : DbContext(options)
{
    public DbSet<InventoryCategory> Categories => Set<InventoryCategory>();
}