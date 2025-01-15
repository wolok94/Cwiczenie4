
using Ćwiczenie4_KamilWolak.Dtos;
using Ćwiczenie4_KamilWolak.Entities;

namespace Ćwiczenie4_KamilWolak.Interfaces
{
    public interface ICurrencyService
    {
        Task<IEnumerable<CurrencyDto>> GetCurrencies();
        Task<PaginationDto<GetCurrenciesDto>> GetCurrenciesByDate(DateTime startDate, DateTime endDate, PaginationFilterDto paginationFilter);
        Task AddCurrencies(DateTime startDate, DateTime endDate);
    }
}
