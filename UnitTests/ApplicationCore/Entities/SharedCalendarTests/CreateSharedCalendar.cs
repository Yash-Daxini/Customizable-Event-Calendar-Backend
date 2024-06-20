using Core.Entities;
using FluentAssertions;

namespace UnitTests.ApplicationCore.Entities.SharedCalendarTests;

public class CreateSharedCalendar
{
    [Fact]
    public void Should_ThrowException_When_InvalidObjectOfSharedCalendarCreated()
    {
        Action action = () => new SharedCalendar(0, null, null, new(2024, 1, 2), new(2024, 1, 1));

        action.Should().Throw<ArgumentException>();
    }
}
