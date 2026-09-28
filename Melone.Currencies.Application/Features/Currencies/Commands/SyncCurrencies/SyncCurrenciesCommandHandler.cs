using MediatR;
using Melone.Currencies.Application.Common.Interfaces;
using Melone.Currencies.Application.Contracts.Persistence;

namespace Melone.Currencies.Application.Features.Currencies.Commands.SyncCurrencies;

public class SyncCurrenciesCommandHandler : IRequestHandler<SyncCurrenciesCommand, DateTime?>
{
    private readonly ICurrencyRateRepository _currencyRateRepository;
    private readonly INbpApiClient _nbpApiClient;

    public SyncCurrenciesCommandHandler(ICurrencyRateRepository currencyRateRepository, INbpApiClient nbpApiClient)
    {
        _currencyRateRepository = currencyRateRepository;
        _nbpApiClient = nbpApiClient;
    }

    public async Task<DateTime?> Handle(SyncCurrenciesCommand request, CancellationToken cancellationToken)
    {
        var currencies = (await _nbpApiClient.GetCurrentRatesAsync(cancellationToken)).ToList();

        if (!currencies.Any())
            return null;

        var effectiveDate = currencies.First().EffectiveDate;

        var alreadyExists = await _currencyRateRepository.ExistsForDateAsync(effectiveDate, cancellationToken);
        if (!alreadyExists)
        {
            await _currencyRateRepository.AddRangeAsync(currencies, cancellationToken);
        }

        return effectiveDate;
    }
}
