using System.Text.Json;
using Ćwiczenie4_KamilWolak.Application.Interfaces;
using Ćwiczenie4_KamilWolak.Domain.Entities;
using Ćwiczenie4_KamilWolak.Domain.Interfaces;

namespace Ćwiczenie4_KamilWolak.Application.Services;

public class ExchangeTableService : IExchangeTableService
{
    private readonly IExchangeTableRepository _exchangeTableRepository;

    public ExchangeTableService(IExchangeTableRepository exchangeTableRepository)
    {
        _exchangeTableRepository = exchangeTableRepository;
    }
    
    public async Task AddExchangeTables(DateTime startDate, DateTime endDate)
    {
        var timeBetween = endDate - startDate;
        List<ExchangeTable> currencies = new List<ExchangeTable>();
        if (timeBetween.TotalDays > 93)
        {
            

            while (startDate < endDate)
            {
                var currentEndDate = startDate.AddDays(93) < endDate ? startDate.AddDays(93) : endDate;
                
                var downloadedCurrencies = await GetExchangeTablesByDateFromApi(startDate, currentEndDate);
                currencies.AddRange(downloadedCurrencies);
                
                startDate = currentEndDate;
            }
        }

        await _exchangeTableRepository.AddRange(currencies);
    }
    
    private async Task<IEnumerable<ExchangeTable>> GetExchangeTablesByDateFromApi(DateTime startDate, DateTime endDate)
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

        var ratesWithNormalizedTime = rates.Select(x => new ExchangeTable
        {
            Id = x.Id,
            No = x.No,
            Rates = x.Rates,
            Table = x.Table,
            EffectiveDate = x.EffectiveDate.ToUniversalTime()
        });

        return ratesWithNormalizedTime;
    }
}