using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using NSubstitute;
using NullArgumentException = Core.Exceptions.NullArgumentException;

namespace UnitTests.ApplicationCore.Services.SharedCalendarServiceTests;

public class AddSharedCalendar
{
    private readonly ISharedCalendarService _sharedCalendarService;

    private readonly ISharedCalendarRepository _sharedCalendarRepository;

    public AddSharedCalendar()
    {
        _sharedCalendarRepository = Substitute.For<ISharedCalendarRepository>();
        _sharedCalendarService = new SharedCalendarService(_sharedCalendarRepository);
    }

    [Fact]
    public async Task Should_ReturnsAddedSharedCalendarId_When_CallsTheMethod()
    {
        SharedCalendar sharedCalendar = new()
        {
            Id = 1,
            Sender = new()
            {
                Id = 1,
                Name = "1",
                Email = "x@gmail.com",
                Password = "1",

            },
            Receiver = new()
            {
                Id = 2,
                Name = "2",
                Email = "y@gmail.com",
                Password = "2",
            },
            FromDate = new DateOnly(2024, 6, 2),
            ToDate = new DateOnly(2024, 6, 20)
        };

        _sharedCalendarRepository.Add(sharedCalendar).Returns(1);

        int id = await _sharedCalendarService.AddSharedCalendar(sharedCalendar);

        Assert.Equal(1, id);

        _sharedCalendarRepository.Received().Add(sharedCalendar);
    }

    [Fact]
    public async Task Should_ThrowException_When_SharedCalendarIsNull()
    {
        SharedCalendar sharedCalendar = null;

        _sharedCalendarRepository.Add(sharedCalendar).Returns(1);

        await Assert.ThrowsAsync<NullArgumentException>(async () => await _sharedCalendarService.AddSharedCalendar(sharedCalendar));

        _sharedCalendarRepository.DidNotReceive().Add(sharedCalendar);
    }
}
