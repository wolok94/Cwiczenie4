using Ćwiczenie4_KamilWolak.Domain.Dtos;

namespace Ćwiczenie4_KamilWolak.Application.Interfaces
{
    public interface ICurrencyService
    {
        Task<IEnumerable<CurrencyDto>> GetCurrencies();
        Task<PaginationDto<GetCurrenciesDto>> GetCurrenciesByDate(DateTime startDate, DateTime endDate, PaginationFilterDto paginationFilter);
        Task AddCurrencies(DateTime startDate, DateTime endDate);
    }
}
