namespace Melone.Currencies.Application.Contracts.Persistence;

using Melone.Currencies.Domain.Entities;
public interface ICurrencyRateRepository
{
    Task AddRangeAsync(IEnumerable<Currency> currencies, CancellationToken cancellationToken = default);

    Task<bool> ExistsForDateAsync(DateTime date, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Currency>> GetLatestRatesAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Currency>> GetHistoryAsync(
        string code, 
        DateTime fromDate, 
        DateTime toDate, 
        CancellationToken cancellationToken = default);
}
