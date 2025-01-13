namespace Ćwiczenie4_KamilWolak.Entities;

public class ExchangeTable
{
    public string Table { get; set; }
    public string No { get; set; }
    public string EffectiveDate { get; set; }
    public List<Rate> Rates { get; set; }
}
