using System;
using System.Collections.Generic;
using AwesomeAssertions;
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

    public static IEnumerable<object[]> ToCronDayCases()
    {
        yield return [DayOfWeekType.Monday, "MON"];
        yield return [DayOfWeekType.Tuesday, "TUE"];
        yield return [DayOfWeekType.Wednesday, "WED"];
        yield return [DayOfWeekType.Thursday, "THU"];
        yield return [DayOfWeekType.Friday, "FRI"];
        yield return [DayOfWeekType.Saturday, "SAT"];
        yield return [DayOfWeekType.Sunday, "SUN"];
    }

    [Test]
    [MethodDataSource(nameof(ToCronDayCases))]
    public void ToCronDay_returns_expected_abbreviation(DayOfWeekType day, string expected)
    {
        string result = CronExpressionUtil.ToCronDay(day);
        result.Should().Be(expected);
    }

    [Test]
    public void ToCronDay_invalid_throws_ArgumentOutOfRangeException()
    {
        var invalidDay = (DayOfWeekType)null!;
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => CronExpressionUtil.ToCronDay(invalidDay));
        ex.ParamName.Should().Be("day");
    }
}

