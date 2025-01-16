using System.Text.Json.Serialization;
using Ćwiczenie4_KamilWolak.Application.Interfaces;
using Ćwiczenie4_KamilWolak.Application.Services;
using Ćwiczenie4_KamilWolak.Domain.Interfaces;
using Ćwiczenie4_KamilWolak.Infrastructure.DbConnection;
using Ćwiczenie4_KamilWolak.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddJsonOptions(option =>
    option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddDbContext<CurrencyDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("CurrencyConnectionString"));
});
builder.Services.AddScoped<IRateRepository, RateRepository>();  
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddScoped<IExchangeTableService, ExchangeTableService>();
builder.Services.AddScoped<IExchangeTableRepository, ExchangeTableRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();