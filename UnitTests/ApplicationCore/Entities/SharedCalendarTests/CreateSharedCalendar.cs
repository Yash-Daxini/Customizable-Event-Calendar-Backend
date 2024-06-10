using Core.Entities;

namespace UnitTests.ApplicationCore.Entities.SharedCalendarTests;

public class CreateSharedCalendar
{
    [Fact]
    public void Should_ThrowException_When_InvalidObjectOfSharedCalendarCreated()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            new SharedCalendar(0, null, null, new(2024, 1, 2), new(2024, 1, 1));
        });
    }
}
