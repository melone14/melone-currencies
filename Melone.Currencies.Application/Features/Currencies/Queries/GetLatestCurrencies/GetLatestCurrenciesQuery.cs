using MediatR;

namespace Melone.Currencies.Application.Features.Currencies.Queries.GetLatestCurrencies;

public record GetLatestCurrenciesQuery : IRequest<List<CurrencyDto>>;
