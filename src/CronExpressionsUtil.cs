using Soenneker.Enums.DayOfWeek;
using Soenneker.Utils.PooledStringBuilders;
using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace Soenneker.Utils.CronExpressions;

/// <summary>
/// A utility class for generating CRON expressions for common scheduling patterns.
/// </summary>
public static class CronExpressionUtil
{
    /// <summary>
    /// Creates a CRON expression that triggers every specified number of minutes.
    /// </summary>
    /// <param name="interval">The interval in minutes (1 to 59).</param>
    /// <returns>A CRON expression string.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="interval"/> is less than 1 or greater than 59.</exception>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string EveryXMinutes(int interval)
    {
        if ((uint)(interval - 1) > 58u)
            throw new ArgumentOutOfRangeException(nameof(interval));

        using var psb = new PooledStringBuilder();
        psb.Append("*/");
        psb.Append(interval);
        psb.Append(" * * * *");
        return psb.ToString();
    }

    /// <summary>
    /// Creates a CRON expression that triggers every specified number of hours at a specific minute past the hour.
    /// </summary>
    /// <param name="interval">The interval in hours (1 to 23).</param>
    /// <param name="minute">The minute within the hour to trigger (default is 0).</param>
    /// <returns>A CRON expression string.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="interval"/> is less than 1 or greater than 23.</exception>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string EveryXHours(int interval, int minute = 0)
    {
        if ((uint)(interval - 1) > 22u)
            throw new ArgumentOutOfRangeException(nameof(interval));

        if ((uint)minute > 59u)
            throw new ArgumentOutOfRangeException(nameof(minute));

        using var psb = new PooledStringBuilder();
        psb.Append(minute);
        psb.Append(" */");
        psb.Append(interval);
        psb.Append(" * * *");
        return psb.ToString();
    }

    /// <summary>
    /// Creates a CRON expression that triggers daily at a specific hour and minute.
    /// </summary>
    /// <param name="hour">The hour of the day (0 to 23).</param>
    /// <param name="minute">The minute of the hour (default is 0).</param>
    /// <returns>A CRON expression string.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string DailyAt(int hour, int minute = 0)
    {
        ValidateHourMinute(hour, minute);
        return Format(minute, hour, "*", "*", "*");
    }

    /// <summary>
    /// Creates a CRON expression that triggers weekly on a specific day at a given time.
    /// </summary>
    /// <param name="day">The day of the week.</param>
    /// <param name="hour">The hour of the day (default is 0).</param>
    /// <param name="minute">The minute of the hour (default is 0).</param>
    /// <returns>A CRON expression string.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string WeeklyAt(DayOfWeekType day, int hour = 0, int minute = 0)
    {
        ValidateHourMinute(hour, minute);
        return Format(minute, hour, "*", "*", ToCronDay(day));
    }

    /// <summary>
    /// Creates a CRON expression that triggers monthly on a specific day at a given time.
    /// </summary>
    /// <param name="dayOfMonth">The day of the month (1 to 31).</param>
    /// <param name="hour">The hour of the day (default is 0).</param>
    /// <param name="minute">The minute of the hour (default is 0).</param>
    /// <returns>A CRON expression string.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="dayOfMonth"/> is less than 1 or greater than 31.</exception>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string MonthlyAt(int dayOfMonth, int hour = 0, int minute = 0)
    {
        if ((uint)(dayOfMonth - 1) > 30u)
            throw new ArgumentOutOfRangeException(nameof(dayOfMonth));

        ValidateHourMinute(hour, minute);

        using var psb = new PooledStringBuilder();
        AppendMinuteHour(psb, minute, hour);
        psb.Append(' ');
        psb.Append(dayOfMonth);
        psb.Append(" * *");
        return psb.ToString();
    }

    /// <summary>
    /// Creates a CRON expression that triggers on weekdays (Monday to Friday) at a given time.
    /// </summary>
    /// <param name="hour">The hour of the day.</param>
    /// <param name="minute">The minute of the hour (default is 0).</param>
    /// <returns>A CRON expression string.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string WeekdaysAt(int hour, int minute = 0)
    {
        ValidateHourMinute(hour, minute);
        return Format(minute, hour, "*", "*", "MON-FRI");
    }

    /// <summary>
    /// Creates a CRON expression that triggers on weekends (Saturday and Sunday) at a given time.
    /// </summary>
    /// <param name="hour">The hour of the day.</param>
    /// <param name="minute">The minute of the hour (default is 0).</param>
    /// <returns>A CRON expression string.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string WeekendsAt(int hour, int minute = 0)
    {
        ValidateHourMinute(hour, minute);
        return Format(minute, hour, "*", "*", "SAT,SUN");
    }

    /// <summary>
    /// Formats a CRON expression string from the given parts.
    /// </summary>
    /// <param name="minute">Minute field.</param>
    /// <param name="hour">Hour field.</param>
    /// <param name="dom">Day of month field.</param>
    /// <param name="month">Month field.</param>
    /// <param name="dow">Day of week field.</param>
    /// <returns>A full CRON expression string.</returns>
    [Pure]
    public static string Format(int minute, int hour, string dom, string month, string dow)
    {
        using var psb = new PooledStringBuilder();

        AppendMinuteHour(psb, minute, hour);
        psb.Append(' ');
        psb.Append(dom);
        psb.Append(' ');
        psb.Append(month);
        psb.Append(' ');
        psb.Append(dow);

        return psb.ToString();
    }

    /// <summary>
    /// Converts a <see cref="DayOfWeekType"/> to a CRON-compatible three-letter day abbreviation (e.g., "MON", "TUE").
    /// </summary>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToCronDay(DayOfWeekType day)
    {
        // Intellenum instances are singletons here, so ReferenceEquals is ideal.
        if (ReferenceEquals(day, DayOfWeekType.Monday))
            return "MON";
        if (ReferenceEquals(day, DayOfWeekType.Tuesday))
            return "TUE";
        if (ReferenceEquals(day, DayOfWeekType.Wednesday))
            return "WED";
        if (ReferenceEquals(day, DayOfWeekType.Thursday))
            return "THU";
        if (ReferenceEquals(day, DayOfWeekType.Friday))
            return "FRI";
        if (ReferenceEquals(day, DayOfWeekType.Saturday))
            return "SAT";
        if (ReferenceEquals(day, DayOfWeekType.Sunday))
            return "SUN";

        throw new ArgumentOutOfRangeException(nameof(day));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void ValidateHourMinute(int hour, int minute)
    {
        if ((uint)hour > 23u)
            throw new ArgumentOutOfRangeException(nameof(hour));

        if ((uint)minute > 59u)
            throw new ArgumentOutOfRangeException(nameof(minute));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void AppendMinuteHour(PooledStringBuilder psb, int minute, int hour)
    {
        psb.Append(minute);
        psb.Append(' ');
        psb.Append(hour);
    }
}