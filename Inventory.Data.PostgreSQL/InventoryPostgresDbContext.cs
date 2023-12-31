﻿using Microsoft.EntityFrameworkCore;

namespace Inventory.Data.PostgreSQL;

public class InventoryPostgresDbContext(DbContextOptions options): InventoryDbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryPostgresDbContext).Assembly);
    }
}