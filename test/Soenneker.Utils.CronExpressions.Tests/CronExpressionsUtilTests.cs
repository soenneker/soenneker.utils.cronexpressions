using System;
using Soenneker.Enums.DayOfWeek;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Utils.CronExpressions.Tests;

[Collection("Collection")]
public class CronExpressionsUtilTests : FixturedUnitTest
{
    public CronExpressionsUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Fact]
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

    [Fact]
    public void ToCronDay_invalid_throws_ArgumentOutOfRangeException()
    {
        var invalidDay = (DayOfWeekType)null!;
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => CronExpressionUtil.ToCronDay(invalidDay));
        Assert.Equal("day", ex.ParamName);
    }
}
