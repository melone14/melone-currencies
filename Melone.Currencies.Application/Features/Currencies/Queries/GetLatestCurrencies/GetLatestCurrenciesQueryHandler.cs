using MediatR;
using Melone.Currencies.Application.Common.Models;
using Melone.Currencies.Application.Contracts.Persistence;

namespace Melone.Currencies.Application.Features.Currencies.Queries.GetLatestCurrencies;

internal class GetLatestCurrenciesQueryHandler : IRequestHandler<GetLatestCurrenciesQuery, PagedResult<CurrencyDto>>
{
    private readonly ICurrencyRateRepository _currencyRateRepository;
    public GetLatestCurrenciesQueryHandler(ICurrencyRateRepository repository)
    {
        _currencyRateRepository = repository;
    }

    public async Task<PagedResult<CurrencyDto>> Handle(GetLatestCurrenciesQuery request, CancellationToken cancellationToken)
    {
        var (currencies, totalCount) = await _currencyRateRepository.GetLatestRatesAsync(
            request.PageNumber, request.PageSize, cancellationToken);

        var items = currencies.Select(c => new CurrencyDto(
            c.Name,
            c.Code,
            c.Value,
            c.EffectiveDate
            )).ToList();

        return new PagedResult<CurrencyDto>(items, request.PageNumber, request.PageSize, totalCount);
    }
}