namespace Melone.Currencies.Application.Features.Currencies.Queries.GetCurrencyHistory;

public record CurrencyHistoryDto(
    string Code,
    string Name,
    List<CurrencyHistoryPointDto> Rates
);

public record CurrencyHistoryPointDto(
    DateTime Date,
    decimal Value
);
