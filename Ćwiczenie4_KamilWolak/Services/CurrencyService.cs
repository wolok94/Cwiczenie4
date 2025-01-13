using Ćwiczenie4_KamilWolak.Entities;
using Ćwiczenie4_KamilWolak.Interfaces;
using System.Text.Json;

namespace Ćwiczenie4_KamilWolak.Services;

public class CurrencyService : ICurrencyService
{


    public async Task GetCurrencies()
    {
        using var httpClient = new HttpClient();

        var url = "https://api.nbp.pl/api/exchangerates/tables/A/?format=json";


        var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);

        try
        {
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
        }catch(Exception ex){
            Console.WriteLine(ex.Message);
        }
    }
}
