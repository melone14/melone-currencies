using Melone.Currencies.Application.Contracts.Persistence;
using Melone.Currencies.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Melone.Currencies.Persistence.Repositories;

public class CurrencyRateRepository : ICurrencyRateRepository
{
    private readonly MeloneDbContext _dbContext;

    public CurrencyRateRepository(MeloneDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddRangeAsync(IEnumerable<Currency> currencies, CancellationToken cancellationToken = default)
    {
        await _dbContext.Currencies.AddRangeAsync(currencies, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsForDateAsync(DateTime date, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Currencies
            .AnyAsync(c => c.EffectiveDate.Date == date.Date, cancellationToken);
    }

    public async Task<(IReadOnlyList<Currency> Items, int TotalCount)> GetLatestRatesAsync(
     int pageNumber,
     int pageSize,
     CancellationToken cancellationToken = default)
    {
        var latestDate = await _dbContext.Currencies
            .Select(c => c.EffectiveDate)
            .OrderByDescending(d => d)
            .FirstOrDefaultAsync(cancellationToken);

        if (latestDate == default)
        {
            return (new List<Currency>(), 0);
        }

        var query = _dbContext.Currencies
            .Where(c => c.EffectiveDate.Date == latestDate.Date)
            .OrderBy(c => c.Code);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public async Task<IReadOnlyList<Currency>> GetHistoryAsync(
        string code,
        DateTime fromDate,
        DateTime toDate,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Currencies
            .Where(c => c.Code.ToUpper() == code.ToUpper()
                     && c.EffectiveDate.Date >= fromDate.Date
                     && c.EffectiveDate.Date <= toDate.Date)
            .OrderBy(c => c.EffectiveDate)
            .ToListAsync(cancellationToken);
    }
}
