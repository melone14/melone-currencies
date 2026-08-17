using MediatR;
using Melone.Currencies.Application.Contracts.Persistence;

namespace Melone.Currencies.Application.Features.Currencies.Commands.SyncCurrencies;

public class SyncCurrenciesCommandHandler : IRequestHandler<SyncCurrenciesCommand, DateTime?>
{
    private readonly ICurrencyRateRepository _currencyRateRepository;
    public SyncCurrenciesCommandHandler(ICurrencyRateRepository currencyRateRepository)
    {
        _currencyRateRepository = currencyRateRepository;
    }

    public async Task<DateTime?> Handle(SyncCurrenciesCommand request, CancellationToken cancellationToken)
    {
        return null;
    }
}
