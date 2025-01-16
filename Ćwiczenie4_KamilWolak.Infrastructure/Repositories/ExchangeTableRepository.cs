using Ćwiczenie4_KamilWolak.Domain.Entities;
using Ćwiczenie4_KamilWolak.Domain.Interfaces;
using Ćwiczenie4_KamilWolak.Infrastructure.DbConnection;

namespace Ćwiczenie4_KamilWolak.Infrastructure.Repositories;

public class ExchangeTableRepository : GenericRepository<ExchangeTable>, IExchangeTableRepository
{
    public ExchangeTableRepository(CurrencyDbContext dbContext) : base(dbContext)
    {
    }
}