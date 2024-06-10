using Core.Entities;
using Core.Exceptions;

namespace UnitTests.ApplicationCore.Entities.DurationTests;

public class DurationGetDurationInFormat
{
    [Theory]
    [InlineData(-5, 25)]
    [InlineData(-10, -25)]
    public void Should_ThrowException_When_InvalidStartHourAndInValidEndHour(int startHour, int endHour)
    {
        Assert.Throws<InvalidDurationException>(() =>
        {
            Duration duration = new(startHour, endHour);
        });
    }

    [Theory]
    [InlineData(-5, 23)]
    [InlineData(25, 23)]
    public void Should_ThrowException_When_InvalidStartHourAndValidEndHour(int startHour, int endHour)
    {
        Assert.Throws<InvalidDurationException>(() =>
        {
            Duration duration = new(startHour, endHour);
        });
    }

    [Theory]
    [InlineData(5, 25)]
    [InlineData(3, -4)]
    public void Should_ThrowException_When_ValidStartHourAndInvalidEndHour(int startHour, int endHour)
    {
        Assert.Throws<InvalidDurationException>(() =>
        {
            Duration duration = new(startHour, endHour);
        });
    }

    [Theory]
    [InlineData(5, 23)]
    [InlineData(1, 23)]
    [InlineData(0, 23)]
    [InlineData(0, 1)]
    [InlineData(22, 23)]
    public void Should_ReturnFormatedString_When_ValidStartHourAndValidEndHour(int startHour, int endHour)
    {
        Duration duration = new(startHour, endHour);

        string? result = duration.GetDurationInFormat();

        if (startHour == 5 && endHour == 23)
            Assert.Equal("5 AM - 11 PM", result);
        else if (startHour == 1 && endHour == 23)
            Assert.Equal("1 AM - 11 PM", result);
        else if (startHour == 0 && endHour == 23)
            Assert.Equal("12 AM - 11 PM", result);
        else if (startHour == 0 && endHour == 1)
            Assert.Equal("12 AM - 1 AM", result);
        else
            Assert.Equal("10 PM - 11 PM", result);

    }
}
