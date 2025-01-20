using Cwiczenie4_KamilWolak.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cwiczenie4_KamilWolak.Infrastructure.DbConnection;

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