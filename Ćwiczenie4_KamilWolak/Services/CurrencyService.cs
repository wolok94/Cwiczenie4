﻿using Ćwiczenie4_KamilWolak.Dtos;
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

    public async Task<List<ExchangeTable>> GetCurrenciesByDate(DateTime startDate, DateTime endDate)
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

    public async Task AddCurrencies(DateTime startDate, DateTime endDate)
    {
        var timeBetween = endDate - startDate;
        List<ExchangeTable> currencies = new List<ExchangeTable>();
        if (timeBetween.TotalDays > 93)
        {
            

            while (startDate < endDate)
            {
                var currentEndDate = startDate.AddDays(93) < endDate ? startDate.AddDays(93) : endDate;
                
                var downloadedCurrencies = await GetCurrenciesByDate(startDate, currentEndDate);
                currencies.AddRange(downloadedCurrencies);
                
                startDate = currentEndDate;
            }
        }
        await _dbContext.ExchangeTables.AddRangeAsync(currencies);
        await _dbContext.SaveChangesAsync();
    }
    
    
}
