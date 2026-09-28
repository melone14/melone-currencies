using Melone.Currencies.Domain.Entities;

namespace Melone.Currencies.Application.Common.Interfaces;

public interface INbpApiClient
{
    Task<IEnumerable<Currency>> GetCurrentRatesAsync(CancellationToken cancellationToken = default);
}