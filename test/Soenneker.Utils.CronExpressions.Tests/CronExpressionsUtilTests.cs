using System;
using Soenneker.Enums.DayOfWeek;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Utils.CronExpressions.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class CronExpressionsUtilTests : HostedUnitTest
{
    public CronExpressionsUtilTests(Host host) : base(host)
    {
    }

    [Test]
    public void Default()
    {

    }

    public static TheoryData<DayOfWeekType, string> ToCronDayCases => new()
    {
        { DayOfWeekType.Monday, "MON" },
        { DayOfWeekType.Tuesday, "TUE" },
        { DayOfWeekType.Wednesday, "WED" },
        { DayOfWeekType.Thursday, "THU" },
        { DayOfWeekType.Friday, "FRI" },
        { DayOfWeekType.Saturday, "SAT" },
        { DayOfWeekType.Sunday, "SUN" },
    };

    [Theory]
    [MemberData(nameof(ToCronDayCases))]
    public void ToCronDay_returns_expected_abbreviation(DayOfWeekType day, string expected)
    {
        string result = CronExpressionUtil.ToCronDay(day);
        Assert.Equal(expected, result);
    }

    [Test]
    public void ToCronDay_invalid_throws_ArgumentOutOfRangeException()
    {
        var invalidDay = (DayOfWeekType)null!;
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => CronExpressionUtil.ToCronDay(invalidDay));
        Assert.Equal("day", ex.ParamName);
    }
}
