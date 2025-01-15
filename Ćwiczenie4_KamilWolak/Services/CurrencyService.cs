using Ćwiczenie4_KamilWolak.Dtos;
using Ćwiczenie4_KamilWolak.Entities;
using Ćwiczenie4_KamilWolak.Interfaces;
using System.Text.Json;
using Ćwiczenie4_KamilWolak.DbConnection;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

namespace Ćwiczenie4_KamilWolak.Services;

public class CurrencyService : ICurrencyService
{
    private readonly CurrencyDbContext _dbContext;

    public CurrencyService(CurrencyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<CurrencyDto>> GetCurrencies()
    {
        using var httpClient = new HttpClient();

        var url = "https://api.nbp.pl/api/exchangerates/tables/A/?format=json";
        


            var httpResponse = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, url));

            httpResponse.EnsureSuccessStatusCode();
            string responseBody = await httpResponse.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
            var rates = JsonSerializer.Deserialize<List<ExchangeTable>>(responseBody, options);
            var mappedRates = rates.SelectMany(x => x.Rates.Select(c =>  new CurrencyDto
            {
                Name = c.Currency
            })).ToList();

            return mappedRates;

    }

    public async Task<PaginationDto<GetCurrenciesDto>> GetCurrenciesByDate(DateTime startDate, DateTime endDate, PaginationFilterDto paginationFilter)
    {
        var basicCurrencies = await _dbContext.Rates
            .Include(x => x.ExchangeTable)
            .Where(x => x.ExchangeTable.EffectiveDate >= startDate
                        && x.ExchangeTable.EffectiveDate <= endDate)
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
        
        var currencies = basicCurrencies
            .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
            .Take(paginationFilter.PageSize)
            .ToList();
        
        var paginatedCurrencies = new PaginationDto<GetCurrenciesDto>(currencies, paginationFilter.PageSize, paginationFilter.PageNumber, basicCurrencies.Count);

        return paginatedCurrencies;
    }



    public async Task AddCurrencies(DateTime startDate, DateTime endDate)
    {
        var timeBetween = endDate - startDate;
        List<ExchangeTable> currencies = new List<ExchangeTable>();
        if (timeBetween.TotalDays > 93)
        {
            

            while (startDate < endDate)
            {
                var currentEndDate = startDate.AddDays(93) < endDate ? startDate.AddDays(93) : endDate;
                
                var downloadedCurrencies = await GetCurrenciesByDateFromApi(startDate, currentEndDate);
                currencies.AddRange(downloadedCurrencies);
                
                startDate = currentEndDate;
            }
        }
        await _dbContext.ExchangeTables.AddRangeAsync(currencies);
        await _dbContext.SaveChangesAsync();
    }
    
    private async Task<List<ExchangeTable>> GetCurrenciesByDateFromApi(DateTime startDate, DateTime endDate)
    {
        using var httpClient = new HttpClient();

        var url = $"https://api.nbp.pl/api/exchangerates/tables/A/{startDate:yyyy-MM-dd}/{endDate:yyyy-MM-dd}/?format=json";
        
        
        var httpResponse = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, url));
        httpResponse.EnsureSuccessStatusCode();
        string responseBody = await httpResponse.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };
        var rates = JsonSerializer.Deserialize<List<ExchangeTable>>(responseBody, options);

        return rates;
    }
    
    
}
