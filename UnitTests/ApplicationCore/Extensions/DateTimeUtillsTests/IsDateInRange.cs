using Core.Extensions;
using FluentAssertions;

namespace UnitTests.ApplicationCore.Extensions.DateTimeUtillsTests;

public class IsDateInRange
{
    [Theory]
    [InlineData("2024-6-25", "2024-10-2", "2024-6-30")]
    [InlineData("2024-6-25", "2024-6-26", "2024-6-25")]
    [InlineData("2024-6-25", "2024-6-26", "2024-6-26")]
    public void Should_Return_True_When_DateIsInGivenRange(string startDateString, string endDateString, string dateString)
    {
        DateOnly startDate = DateOnly.Parse(startDateString);
        DateOnly endDate = DateOnly.Parse(endDateString);
        DateOnly date = DateOnly.Parse(dateString);

        bool result = date.IsDateInRange(startDate, endDate);

        result.Should().BeTrue();
    }

    [Theory]
    [InlineData("2024-6-25", "2024-10-2", "2024-10-3")]
    [InlineData("2024-6-25", "2024-6-26", "2024-6-24")]
    [InlineData("2024-6-25", "2024-6-26", "2024-6-27")]
    public void Should_Return_False_When_DateIsNotInGivenRange(string startDateString, string endDateString, string dateString)
    {
        DateOnly startDate = DateOnly.Parse(startDateString);
        DateOnly endDate = DateOnly.Parse(endDateString);
        DateOnly date = DateOnly.Parse(dateString);

        bool result = date.IsDateInRange(startDate, endDate);

        result.Should().BeFalse();
    }
}
