[![](https://img.shields.io/nuget/v/soenneker.utils.cronexpressions.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.utils.cronexpressions/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.utils.cronexpressions/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.utils.cronexpressions/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.utils.cronexpressions.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.utils.cronexpressions/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.utils.cronexpressions/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.utils.cronexpressions/actions/workflows/codeql.yml)

# Soenneker.Utils.CronExpressions

Static helpers for producing common five-field cron expressions.

## Installation

```bash
dotnet add package Soenneker.Utils.CronExpressions
```

## Usage

```csharp
string everyFiveMinutes = CronExpressionUtil.EveryXMinutes(5); // */5 * * * *
string everySixHours = CronExpressionUtil.EveryXHours(6, minute: 15); // 15 */6 * * *
string daily = CronExpressionUtil.DailyAt(hour: 2, minute: 30); // 30 2 * * *
string weekly = CronExpressionUtil.WeeklyAt(DayOfWeekType.Monday, hour: 9); // 0 9 * * MON
string monthly = CronExpressionUtil.MonthlyAt(dayOfMonth: 1, hour: 8); // 0 8 1 * *
string weekdays = CronExpressionUtil.WeekdaysAt(hour: 7, minute: 45); // 45 7 * * MON-FRI
string weekends = CronExpressionUtil.WeekendsAt(hour: 10); // 0 10 * * SAT,SUN
```

The output order is `minute hour day-of-month month day-of-week`. These expressions do not include a seconds or year field. The scheduler consuming the expression determines its timezone and exact cron dialect, so verify that it accepts standard five-field syntax and named weekdays.

The pattern helpers validate minute, hour, interval, and day-of-month ranges. A monthly expression using day 29, 30, or 31 simply has no occurrence in months that lack that date.

## Custom fields

`Format()` combines numeric minute/hour values with caller-supplied day, month, and weekday fields:

```csharp
string firstQuarter = CronExpressionUtil.Format(
    minute: 0,
    hour: 6,
    dom: "1",
    month: "1,4,7,10",
    dow: "*");
```

`Format()` performs formatting only; it does not validate any field. Use the pattern-specific methods when their validation and semantics fit the schedule.
