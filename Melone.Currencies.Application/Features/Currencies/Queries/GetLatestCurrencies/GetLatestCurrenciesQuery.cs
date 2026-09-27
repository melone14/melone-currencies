using MediatR;
using Melone.Currencies.Application.Common.Models;

namespace Melone.Currencies.Application.Features.Currencies.Queries.GetLatestCurrencies;

public record GetLatestCurrenciesQuery(int PageNumber = 1, int PageSize = 12) : IRequest<PagedResult<CurrencyDto>>;