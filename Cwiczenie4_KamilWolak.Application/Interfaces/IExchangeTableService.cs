namespace Cwiczenie4_KamilWolak.Application.Interfaces;

public interface IExchangeTableService
{
    Task AddExchangeTables(DateTime startDate, DateTime endDate);
}