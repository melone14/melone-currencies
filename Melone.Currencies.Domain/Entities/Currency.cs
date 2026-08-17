namespace Melone.Currencies.Domain.Entities;

public class Currency
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public DateTime EffectiveDate { get; set; }
}
