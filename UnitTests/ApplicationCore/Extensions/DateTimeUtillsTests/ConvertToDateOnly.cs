using Core.Extensions;
using FluentAssertions;

namespace UnitTests.ApplicationCore.Extensions.DateTimeUtillsTests;

public class ConvertToDateOnly
{
    [Fact]
    public void Should_Return_DateOnly_When_InputIsDateTime()
    {
        DateTime dateTime = DateTime.Now;

        var actualResult = dateTime.ConvertToDateOnly();

        DateOnly expectedResult = new (dateTime.Year, dateTime.Month, dateTime.Day);

        actualResult.Should().Be(expectedResult);
    }
}
