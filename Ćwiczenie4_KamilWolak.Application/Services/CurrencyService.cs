using System.Text.Json;
using Ćwiczenie4_KamilWolak.Application.Interfaces;
using Ćwiczenie4_KamilWolak.Domain.Dtos;
using Ćwiczenie4_KamilWolak.Domain.Entities;
using Ćwiczenie4_KamilWolak.Domain.Interfaces;
using Ćwiczenie4_KamilWolak.Infrastructure.DbConnection;
using Microsoft.EntityFrameworkCore;

namespace Ćwiczenie4_KamilWolak.Application.Services;

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
        
        var currencies = basicCurrencies
            .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
            .Take(paginationFilter.PageSize)
            .ToList();
        
        var paginatedCurrencies = new PaginationDto<GetCurrenciesDto>(currencies, paginationFilter.PageSize, paginationFilter.PageNumber, basicCurrencies.Count());

        return paginatedCurrencies;
    }

    
    

    
    
}
