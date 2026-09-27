namespace Melone.Currencies.Infrastructure.Services.Holidays;
    public class PolishHolidayCalendar : IPolishHolidayCalendar
{
    public bool IsBusinessDay(DateOnly date) =>
        date.DayOfWeek is not (DayOfWeek.Saturday or DayOfWeek.Sunday)
        && !IsPublicHoliday(date);

    public bool IsPublicHoliday(DateOnly date)
    {
        var year = date.Year;

        Span<(int Month, int Day)> fixedHolidays =
        [
            (1, 1),   // Nowy Rok
            (1, 6),   // Trzech Króli
            (5, 1),   // Święto Pracy
            (5, 3),   // Święto Konstytucji 3 Maja
            (8, 15),  // Wniebowzięcie NMP
            (11, 1),  // Wszystkich Świętych
            (11, 11), // Święto Niepodległości
            (12, 25), // Boże Narodzenie I
            (12, 26), // Boże Narodzenie II
        ];

        foreach (var (month, day) in fixedHolidays)
        {
            if (date.Month == month && date.Day == day)
                return true;
        }

        var easterSunday = GetEasterSunday(year);

        return date == easterSunday               // Wielkanoc
            || date == easterSunday.AddDays(1)     // Poniedziałek Wielkanocny
            || date == easterSunday.AddDays(49)    // Zielone Świątki
            || date == easterSunday.AddDays(60);   // Boże Ciało
    }

    private static DateOnly GetEasterSunday(int year)
    {
        int a = year % 19;
        int b = year / 100;
        int c = year % 100;
        int d = b / 4;
        int e = b % 4;
        int f = (b + 8) / 25;
        int g = (b - f + 1) / 3;
        int h = (19 * a + b - d - g + 15) % 30;
        int i = c / 4;
        int k = c % 4;
        int l = (32 + 2 * e + 2 * i - h - k) % 7;
        int m = (a + 11 * h + 22 * l) / 451;
        int month = (h + l - 7 * m + 114) / 31;
        int day = (h + l - 7 * m + 114) % 31 + 1;

        return new DateOnly(year, month, day);
    }
}
