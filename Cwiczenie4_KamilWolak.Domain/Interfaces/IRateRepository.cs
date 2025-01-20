using Cwiczenie4_KamilWolak.Domain.Dtos;
using Cwiczenie4_KamilWolak.Domain.Entities;

namespace Cwiczenie4_KamilWolak.Domain.Interfaces;

public interface IRateRepository : IGenericRepository<Rate>
{
    Task<IEnumerable<CurrencyDto>> GetCurrencies();

    Task<IEnumerable<GetCurrenciesDto>> GetCurrenciesByDate(DateTime startDate, DateTime endDate,
        PaginationFilterDto paginationFilter);
}