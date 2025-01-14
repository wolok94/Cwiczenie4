﻿using Ćwiczenie4_KamilWolak.Dtos;
using Ćwiczenie4_KamilWolak.Entities;
using Ćwiczenie4_KamilWolak.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ćwiczenie4_KamilWolak.Controllers
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
        public async Task<ActionResult<IEnumerable<CurrencyDto>>> Currencies()
        {
            var currencies = await _currencyService.GetCurrencies();
            return Ok(currencies);
        }

        [HttpGet]
        [Route("{date}")]
        public async Task<ActionResult<List<ExchangeTable>>> CurrenciesByDate([FromRoute] string date)
        {
            var currencies = await _currencyService.GetCurrenciesByDate(date);
            return Ok(currencies);
        }
    }
}
