using Core.Extensions;
using FluentAssertions;

namespace UnitTests.ApplicationCore.Extensions.DateTimeUtillsTests;

public class GetMaxDate
{
    [Theory]
    [InlineData("2024-2-29", 31, "2024-2-29")]
    [InlineData("2024-2-29", 30, "2024-2-29")]
    [InlineData("2023-2-28", 31, "2023-2-28")]
    [InlineData("2023-2-28", 30, "2023-2-28")]
    [InlineData("2024-3-29", 31, "2024-3-31")]
    [InlineData("2024-4-29", 31, "2024-4-30")]
    public void Should_Return_MaximumPossibleDate_When_GivenMonthIsNull(string dateString, int monthDay, string expectedResultString)
    {
        DateOnly date = DateOnly.Parse(dateString);
        DateOnly expected = DateOnly.Parse(expectedResultString);

        var actualResult = date.GetMaxDate(monthDay, null);

        actualResult.Should().Be(expected);
    }

    [Theory]
    [InlineData("2024-2-29", 31, 3, "2024-3-31")]
    [InlineData("2024-3-29", 30, 2, "2024-2-29")]
    [InlineData("2023-4-28", 31, 2, "2023-2-28")]
    [InlineData("2023-10-28", 30, 2, "2023-2-28")]
    [InlineData("2024-5-29", 31, 3, "2024-3-31")]
    [InlineData("2024-8-29", 31, 4, "2024-4-30")]
    public void Should_Return_MaximumPossibleDate_When_GivenMonthIsNotNull(string dateString, int monthDay, int month, string expectedResultString)
    {
        DateOnly date = DateOnly.Parse(dateString);
        DateOnly expected = DateOnly.Parse(expectedResultString);

        var actualResult = date.GetMaxDate(monthDay, month);

        actualResult.Should().Be(expected);
    }
}
