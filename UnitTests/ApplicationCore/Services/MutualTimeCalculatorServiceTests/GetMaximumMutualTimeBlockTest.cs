using Core.Entities;
using Core.Services;
using FluentAssertions;
using UnitTests.Builders;

namespace UnitTests.ApplicationCore.Services.MutualTimeCalculatorServiceTests;

public class GetMaximumMutualTimeBlockTest
{
    public static IEnumerable<object[]> Data1 =>
        [
            [new List<Duration>{ new(1, 2), new(1, 2), new(1, 2) }, new Duration(1,2)],
            [new List<Duration>{ new(10, 12), new(12, 14), new(14, 16) }, new Duration(1,2)],
            [new List<Duration>{ new(10, 13), new(11, 14), new(10, 16) }, new Duration(11,12)],
            [new List<Duration>{ new(10, 12), new(11, 14), new(10, 16) }, new Duration(11,12)],
            [new List<Duration>{ new(10, 12), new(11, 12), new(10, 11) }, new Duration(10,11)]
        ];

    [Theory, MemberData(nameof(Data1))]
    public void Should_Return_MutualDuration_WhenTimeBlockIs1HourLong(List<Duration> durations, Duration expectedResult)
    {
        var proposedHours = new ProposedHourArrayBuilder()
                                .WithDurations(durations);

        var eventObj = new Event { Duration = new Duration(1, 2) };

        var result = MutualTimeCalculatorService.GetMaximumMutualTimeBlock(proposedHours, eventObj);

        result.StartHour.Should().Be(expectedResult.StartHour);
        result.EndHour.Should().Be(expectedResult.EndHour);
    }

    public static IEnumerable<object[]> Data2 =>
        [
            [new List<Duration>{new (1, 3), new (1, 3), new (1, 3) }, new Duration(1,3)]  ,
            [new List<Duration>{ new (10, 12), new (12, 14), new (14, 16) }, new Duration(1,3)],
            [new List<Duration>{ new (10, 13), new (11, 14), new (10, 16) }, new Duration(11,13)],
            [new List<Duration>{ new(10, 12), new(11, 14), new(10, 16) }, new Duration(11,12)],
            [new List<Duration>{ new(10, 12), new(11, 12), new(10, 11) }, new Duration(10,12)]
        ];

    [Theory, MemberData(nameof(Data2))]
    public void Should_Return_MutualDuration_WhenTimeBlockIs2HourLong(List<Duration> durations, Duration expectedResult)
    {
        var proposedHours = new ProposedHourArrayBuilder()
                                .WithDurations(durations);

        var eventObj = new Event { Duration = new Duration(1, 3) };

        var result = MutualTimeCalculatorService.GetMaximumMutualTimeBlock(proposedHours, eventObj);

        result.StartHour.Should().Be(expectedResult.StartHour);
        result.EndHour.Should().Be(expectedResult.EndHour);
    }

    public static IEnumerable<object[]> Data3 =>
        [
            [new List<Duration>{new (1, 3), new (1, 3), new (1, 3) }, new Duration(1,3)]  ,
            [new List<Duration>{ new (10, 12), new (12, 14), new (14, 16) }, new Duration(1,4)],
            [new List<Duration>{ new (10, 13), new (11, 14), new (10, 16) }, new Duration(11,13)],
            [new List<Duration>{ new (10, 14), new (11, 14), new (10, 16) }, new Duration(11,14)],
            [new List<Duration>{ new(10, 12), new(11, 14), new(10, 16) }, new Duration(11,12)],
            [new List<Duration>{ new(10, 12), new(11, 12), new(10, 11) }, new Duration(10,12)]
        ];

    [Theory, MemberData(nameof(Data3))]
    public void Should_Return_MutualDuration_WhenTimeBlockIs3HourLong(List<Duration> durations, Duration expectedResult)
    {
        var proposedHours = new ProposedHourArrayBuilder()
                                .WithDurations(durations);

        var eventObj = new Event { Duration = new Duration(1, 4) };

        var result = MutualTimeCalculatorService.GetMaximumMutualTimeBlock(proposedHours, eventObj);

        result.StartHour.Should().Be(expectedResult.StartHour);
        result.EndHour.Should().Be(expectedResult.EndHour);
    }
}
