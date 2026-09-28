namespace Melone.Currencies.Infrastructure.Services.Holidays;
    public interface IPolishHolidayCalendar
    {
    bool IsPublicHoliday(DateOnly date);
    bool IsBusinessDay(DateOnly date);
    }

