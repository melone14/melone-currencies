using System.Net.Http.Json;
using Melone.Currencies.Application.Common.Interfaces;
using Melone.Currencies.Domain.Entities;
using Melone.Currencies.Infrastructure.Services.NBP.Dtos;
using Microsoft.Extensions.Options;

namespace Melone.Currencies.Infrastructure.Services.NBP;

public class NbpApiClient : INbpApiClient
{
    private readonly HttpClient _httpClient;
    private readonly NbpApiOptions _options;

    public NbpApiClient(HttpClient httpClient, IOptions<NbpApiOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<IEnumerable<Currency>> GetCurrentRatesAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetFromJsonAsync<List<NbpTableDto>>(
            _options.TableBEndpoint,
            cancellationToken
        );

        var table = response?.FirstOrDefault();

        if (table?.Rates is null || table.Rates.Count == 0)
        {
            return Enumerable.Empty<Currency>();
        }

        if (!DateTime.TryParse(table.EffectiveDate, out var effectiveDate))
        {
            effectiveDate = DateTime.UtcNow;
        }

        return table.Rates.Select(r => new Currency
        {
            Id = Guid.NewGuid(),
            Code = r.Code,
            Name = r.Currency,
            Value = r.Mid,
            EffectiveDate = effectiveDate
        }).ToList();
    }
}