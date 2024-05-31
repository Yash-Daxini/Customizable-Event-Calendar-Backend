using Core.Entities;

namespace UnitTests.ApplicationCore.Entities.DurationTests;

public class DurationGetStartHourIn12HourFormat
{

    [Theory]
    [InlineData(-2)]
    [InlineData(25)]
    [InlineData(24)]
    public void ReturnNullIfInValid24HourFormat(int hour)
    {
        Duration duration = new Duration
        {
            StartHour = hour,
            EndHour = hour,
        };

        string? result = duration.GetStartHourIn12HourFormat();

        Assert.Equal(result, null);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(12)]
    [InlineData(13)]
    [InlineData(23)]
    public void ReturnValid12HourFormatIfValid24HourFormat(int hour)
    {
        Duration duration = new Duration
        {
            StartHour = hour,
            EndHour = hour,
        };

        string actualResult = "";

        switch (hour)
        {
            case 0:
                actualResult = "12 AM";
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

        string? result = duration.GetStartHourIn12HourFormat();

        Assert.Equal(result, actualResult);
    }
}
