using Ćwiczenie4_KamilWolak.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ćwiczenie4_KamilWolak.Controllers
{
    [ApiController]
    public class CurrencyController : Controller
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }
        [HttpGet]
        [Route("currencies")]
        public async Task Currencies()
        {
            await _currencyService.GetCurrencies();
        }
    }
}
