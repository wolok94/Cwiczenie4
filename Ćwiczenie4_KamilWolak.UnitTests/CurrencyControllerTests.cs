using Ćwiczenie4_KamilWolak.Api.Controllers;
using Ćwiczenie4_KamilWolak.Application.Interfaces;
using Ćwiczenie4_KamilWolak.Domain.Dtos;
using Ćwiczenie4_KamilWolak.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Ćwiczenie4_KamilWolak.UnitTests;

public class CurrencyControllerTests
{
    private readonly Mock<ICurrencyService> _currencyService;
    private readonly Mock<IExchangeTableService> _exchangeTableService;


    public CurrencyControllerTests()
    {
        _currencyService = new Mock<ICurrencyService>();
        _exchangeTableService = new Mock<IExchangeTableService>();
        
    }

    [Fact]
    public async Task GetCurrenciesWithValidCurrencies()
    {


        _currencyService.Setup(x => x.GetCurrencies()).ReturnsAsync(new List<CurrencyDto>()
        {
            new CurrencyDto()
            {
                Name = "Złoty"
            }
        });

        var controller = new CurrencyController(_currencyService.Object, _exchangeTableService.Object);
        var result = await controller.Currencies();
        
        Assert.IsType<OkObjectResult>(result);

    }
    
    [Fact]
    public async Task GetCurrenciesWithInValidCurrencies()
    {


        _currencyService.Setup(x => x.GetCurrencies()).ReturnsAsync(null as IEnumerable<CurrencyDto>);

        var controller = new CurrencyController(_currencyService.Object, _exchangeTableService.Object);
        var result = await controller.Currencies();
        
        Assert.IsType<NotFoundResult>(result);

    }

    [Fact]
    public async Task CurrenciesByDateWithValidCurrenciesReturnsOkResult()
    {
        var startDate = new DateTime(2024, 12, 1);
        var endDate = new DateTime(2025, 01, 16);
        var paginationFilter = new PaginationFilterDto(2, 10);
        var getCurrenciesDtos = new List<GetCurrenciesDto>()
        {
            new GetCurrenciesDto()
            {
                Code = "$",
                Currency = "Dolar",
                Mid = 2.2M
                
            }
        };
        var pagination = new PaginationDto<GetCurrenciesDto>(getCurrenciesDtos, paginationFilter.PageSize, paginationFilter.PageNumber, getCurrenciesDtos.Count);
        _currencyService.Setup(x => x.GetCurrenciesByDate(startDate, endDate, paginationFilter))
            .ReturnsAsync(pagination);
        
        var controller = new CurrencyController(_currencyService.Object, _exchangeTableService.Object);
        var result = await controller.CurrenciesByDate(startDate, endDate, paginationFilter);
        
        Assert.IsType<OkObjectResult>(result);
    }
    
    [Fact]
    public async Task CurrenciesByDateWithInValidCurrenciesReturnsBadRequest()
    {
        var startDate = new DateTime(2024, 02, 1);
        var endDate = new DateTime(2024, 01, 31);
        var paginationFilter = new PaginationFilterDto(2, 10);
        var getCurrenciesDtos = new List<GetCurrenciesDto>()
        {
            new GetCurrenciesDto()
            {
                Code = "$",
                Currency = "Dolar",
                Mid = 2.2M
                
            }
        };
        var pagination = new PaginationDto<GetCurrenciesDto>(getCurrenciesDtos, paginationFilter.PageSize, paginationFilter.PageNumber, getCurrenciesDtos.Count);
        _currencyService.Setup(x => x.GetCurrenciesByDate(startDate, endDate, paginationFilter))
            .ReturnsAsync(pagination);
        
        var controller = new CurrencyController(_currencyService.Object, _exchangeTableService.Object);
        var result = await controller.CurrenciesByDate(startDate, endDate, paginationFilter);
        
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task SaveCurrenciesWithValidArgumentsReturnsOkResult()
    {
        var startDate = new DateTime(2024, 02, 1);
        var endDate = new DateTime(2025, 01, 16);
        var fetchCurrenciesDto = new FetchCurrenciesDto()
        {
            StartDate = startDate,
            EndDate = endDate,
        };
        _exchangeTableService.Setup(x => x.AddExchangeTables(startDate, endDate)).Returns(Task.CompletedTask);
        
        var controller = new CurrencyController(_currencyService.Object, _exchangeTableService.Object);
        var result = await controller.SaveCurrencies(fetchCurrenciesDto);
        
        Assert.IsType<OkResult>(result);
    }
    
    [Fact]
    public async Task SaveCurrenciesWithInValidArgumentsReturnsBadRequestResult()
    {
        var startDate = new DateTime(2024, 02, 1);
        var endDate = new DateTime(2024, 01, 16);
        var fetchCurrenciesDto = new FetchCurrenciesDto()
        {
            StartDate = startDate,
            EndDate = endDate,
        };
        _exchangeTableService.Setup(x => x.AddExchangeTables(startDate, endDate)).Returns(Task.CompletedTask);
        
        var controller = new CurrencyController(_currencyService.Object, _exchangeTableService.Object);
        var result = await controller.SaveCurrencies(fetchCurrenciesDto);
        
        Assert.IsType<BadRequestResult>(result);
    }
}