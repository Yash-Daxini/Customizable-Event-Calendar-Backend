using Core.Entities;
using Core.Exceptions;
using FluentAssertions;

namespace UnitTests.ApplicationCore.Entities.DurationTests;

public class DurationGetEndHourIn12HourFormat
{
    [Theory]
    [InlineData(-2)]
    [InlineData(25)]
    [InlineData(24)]
    [InlineData(-4)]
    public void Should_ThrowException_When_Invalid24HourFormat(int hour)
    {
        Action action = () =>
        {
            Duration duration = new(hour, hour);
        };

        action.Should().Throw<InvalidDurationException>();
    }

    [Theory]
    [InlineData(2, "2 AM")]
    [InlineData(1, "1 AM")]
    [InlineData(12, "12 PM")]
    [InlineData(13, "1 PM")]
    [InlineData(23, "11 PM")]
    public void Should_ReturnValid12HourFormat_When_Valid24HourFormat(int hour, string expectedResult)
    {
        Duration duration = new(0, hour);

        string? actualResult = duration.GetEndHourIn12HourFormat();

        actualResult.Should().Be(expectedResult);
    }
}
