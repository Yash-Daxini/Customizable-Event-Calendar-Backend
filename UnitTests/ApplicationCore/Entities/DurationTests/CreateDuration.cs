using Core.Entities;
using Core.Exceptions;

namespace UnitTests.ApplicationCore.Entities.DurationTests;

public class CreateDuration
{
    [Theory]
    [InlineData(-5, 22)]
    [InlineData(25, 4)]

    public void Should_ThrowException_When_StartHourIsNotInRange(int startHour, int endHour)
    {
        Assert.Throws<InvalidDurationException>(() =>
        {
            Duration duration = new(startHour, endHour);
        });
    }

    [Theory]
    [InlineData(5, -22)]
    [InlineData(2, 24)]

    public void Should_ThrowException_When_EndHourIsNotInRange(int startHour, int endHour)
    {
        Assert.Throws<InvalidDurationException>(() =>
        {
            Duration duration = new(startHour, endHour);
        });
    }

    [Theory]
    [InlineData(-5, -22)]
    [InlineData(25, -4)]
    [InlineData(5, 4)]
    [InlineData(23, 0)]

    public void Should_ThrowException_When_StartHourAndEndHourAreNotInRange(int startHour, int endHour)
    {
        Assert.Throws<InvalidDurationException>(() =>
        {
            Duration duration = new(startHour, endHour);
        });
    }

    [Theory]
    [InlineData(5, 22)]
    [InlineData(3, 4)]
    [InlineData(22, 23)]
    [InlineData(0, 1)]

    public void Should_NotThrowException_When_StartHourAndEndHourAreInRange(int startHour, int endHour)
    {
        Duration duration = new(startHour, endHour);
        Assert.True(duration.StartHour == startHour);
        Assert.True(duration.EndHour == endHour);
    }
}
