using Core.Extensions;
using FluentAssertions;

namespace UnitTests.ApplicationCore.Extensions.DateTimeUtillsTests;

public class GetEndDateOfWeek
{
    [Theory]
    [InlineData("2024-6-9")]
    [InlineData("2024-6-16")]
    [InlineData("2024-6-23")]
    public void Should_Return_EndDateOfWeek_When_GivenDateIsAlreadyEndDateOfWeek(string date)
    {
        DateTime dateTime = DateTime.Parse(date);

        DateOnly actualResult = dateTime.GetEndDateOfWeek();

        actualResult.Should().Be(DateOnly.FromDateTime(dateTime));
    }

    [Theory]
    [InlineData("2024-6-5", "2024-6-9")]
    [InlineData("2024-6-16", "2024-6-16")]
    [InlineData("2024-6-23", "2024-6-23")]
    public void Should_Return_EndDateOfWeek_When_GivenDateIsNotEndDateOfWeek(string date, string expectedResultString)
    {
        DateTime dateTime = DateTime.Parse(date);

        DateOnly expectedResult = DateOnly.Parse(expectedResultString);

        DateOnly actualResult = dateTime.GetEndDateOfWeek();

        actualResult.Should().Be(expectedResult);
    }
}
