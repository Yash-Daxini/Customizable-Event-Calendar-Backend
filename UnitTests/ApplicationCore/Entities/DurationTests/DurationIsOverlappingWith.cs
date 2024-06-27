using Core.Entities;
using FluentAssertions;

namespace UnitTests.ApplicationCore.Entities.DurationTests;

public class DurationIsOverlappingWith
{
    [Fact]
    public void Should_Return_False_When_DurationIsNull()
    {
        Duration firstDuration = new(5, 6);

        Duration secondDuration = null;

        bool result = firstDuration.IsOverlappingWith(secondDuration);

        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(5, 6, 6, 7)]
    [InlineData(4, 5, 3, 4)]
    [InlineData(4, 8, 10, 12)]
    public void Should_Return_False_When_DurationNotOverlaps(int firstStartHour, int firstEndHour, int secondStartHour, int secondEndHour)
    {
        Duration firstDuration = new(firstStartHour, firstEndHour);

        Duration secondDuration = new(secondStartHour, secondEndHour);

        bool result = firstDuration.IsOverlappingWith(secondDuration);

        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(5, 6, 5, 6)]
    public void Should_ReturnTrue_When_DurationIsSame(int firstStartHour, int firstEndHour, int secondStartHour, int secondEndHour)
    {
        Duration firstDuration = new(firstStartHour, firstEndHour);

        Duration secondDuration = new(secondStartHour, secondEndHour);

        bool result = firstDuration.IsOverlappingWith(secondDuration);

        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(5, 6, 5, 7)]
    public void Should_ReturnTrue_When_StartHoursAreSame(int firstStartHour, int firstEndHour, int secondStartHour, int secondEndHour)
    {
        Duration firstDuration = new(firstStartHour, firstEndHour);

        Duration secondDuration = new(secondStartHour, secondEndHour);

        bool result = firstDuration.IsOverlappingWith(secondDuration);

        result.Should().BeTrue();

    }

    [Theory]
    [InlineData(4, 6, 5, 6)]
    public void Should_ReturnTrue_When_EndHoursAreSame(int firstStartHour, int firstEndHour, int secondStartHour, int secondEndHour)
    {
        Duration firstDuration = new(firstStartHour, firstEndHour);

        Duration secondDuration = new(secondStartHour, secondEndHour);

        bool result = firstDuration.IsOverlappingWith(secondDuration);

        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(5, 6, 4, 8)]
    [InlineData(5, 6, 4, 7)]
    [InlineData(5, 8, 4, 7)]
    [InlineData(5, 8, 7, 10)]
    [InlineData(10, 12, 4, 11)]
    public void Should_ReturnTrue_When_HourOverlaps(int firstStartHour, int firstEndHour, int secondStartHour, int secondEndHour)
    {
        Duration firstDuration = new(firstStartHour, firstEndHour);

        Duration secondDuration = new(secondStartHour, secondEndHour);

        bool result = firstDuration.IsOverlappingWith(secondDuration);

        result.Should().BeTrue();
    }
}