using Core.Entities;
using Core.Exceptions;

namespace UnitTests.ApplicationCore.Entities.DurationTests;

public class DurationGetStartHourIn12HourFormat
{

    [Theory]
    [InlineData(-2)]
    [InlineData(25)]
    [InlineData(24)]
    [InlineData(-1)]
    public void Should_ThrowException_When_Invalid24HourFormat(int hour)
    {
        Assert.Throws<InvalidDurationException>(() =>
        {
            Duration duration = new(hour, hour);
        });
    }

    [Theory]
    [InlineData(2)]
    [InlineData(1)]
    [InlineData(12)]
    [InlineData(13)]
    [InlineData(22)]
    public void Should_ReturnValid12HourFormat_When_Valid24HourFormat(int hour)
    {
        Duration duration = new(hour, 23);

        string actualResult = "";

        switch (hour)
        {
            case 2:
                actualResult = "2 AM";
                break;
            case 1:
                actualResult = "1 AM";
                break;
            case 12:
                actualResult = "12 PM";
                break;
            case 13:
                actualResult = "1 PM";
                break;
            case 22:
                actualResult = "10 PM";
                break;
        }

        string? result = duration.GetStartHourIn12HourFormat();

        Assert.Equal(result, actualResult);
    }
}
