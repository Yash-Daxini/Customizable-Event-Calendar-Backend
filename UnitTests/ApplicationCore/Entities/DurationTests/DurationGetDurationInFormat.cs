using Core.Entities;

namespace UnitTests.ApplicationCore.Entities.DurationTests;

public class DurationGetDurationInFormat
{
    [Theory]
    [InlineData(-5, 25)]
    public void GetNullIfInvalidStartHourInvalidEndHour(int startHour, int endHour)
    {
        Duration duration = new Duration
        {
            StartHour = startHour,
            EndHour = endHour
        };

        string? result = duration.GetDurationInFormat();

        Assert.Equal(result, null);
    }

    [Theory]
    [InlineData(-5, 23)]
    public void GetNullIfInvalidStartHourValidEndHour(int startHour, int endHour)
    {
        Duration duration = new Duration
        {
            StartHour = startHour,
            EndHour = endHour
        };

        string? result = duration.GetDurationInFormat();

        Assert.Equal(result, null);
    }

    [Theory]
    [InlineData(5, 25)]
    public void GetNullIfValidStartHourInvalidEndHour(int startHour, int endHour)
    {
        Duration duration = new Duration
        {
            StartHour = startHour,
            EndHour = endHour
        };

        string? result = duration.GetDurationInFormat();

        Assert.Equal(result, null);
    }

    [Theory]
    [InlineData(5, 23)]
    [InlineData(1, 23)]
    [InlineData(0, 23)]
    [InlineData(23, 1)]
    [InlineData(23, 0)]
    public void GetSpaceSeparatedStartHourAndEndHourIfValidStartHourValidEndHour(int startHour, int endHour)
    {
        Duration duration = new Duration
        {
            StartHour = startHour,
            EndHour = endHour
        };

        string? result = duration.GetDurationInFormat();

        if(startHour == 5 && endHour == 23)
            Assert.Equal(result, "5 AM - 11 PM");
        else if(startHour == 1 && endHour == 23)
            Assert.Equal(result, "1 AM - 11 PM");
        else if (startHour == 0 && endHour == 23)
            Assert.Equal(result, "12 AM - 11 PM");
        else if (startHour == 23 && endHour == 1)
            Assert.Equal(result, "11 PM - 1 AM");
        else if (startHour == 23 && endHour == 0)
            Assert.Equal(result, "11 PM - 12 AM");

    }
}
