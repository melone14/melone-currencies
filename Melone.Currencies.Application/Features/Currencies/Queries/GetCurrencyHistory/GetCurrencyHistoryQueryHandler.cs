using MediatR;
using Melone.Currencies.Application.Contracts.Persistence;

namespace Melone.Currencies.Application.Features.Currencies.Queries.GetCurrencyHistory;

internal class GetCurrencyHistoryQueryHandler : IRequestHandler<GetCurrencyHistoryQuery, CurrencyHistoryDto?>
{
    private readonly ICurrencyRateRepository _currencyRateRepository;
    public GetCurrencyHistoryQueryHandler(ICurrencyRateRepository currencyRateRepository)
    {
        _currencyRateRepository = currencyRateRepository;
    }

    public async Task<CurrencyHistoryDto?> Handle(GetCurrencyHistoryQuery request, CancellationToken cancellationToken)
    {
        var history = await _currencyRateRepository.GetHistoryAsync(
            request.Code,
            request.FromDate,
            request.ToDate,
            cancellationToken);

        if (history == null || !history.Any())
        {
            return null;
        }

        var firstItem = history.First();

        var points = history.Select(c => new CurrencyHistoryPointDto(
            c.EffectiveDate,
            c.Value
            )).ToList();

        return new CurrencyHistoryDto(
            firstItem.Code,
            firstItem.Name,
            points
            );
    }
}
