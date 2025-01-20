using Cwiczenie4_KamilWolak.Domain.Entities;
using Cwiczenie4_KamilWolak.Domain.Interfaces;
using Cwiczenie4_KamilWolak.Infrastructure.DbConnection;

namespace Cwiczenie4_KamilWolak.Infrastructure.Repositories;

public class ExchangeTableRepository : GenericRepository<ExchangeTable>, IExchangeTableRepository
{
    public ExchangeTableRepository(CurrencyDbContext dbContext) : base(dbContext)
    {
    }
}