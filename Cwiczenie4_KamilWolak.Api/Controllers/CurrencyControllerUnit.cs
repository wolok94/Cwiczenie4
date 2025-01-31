﻿using Cwiczenie4_KamilWolak.Application.Interfaces;
using Cwiczenie4_KamilWolak.Domain.Dtos;
using Cwiczenie4_KamilWolak.Domain.Entities;
using Microsoft.AspNetCore.Mvc;


namespace Cwiczenie4_KamilWolak.Api.Controllers
{
    [ApiController]
    [Route("currencies")]
    public class CurrencyControllerUnit : Controller
    {
        private readonly ICurrencyService _currencyService;
        private readonly IExchangeTableService _exchangeTableService;

        public CurrencyControllerUnit(ICurrencyService currencyService, IExchangeTableService exchangeTableService)
        {
            _currencyService = currencyService;
            _exchangeTableService = exchangeTableService;
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
            await _exchangeTableService.AddExchangeTables(dto.StartDate, dto.EndDate);
            return Ok();
        }
    }
}
