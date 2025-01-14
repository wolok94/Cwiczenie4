using Ćwiczenie4_KamilWolak.Dtos;
using Ćwiczenie4_KamilWolak.Entities;
using Ćwiczenie4_KamilWolak.Interfaces;
using System.Text.Json;
using Ćwiczenie4_KamilWolak.DbConnection;

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

    public async Task<List<ExchangeTable>> GetCurrenciesByDate(string date)
    {
        using var httpClient = new HttpClient();

        var url = $"https://api.nbp.pl/api/exchangerates/tables/A/{date}/?format=json";
        
        
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

    public async Task AddCurrencies(string date)
    {
        var currencies = await GetCurrenciesByDate(date);
        await _dbContext.ExchangeTables.AddRangeAsync(currencies);
        await _dbContext.SaveChangesAsync();
    }
    
    
}
