using Core.Entities;
using Core.Exceptions;
using FluentAssertions;

namespace UnitTests.ApplicationCore.Entities.DurationTests;

public class CreateDuration
{
    [Theory]
    [InlineData(-5, 22)]
    [InlineData(25, 4)]

    public void Should_Throw_Exception_When_StartHourIsNotInRange(int startHour, int endHour)
    {
        Action action = () =>
        {
            Duration duration = new(startHour, endHour);
        };

        action.Should().Throw<InvalidDurationException>();
    }

    [Theory]
    [InlineData(5, -22)]
    [InlineData(2, 24)]

    public void Should_Throw_Exception_When_EndHourIsNotInRange(int startHour, int endHour)
    {
        Action action = () =>
        {
            Duration duration = new(startHour, endHour);
        };

        action.Should().Throw<InvalidDurationException>();
    }

    [Theory]
    [InlineData(-5, -22)]
    [InlineData(25, -4)]
    [InlineData(5, 4)]
    [InlineData(23, 0)]

    public void Should_Throw_Exception_When_StartHourAndEndHourAreNotInRange(int startHour, int endHour)
    {
        Action action = () =>
        {
            Duration duration = new(startHour, endHour);
        };

        action.Should().Throw<InvalidDurationException>();
    }

    [Theory]
    [InlineData(5, 22)]
    [InlineData(3, 4)]
    [InlineData(22, 23)]
    [InlineData(0, 1)]

    public void Should_NotThrow_Exception_When_StartHourAndEndHourAreInRange(int startHour, int endHour)
    {
        Duration duration = new(startHour, endHour);

        duration.StartHour.Should().Be(startHour);
        duration.EndHour.Should().Be(endHour);
    }
}
