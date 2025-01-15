﻿using Ćwiczenie4_KamilWolak.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ćwiczenie4_KamilWolak.DbConnection;

public class CurrencyDbContext : DbContext
{
    public CurrencyDbContext(DbContextOptions<CurrencyDbContext> options) : base(options)
    {
        
    }

    public DbSet<Rate> Rates { get; set; }
    public DbSet<ExchangeTable> ExchangeTables { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ExchangeTable>()
            .HasMany(x => x.Rates)
            .WithOne(x => x.ExchangeTable)
            .HasForeignKey(x => x.ExchangeTableId);
    }
}