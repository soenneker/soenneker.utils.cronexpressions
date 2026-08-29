[![](https://img.shields.io/nuget/v/soenneker.utils.cronexpressions.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.utils.cronexpressions/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.utils.cronexpressions/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.utils.cronexpressions/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.utils.cronexpressions.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.utils.cronexpressions/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.utils.cronexpressions/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.utils.cronexpressions/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Utils.CronExpressions
A comprehensive utility class that provides intuitive, validated, and fluent methods for generating dynamic and human-readable cron expressions for scheduling tasks.

## Installation

```bash
dotnet add package Soenneker.Utils.CronExpressions
```

## Quick start

```csharp
using Soenneker.Utils.CronExpressions;
```

Call the static `CronExpressionUtil` methods directly; no dependency-injection registration is required.

## Common operations

- `EveryXMinutes()` - Creates a CRON expression that triggers every specified number of minutes. Returns a CRON expression string.
- `EveryXHours()` - Creates a CRON expression that triggers every specified number of hours at a specific minute past the hour. Returns a CRON expression string.
- `DailyAt()` - Creates a CRON expression that triggers daily at a specific hour and minute. Returns a CRON expression string.
- `WeeklyAt()` - Creates a CRON expression that triggers weekly on a specific day at a given time. Returns a CRON expression string.
- `MonthlyAt()` - Creates a CRON expression that triggers monthly on a specific day at a given time. Returns a CRON expression string.
- `WeekdaysAt()` - Creates a CRON expression that triggers on weekdays (Monday to Friday) at a given time. Returns a CRON expression string.
- `WeekendsAt()` - Creates a CRON expression that triggers on weekends (Saturday and Sunday) at a given time. Returns a CRON expression string.
- `Format()` - Formats a CRON expression string from the given parts. Returns a full CRON expression string.
- `ToCronDay()` - Converts a `DayOfWeekType` to a CRON-compatible three-letter day abbreviation (e.g., "MON", "TUE").
