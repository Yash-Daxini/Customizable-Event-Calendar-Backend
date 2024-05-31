using Core.Entities;

namespace UnitTests.ApplicationCore.Entities.DurationTests;

public class DurationIsOverlappingWith
{
    [Fact]
    public void ReturnFalseIfDurationIsNull()
    {
        Duration firstDuration = new()
        {
            StartHour = 5,
            EndHour = 6
        };

        Duration secondDuration = null;

        bool result = firstDuration.IsOverlappingWith(secondDuration);

        Assert.False(result);
    }

    [Theory]
    [InlineData(5, 6, 6, 7)]
    [InlineData(4, 5, 3, 4)]
    [InlineData(4, 8, 10, 12)]
    public void ReturnFalseIfDurationNotOverlaps(int firstStartHour, int firstEndHour, int secondStartHour, int secondEndHour)
    {
        Duration firstDuration = new()
        {
            StartHour = firstStartHour,
            EndHour = firstEndHour
        };

        Duration secondDuration = new()
        {
            StartHour = secondStartHour,
            EndHour = secondEndHour
        };

        bool result = firstDuration.IsOverlappingWith(secondDuration);

        Assert.False(result);
    }

    [Theory]
    [InlineData(5, 6, 5, 6)]
    public void ReturnTrueIfDurationIsSame(int firstStartHour, int firstEndHour, int secondStartHour, int secondEndHour)
    {
        Duration firstDuration = new()
        {
            StartHour = firstStartHour,
            EndHour = firstEndHour
        };

        Duration secondDuration = new()
        {
            StartHour = secondStartHour,
            EndHour = secondEndHour
        };

        bool result = firstDuration.IsOverlappingWith(secondDuration);

        Assert.True(result);
    }

    [Theory]
    [InlineData(5, 6, 5, 7)]
    public void ReturnTrueIfStartHoursAreSame(int firstStartHour, int firstEndHour, int secondStartHour, int secondEndHour)
    {
        Duration firstDuration = new()
        {
            StartHour = firstStartHour,
            EndHour = firstEndHour
        };

        Duration secondDuration = new()
        {
            StartHour = secondStartHour,
            EndHour = secondEndHour
        };

        bool result = firstDuration.IsOverlappingWith(secondDuration);

        Assert.True(result);

    }

    [Theory]
    [InlineData(4, 6, 5, 6)]
    public void ReturnTrueIfEndHoursAreSame(int firstStartHour, int firstEndHour, int secondStartHour, int secondEndHour)
    {
        Duration firstDuration = new()
        {
            StartHour = firstStartHour,
            EndHour = firstEndHour
        };

        Duration secondDuration = new()
        {
            StartHour = secondStartHour,
            EndHour = secondEndHour
        };

        bool result = firstDuration.IsOverlappingWith(secondDuration);

        Assert.True(result);
    }

    [Theory]
    [InlineData(5, 6, 4, 8)]
    [InlineData(5, 6, 4, 7)]
    [InlineData(5, 8, 4, 7)]
    [InlineData(10, 12, 4, 11)]
    public void ReturnTrueIfOverlaps(int firstStartHour, int firstEndHour, int secondStartHour, int secondEndHour)
    {
        Duration firstDuration = new()
        {
            StartHour = firstStartHour,
            EndHour = firstEndHour
        };

        Duration secondDuration = new()
        {
            StartHour = secondStartHour,
            EndHour = secondEndHour
        };

        bool result = firstDuration.IsOverlappingWith(secondDuration);

        Assert.True(result);
    }
}