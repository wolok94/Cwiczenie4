
using Ćwiczenie4_KamilWolak.Dtos;
using Ćwiczenie4_KamilWolak.Entities;

namespace Ćwiczenie4_KamilWolak.Interfaces
{
    public interface ICurrencyService
    {
        Task<IEnumerable<CurrencyDto>> GetCurrencies();
        Task<List<ExchangeTable>> GetCurrenciesByDate(DateTime startDate, DateTime endDate);
        Task AddCurrencies(DateTime startDate, DateTime endDate);
    }
}
