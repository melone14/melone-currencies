namespace Melone.Currencies.Infrastructure.Services.NBP.Dtos;

public class NbpTableDto
{
    public string Table { get; set; } = string.Empty;
    public string No { get; set; } = string.Empty;
    public string EffectiveDate { get; set; } = string.Empty;
    public List<NbpRateDto> Rates { get; set; } = new();
}

public class NbpRateDto
{
    public string Currency { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public decimal Mid { get; set; }
}