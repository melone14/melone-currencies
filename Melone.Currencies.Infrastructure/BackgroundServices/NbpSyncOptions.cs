namespace Melone.Currencies.Infrastructure.BackgroundServices;

    public class NbpSyncOptions
    {
    public const string SectionName = "NbpSync";

    public DayOfWeek PublishDayOfWeek { get; set; } = DayOfWeek.Wednesday;

    public TimeSpan PublishTime { get; set; } = new(12, 15, 0);

    public TimeSpan RetryInterval { get; set; } = TimeSpan.FromMinutes(30);

    public int MaxRetryAttempts { get; set; } = 6;

    public bool RunImmediatelyOnStartup { get; set; } = true;
}

