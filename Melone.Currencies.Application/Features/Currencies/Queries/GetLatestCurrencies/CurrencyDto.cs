namespace Melone.Currencies.Application.Features.Currencies.Queries.GetLatestCurrencies;

public record CurrencyDto(
    string Name,
    string Code,
    decimal Value,
    DateTime EffectiveDate
);