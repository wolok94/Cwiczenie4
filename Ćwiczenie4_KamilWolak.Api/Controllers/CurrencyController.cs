using Ćwiczenie4_KamilWolak.Application.Interfaces;
using Ćwiczenie4_KamilWolak.Domain.Dtos;
using Ćwiczenie4_KamilWolak.Domain.Entities;
using Microsoft.AspNetCore.Mvc;


namespace Ćwiczenie4_KamilWolak.Api.Controllers
{
    [ApiController]
    [Route("currencies")]
    public class CurrencyController : Controller
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }
        [HttpGet]
        public async Task<IActionResult> Currencies()
        {
            var currencies = await _currencyService.GetCurrencies();

            if (currencies == null)
            {
                return NotFound();
            }
            
            return Ok(currencies);
        }

        [HttpGet]
        [Route("{startDate}/{endDate}")]
        public async Task<IActionResult> CurrenciesByDate([FromRoute] DateTime startDate, [FromRoute] DateTime endDate, [FromQuery] PaginationFilterDto paginationFilter)
        {
            if (endDate < startDate)
            {
                return BadRequest();
            }
            var currencies = await _currencyService.GetCurrenciesByDate(startDate, endDate, paginationFilter);
            return Ok(currencies);
        }

        [HttpPost]
        [Route("fetch")]
        public async Task<IActionResult> SaveCurrencies([FromBody] FetchCurrenciesDto dto)
        {
            if (dto.EndDate < dto.StartDate)
            {
                return BadRequest();
            }
            await _currencyService.AddCurrencies(dto.StartDate, dto.EndDate);
            return Ok();
        }
    }
}
