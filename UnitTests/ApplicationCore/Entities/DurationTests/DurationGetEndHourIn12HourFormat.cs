using Core.Entities;
using Core.Exceptions;

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
        Assert.Throws<InvalidDurationException>(() =>
        {
            Duration duration = new(hour, hour);

            string? result = duration.GetEndHourIn12HourFormat();
        });
    }

    [Theory]
    [InlineData(2)]
    [InlineData(1)]
    [InlineData(12)]
    [InlineData(13)]
    [InlineData(23)]
    public void Should_ReturnValid12HourFormat_When_Valid24HourFormat(int hour)
    {
        Duration duration = new(0, hour);

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
            case 23:
                actualResult = "11 PM";
                break;
        }

        string? result = duration.GetEndHourIn12HourFormat();

        Assert.Equal(result, actualResult);
    }
}
