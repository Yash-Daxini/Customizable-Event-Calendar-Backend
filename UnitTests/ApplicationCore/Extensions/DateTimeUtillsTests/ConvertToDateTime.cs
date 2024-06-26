using Core.Extensions;
using FluentAssertions;

namespace UnitTests.ApplicationCore.Extensions.DateTimeUtillsTests;

public class ConvertToDateTime
{
    [Fact]
    public void Should_Return_DateTime_When_InputIsDateOnly()
    {
        DateTime dateTime = DateTime.Now;

        DateOnly dateonly = new (dateTime.Year, dateTime.Month, dateTime.Day);

        DateTime expectedResult = new (dateTime.Year, dateTime.Month, dateTime.Day);

        var actualResult = dateonly.ConvertToDateTime();

        actualResult.Should().Be(expectedResult);
    }
}
