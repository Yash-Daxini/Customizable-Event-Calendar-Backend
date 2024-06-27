using Core.Entities;
using Core.Services;
using FluentAssertions;

namespace UnitTests.ApplicationCore.Services.MutualTimeCalculatorServiceTests;

public class GetMaximumMutualTimeBlockTest
{
    [Fact]
    public void Should_Return_MutualDuration_WhenMaxValueIsPresent()
    {
        var proposedHours = new[] { 0, 2, 2, 1, 0, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        var eventObj = new Event { Duration = new Duration(0, 3) };

        var result = MutualTimeCalculatorService.GetMaximumMutualTimeBlock(proposedHours, eventObj);

        result.StartHour.Should().Be(5);
        result.EndHour.Should().Be(8);
    }

    [Fact]
    public void Should_Return_MutualDuration_WhenMaxValueIsOneOrLess()
    {
        var proposedHours = new[] { 0, 1, 1, 1, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; ;
        var eventObj = new Event { Duration = new Duration(2, 5) };

        var result = MutualTimeCalculatorService.GetMaximumMutualTimeBlock(proposedHours, eventObj);

        result.StartHour.Should().Be(2);
        result.EndHour.Should().Be(5);
    }

    [Fact]
    public void Should_Return_MutualDuration_WhenMaxValueIsNotPresent()
    {
        var proposedHours = new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        var eventObj = new Event { Duration = new Duration(1, 4) };

        var result = MutualTimeCalculatorService.GetMaximumMutualTimeBlock(proposedHours, eventObj);

        result.StartHour.Should().Be(1);
        result.EndHour.Should().Be(4);
    }

    [Fact]
    public void Should_Return_MutualDuration_WhenMaxBlockIsShorterThanEventDuration()
    {
        var proposedHours = new[] { 2, 2, 2, 1, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; ;
        var eventObj = new Event { Duration = new Duration(1, 5) };

        var result = MutualTimeCalculatorService.GetMaximumMutualTimeBlock(proposedHours, eventObj);

        result.StartHour.Should().Be(0);
        result.EndHour.Should().Be(3);
    }
}
