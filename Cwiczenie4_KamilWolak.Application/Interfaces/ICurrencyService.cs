using Cwiczenie4_KamilWolak.Domain.Dtos;

namespace Cwiczenie4_KamilWolak.Application.Interfaces
{
    public interface ICurrencyService
    {
        Task<IEnumerable<CurrencyDto>> GetCurrencies();
        Task<PaginationDto<GetCurrenciesDto>> GetCurrenciesByDate(DateTime startDate, DateTime endDate, PaginationFilterDto paginationFilter);

    }
}
