using System.Text.Json;
using Cwiczenie4_KamilWolak.Application.Interfaces;
using Cwiczenie4_KamilWolak.Domain.Dtos;
using Cwiczenie4_KamilWolak.Domain.Entities;
using Cwiczenie4_KamilWolak.Domain.Interfaces;
using Cwiczenie4_KamilWolak.Infrastructure.DbConnection;

using Microsoft.EntityFrameworkCore;

namespace Cwiczenie4_KamilWolak.Application.Services;

public class CurrencyService : ICurrencyService
{
    private readonly IRateRepository _rateRepository;
    private readonly CurrencyDbContext _dbContext;

    public CurrencyService(IRateRepository rateRepository)
    {
        _rateRepository = rateRepository;
    }

    public async Task<IEnumerable<CurrencyDto>> GetCurrencies()
    {
            var rates = await _rateRepository.GetCurrencies();

            return rates;

    }

    public async Task<PaginationDto<GetCurrenciesDto>> GetCurrenciesByDate(DateTime startDate, DateTime endDate, PaginationFilterDto paginationFilter)
    {
        var basicCurrencies = await _rateRepository.GetCurrenciesByDate(startDate, endDate, paginationFilter);

        if (paginationFilter.SearchPhrase != null)
        {
            basicCurrencies = basicCurrencies.Where(x => x.Currency.Contains(paginationFilter.SearchPhrase) ||
                                                         x.Code.Contains(paginationFilter.SearchPhrase) ||
                                                         x.EffectiveDate.ToString().Contains(paginationFilter.SearchPhrase));
        }
        
        var currencies = basicCurrencies
            .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
            .Take(paginationFilter.PageSize)
            .ToList();
        
        var paginatedCurrencies = new PaginationDto<GetCurrenciesDto>(currencies, paginationFilter.PageSize, paginationFilter.PageNumber, basicCurrencies.Count());

        return paginatedCurrencies;
    }

    
    

    
    
}
