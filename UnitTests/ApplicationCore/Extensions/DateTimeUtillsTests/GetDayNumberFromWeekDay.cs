using Core.Extensions;
using FluentAssertions;

namespace UnitTests.ApplicationCore.Extensions.DateTimeUtillsTests;

public class GetDayNumberFromWeekDay
{
    [Theory]
    [InlineData("2024-6-24",1)]
    [InlineData("2024-6-25",2)]
    [InlineData("2024-6-26",3)]
    [InlineData("2024-6-27",4)]
    [InlineData("2024-6-28",5)]
    [InlineData("2024-6-29",6)]
    [InlineData("2024-6-30",7)]
    public void Should_Return_DayNumber_When_DateIsGiven(string date,int expectedResult)
    {
        DateOnly dateOnly = DateOnly.Parse(date);

        int result = dateOnly.GetDayNumberFromWeekDay();

        result.Should().Be(expectedResult);
    }
}
