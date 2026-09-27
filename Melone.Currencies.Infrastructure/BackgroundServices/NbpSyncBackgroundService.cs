using MediatR;
using Melone.Currencies.Application.Features.Currencies.Commands.SyncCurrencies;
using Melone.Currencies.Infrastructure.Services.Holidays;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Melone.Currencies.Infrastructure.BackgroundServices;

public class NbpSyncBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IPolishHolidayCalendar _holidayCalendar;
    private readonly NbpSyncOptions _options;
    private readonly ILogger<NbpSyncBackgroundService> _logger;

    public NbpSyncBackgroundService(
        IServiceScopeFactory scopeFactory,
        IPolishHolidayCalendar holidayCalendar,
        IOptions<NbpSyncOptions> options,
        ILogger<NbpSyncBackgroundService> logger)
    {
        _scopeFactory = scopeFactory;
        _holidayCalendar = holidayCalendar;
        _options = options.Value;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (_options.RunImmediatelyOnStartup)
        {
            await TrySyncAsync(stoppingToken);
        }

        while (!stoppingToken.IsCancellationRequested)
        {
            var nextRun = GetNextRunTime(DateTime.Now);
            var delay = nextRun - DateTime.Now;

            _logger.LogInformation(
                "Kolejna synchronizacja kursów NBP zaplanowana na {NextRun}", nextRun);

            if (delay > TimeSpan.Zero && !await SafeDelay(delay, stoppingToken))
                break;

            await TrySyncWithRetriesAsync(stoppingToken);
        }
    }

    private async Task TrySyncWithRetriesAsync(CancellationToken stoppingToken)
    {
        for (var attempt = 1; attempt <= _options.MaxRetryAttempts; attempt++)
        {
            if (stoppingToken.IsCancellationRequested) return;

            if (await TrySyncAsync(stoppingToken))
                return;

            _logger.LogWarning(
                "Tabela B jeszcze niedostępna (próba {Attempt}/{Max}). Ponowna próba za {Interval}.",
                attempt, _options.MaxRetryAttempts, _options.RetryInterval);

            if (!await SafeDelay(_options.RetryInterval, stoppingToken))
                return;
        }

        _logger.LogError(
            "Nie udało się pobrać tabeli B po {Max} próbach - spróbuję ponownie w kolejnym cyklu.",
            _options.MaxRetryAttempts);
    }

    private async Task<bool> TrySyncAsync(CancellationToken stoppingToken)
    {
        try
        {
            using var scope = _scopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

            var effectiveDate = await mediator.Send(new SyncCurrenciesCommand(), stoppingToken);

            if (effectiveDate is null)
                return false;

            _logger.LogInformation("Zsynchronizowano kursy walut z dnia {EffectiveDate}.", effectiveDate);
            return true;
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.LogError(ex, "Błąd podczas synchronizacji kursów walut z NBP.");
            return false;
        }
    }

    private DateTime GetNextRunTime(DateTime from)
    {
        var today = DateOnly.FromDateTime(from);

        if (today.DayOfWeek == _options.PublishDayOfWeek
            && !_holidayCalendar.IsPublicHoliday(today)
            && from.TimeOfDay < _options.PublishTime)
        {
            return today.ToDateTime(TimeOnly.FromTimeSpan(_options.PublishTime));
        }

        var candidate = today.AddDays(1);
        while (candidate.DayOfWeek != _options.PublishDayOfWeek)
        {
            candidate = candidate.AddDays(1);
        }

        while (!_holidayCalendar.IsBusinessDay(candidate))
        {
            candidate = candidate.AddDays(1);
        }

        return candidate.ToDateTime(TimeOnly.FromTimeSpan(_options.PublishTime));
    }

    private static async Task<bool> SafeDelay(TimeSpan delay, CancellationToken stoppingToken)
    {
        try
        {
            await Task.Delay(delay, stoppingToken);
            return true;
        }
        catch (TaskCanceledException)
        {
            return false;
        }
    }
}