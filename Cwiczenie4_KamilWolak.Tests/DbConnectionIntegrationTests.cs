using Cwiczenie4_KamilWolak.Infrastructure.DbConnection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Cwiczenie4_KamilWolak.UnitTests;

public class DbConnectionIntegrationTests
{
    private readonly IConfiguration _configuration;
    private CurrencyDbContext _currencyDbContext;


    public DbConnectionIntegrationTests()
    {
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) 
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        _configuration = configurationBuilder.Build();

    }

    [Fact]
    public async Task CheckWithValidConnectionStringShouldConnect()
    {
        var options = new DbContextOptionsBuilder<CurrencyDbContext>()
            .UseNpgsql(_configuration.GetConnectionString("CurrencyConnectionString"))
            .Options;
        
        _currencyDbContext = new CurrencyDbContext(options);
        
        var canConnect = await _currencyDbContext.Database.CanConnectAsync();
        
        Assert.True(canConnect);
    }
    
    [Fact]
    public async Task CheckWithInvalidConnectionStringShouldConnect()
    {
        var options = new DbContextOptionsBuilder<CurrencyDbContext>()
            .UseNpgsql(_configuration.GetConnectionString("asd"))
            .Options;
        
        _currencyDbContext = new CurrencyDbContext(options);
        
        var canConnect = await _currencyDbContext.Database.CanConnectAsync();
        
        Assert.False(canConnect);
    }
}