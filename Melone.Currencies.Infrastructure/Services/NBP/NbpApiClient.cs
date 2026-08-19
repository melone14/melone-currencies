using System.Net.Http.Json;
using Melone.Currencies.Application.Common.Interfaces;
using Melone.Currencies.Domain.Entities;
using Melone.Currencies.Infrastructure.Services.NBP.Dtos;

namespace Melone.Currencies.Infrastructure.Services.NBP;

public class NbpApiClient : INbpApiClient
{
    private readonly HttpClient _httpClient;

    public NbpApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Currency>> GetCurrentRatesAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetFromJsonAsync<List<NbpTableDto>>(
            "http://api.nbp.pl/api/exchangerates/tables/B?format=json",
            cancellationToken
        );

        var table = response?.FirstOrDefault();
        if (table == null) return Enumerable.Empty<Currency>();

        var publicationDate = DateOnly.Parse(table.EffectiveDate);

        var effectiveDate = DateTime.Parse(table.EffectiveDate);

        return table.Rates.Select(r => new Currency
        {
            Id = Guid.NewGuid(),
            Code = r.Code,
            Name = r.Currency,
            Value = r.Mid,
            EffectiveDate = effectiveDate
        });
    }
}