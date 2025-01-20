using System.Text.Json.Serialization;
using Cwiczenie4_KamilWolak.Application.Interfaces;
using Cwiczenie4_KamilWolak.Application.Services;
using Cwiczenie4_KamilWolak.Domain.Interfaces;
using Cwiczenie4_KamilWolak.Infrastructure.DbConnection;
using Cwiczenie4_KamilWolak.Infrastructure.Repositories;
using Microsoft.AspNetCore.Cors.Infrastructure;
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
builder.Services.AddCors(x => x.AddPolicy("AllowLocalHost", policy =>
{
    policy.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod();
}));


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CurrencyDbContext>();
    dbContext.Database.Migrate();
}
app.UseCors("AllowLocalHost");
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