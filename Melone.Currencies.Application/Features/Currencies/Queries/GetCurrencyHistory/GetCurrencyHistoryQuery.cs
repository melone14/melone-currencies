using MediatR;

namespace Melone.Currencies.Application.Features.Currencies.Queries.GetCurrencyHistory;

public record GetCurrencyHistoryQuery(string Code, DateTime FromDate, DateTime ToDate) : IRequest<CurrencyHistoryDto?>;
