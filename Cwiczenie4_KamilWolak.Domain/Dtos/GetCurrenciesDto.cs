﻿namespace Cwiczenie4_KamilWolak.Domain.Dtos;

public class GetCurrenciesDto
{
    public Guid Id { get; set; }
    public string Currency { get; set; }
    public string Code { get; set; }
    public decimal Mid { get; set; }
    public DateTime EffectiveDate { get; set; }
}