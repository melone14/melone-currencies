using MediatR;
using Melone.Currencies.Application.Contracts.Persistence;

namespace Melone.Currencies.Application.Features.Currencies.Queries.GetLatestCurrencies;

internal class GetLatestCurrenciesQueryHandler : IRequestHandler<GetLatestCurrenciesQuery, List<CurrencyDto>>
{
    private readonly ICurrencyRateRepository _currencyRateRepository;
    public GetLatestCurrenciesQueryHandler(ICurrencyRateRepository repository)
    {
        _currencyRateRepository = repository;
    }

    public async Task<List<CurrencyDto>> Handle(GetLatestCurrenciesQuery request, CancellationToken cancellationToken)
    {
        var currencies = await _currencyRateRepository.GetLatestRatesAsync(cancellationToken);

        return currencies.Select(c => new CurrencyDto(
            c.Name,
            c.Code,
            c.Value,
            c.EffectiveDate 
            )).ToList();
    }
}
