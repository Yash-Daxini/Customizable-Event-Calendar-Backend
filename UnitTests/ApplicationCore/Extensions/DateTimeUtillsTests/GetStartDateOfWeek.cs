using Core.Extensions;
using FluentAssertions;

namespace UnitTests.ApplicationCore.Extensions.DateTimeUtillsTests;

public class GetStartDateOfWeek
{
    [Theory]
    [InlineData("2024-6-3")]
    [InlineData("2024-6-10")]
    [InlineData("2024-6-17")]
    public void Should_Return_StartDateOfWeek_When_GivenDateIsAlreadyStartDateOfWeek(string date)
    {
        DateTime dateTime = DateTime.Parse(date);

        DateOnly expectedResult = DateOnly.Parse(date);

        DateOnly actualResult = dateTime.GetStartDateOfWeek();

        actualResult.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData("2024-6-5", "2024-6-3")]
    [InlineData("2024-6-16", "2024-6-10")]
    [InlineData("2024-6-23", "2024-6-17")]
    public void Should_Return_StartDateOfWeek_When_GivenDateIsNotStartDateOfWeek(string date, string expectedResultString)
    {
        DateTime dateTime = DateTime.Parse(date);

        DateOnly expectedResult = DateOnly.Parse(expectedResultString);

        DateOnly actualResult = dateTime.GetStartDateOfWeek();

        actualResult.Should().Be(expectedResult);
    }
}
