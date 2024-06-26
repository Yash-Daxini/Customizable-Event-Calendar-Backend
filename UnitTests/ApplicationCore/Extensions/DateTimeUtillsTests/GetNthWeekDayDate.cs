using Core.Extensions;
using FluentAssertions;

namespace UnitTests.ApplicationCore.Extensions.DateTimeUtillsTests;

public class GetNthWeekDayDate
{
    [Theory]
    [InlineData("2024-2-29", 1, DayOfWeek.Sunday, "2024-2-4")]
    [InlineData("2024-3-1", 2, DayOfWeek.Monday, "2024-3-11")]
    [InlineData("2024-5-4", 3, DayOfWeek.Tuesday, "2024-5-21")]
    [InlineData("2024-4-28", 4, DayOfWeek.Wednesday, "2024-4-24")]
    [InlineData("2024-6-10", 5, DayOfWeek.Thursday, "2024-6-27")]
    [InlineData("2024-8-20", 6, DayOfWeek.Friday, "2024-8-30")]
    [InlineData("2024-7-11", 10, DayOfWeek.Saturday, "2024-7-27")]
    public void Should_Return_NthWeekDayDate_When_GivenMonthIsNull(string dateString, int weekOrder, DayOfWeek dayOfWeek, string expectedResultString)
    {
        DateOnly date = DateOnly.Parse(dateString);
        DateOnly expected = DateOnly.Parse(expectedResultString);

        var actualResult = date.GetNthWeekDayDate(weekOrder,dayOfWeek);

        actualResult.Should().Be(expected);
    }
}
