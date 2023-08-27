using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Inventory.Data.PostgreSQL;

public class DesignTimeDbContextFactory: IDesignTimeDbContextFactory<InventoryPostgresDbContext>
{
    public InventoryPostgresDbContext CreateDbContext(string[] args)
    {
        return new InventoryPostgresDbContext(new DbContextOptionsBuilder()
            .UseNpgsql("Host=localhost;Database=Inventory;Username=postgres;Password=postgres")
            .Options);
    }
}