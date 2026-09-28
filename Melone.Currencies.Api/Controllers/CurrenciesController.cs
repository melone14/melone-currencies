using MediatR;
using Melone.Currencies.Application.Features.Currencies.Commands.SyncCurrencies;
using Melone.Currencies.Application.Features.Currencies.Queries.GetCurrencyHistory;
using Melone.Currencies.Application.Features.Currencies.Queries.GetLatestCurrencies;
using Microsoft.AspNetCore.Mvc;

namespace Melone.Currencies.Api.Controllers;

[ApiController]
[Route("api/currencies")]
public class CurrenciesController : ControllerBase
{
    private readonly ISender _mediator;

    public CurrenciesController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("sync")]
    public async Task<IActionResult> Sync(CancellationToken cancellationToken)
    {
        var effectiveDate = await _mediator.Send(new SyncCurrenciesCommand(), cancellationToken);

        if (effectiveDate == null)
            return BadRequest("Nie udało się pobrać kursów z NBP.");

        return Ok(new { Message = "Kursy zsynchronizowane pomyślnie.", EffectiveDate = effectiveDate });
    }

    [HttpGet("latest")]
    public async Task<IActionResult> GetLatest(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 50,
        CancellationToken cancellationToken = default)
    {
        pageNumber = Math.Max(pageNumber, 1);
        pageSize = Math.Clamp(pageSize, 1, 500);

        var currencies = await _mediator.Send(new GetLatestCurrenciesQuery(pageNumber, pageSize), cancellationToken);
        return Ok(currencies);
    }

    [HttpGet("history/{code}")]
    public async Task<IActionResult> GetHistory(
        [FromRoute] string code,
        [FromQuery] DateTime? fromDate,
        [FromQuery] DateTime? toDate,
        CancellationToken cancellationToken = default)
    {
        var start = fromDate ?? DateTime.Today.AddDays(-30);
        var end = toDate ?? DateTime.Today;

        var history = await _mediator.Send(new GetCurrencyHistoryQuery(code, start, end), cancellationToken);
        return Ok(history);
    }
}
