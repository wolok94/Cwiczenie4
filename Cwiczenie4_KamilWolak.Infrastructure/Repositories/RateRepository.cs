using Cwiczenie4_KamilWolak.Domain.Dtos;
using Cwiczenie4_KamilWolak.Domain.Entities;
using Cwiczenie4_KamilWolak.Domain.Interfaces;
using Cwiczenie4_KamilWolak.Infrastructure.DbConnection;
using Microsoft.EntityFrameworkCore;

namespace Cwiczenie4_KamilWolak.Infrastructure.Repositories;

public class RateRepository : GenericRepository<Rate>, IRateRepository
{
    private readonly CurrencyDbContext _dbContext;

    public RateRepository(CurrencyDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<CurrencyDto>> GetCurrencies()
    {
        var rates = await _dbContext.Rates
            .Select(x => new CurrencyDto()
            {
                Name = x.Currency
            })
            .Distinct()
            .ToListAsync();

        return rates;
    }

    public async Task<IEnumerable<GetCurrenciesDto>> GetCurrenciesByDate(DateTime startDate, DateTime endDate, PaginationFilterDto paginationFilter)
    {
        var currencies = await _dbContext.Rates
            .Include(x => x.ExchangeTable)
            .Where(x => x.ExchangeTable.EffectiveDate >= startDate.ToUniversalTime()
                        && x.ExchangeTable.EffectiveDate <= endDate.ToUniversalTime())
            .Select(x => new GetCurrenciesDto
            {
                Id = x.Id,
                Currency = x.Currency,
                EffectiveDate = x.ExchangeTable.EffectiveDate,
                Code = x.Code,
                Mid = x.Mid
            })
            .OrderBy(x => x.EffectiveDate.Date)
            .ToListAsync();

        return currencies;
    }
}