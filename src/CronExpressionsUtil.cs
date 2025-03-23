using System;
using Soenneker.Enums.DayOfWeek;
using Soenneker.Extensions.String;

namespace Soenneker.Utils.CronExpressions;

public static class CronExpressionUtil
{
    public static string EveryXMinutes(int interval)
    {
        if (interval is <= 0 or > 59)
            throw new ArgumentOutOfRangeException(nameof(interval));
        return $"*/{interval} * * * *";
    }

    public static string EveryXHours(int interval, int minute = 0)
    {
        if (interval is <= 0 or > 23)
            throw new ArgumentOutOfRangeException(nameof(interval));

        return $"{minute} */{interval} * * *";
    }

    public static string DailyAt(int hour, int minute = 0) => Format(minute, hour, "*", "*", "*");

    public static string WeeklyAt(DayOfWeekType day, int hour = 0, int minute = 0) => Format(minute, hour, "*", "*", ToCronDay(day));

    public static string MonthlyAt(int dayOfMonth, int hour = 0, int minute = 0)
    {
        if (dayOfMonth is < 1 or > 31)
            throw new ArgumentOutOfRangeException(nameof(dayOfMonth));

        return Format(minute, hour, dayOfMonth.ToString(), "*", "*");
    }

    public static string WeekdaysAt(int hour, int minute = 0) => Format(minute, hour, "*", "*", "MON-FRI");

    public static string WeekendsAt(int hour, int minute = 0) => Format(minute, hour, "*", "*", "SAT,SUN");

    public static string Format(int minute, int hour, string dom, string month, string dow) => $"{minute} {hour} {dom} {month} {dow}";

    public static string ToCronDay(DayOfWeekType day) => day.ToString().ToUpperInvariantFast()[..3];
}