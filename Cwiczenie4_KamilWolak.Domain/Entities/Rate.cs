namespace Cwiczenie4_KamilWolak.Domain.Entities;

public class Rate
{
    public Guid Id { get; set; }
    public string Currency { get; set; }
    public string Code { get; set; }
    public decimal Mid { get; set; }
    public Guid ExchangeTableId { get; set; }
    public ExchangeTable ExchangeTable { get; set; }
}
