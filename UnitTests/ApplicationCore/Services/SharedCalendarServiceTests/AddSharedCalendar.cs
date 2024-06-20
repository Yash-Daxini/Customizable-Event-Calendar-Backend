using Core.Entities;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using FluentAssertions;
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
        SharedCalendar sharedCalendar = new(1, new User { Id = 1, Name = "a", Email = "a@gmail.com", Password = "a" },
                                               new User { Id = 2, Name = "b", Email = "b@gmail.com", Password = "b" },
                                               new DateOnly(),
                                               new DateOnly()); ;

        _sharedCalendarRepository.Add(sharedCalendar).Returns(1);

        int id = await _sharedCalendarService.AddSharedCalendar(sharedCalendar);

        id.Should().Be(1);

        await _sharedCalendarRepository.Received().Add(sharedCalendar);
    }

    [Fact]
    public async Task Should_ThrowException_When_SharedCalendarIsNull()
    {
        SharedCalendar sharedCalendar = null;

        _sharedCalendarRepository.Add(sharedCalendar).Returns(1);

        Action action = async () => await _sharedCalendarService.AddSharedCalendar(sharedCalendar);

        action.Should().Throw<NullArgumentException>();

        await _sharedCalendarRepository.DidNotReceive().Add(sharedCalendar);
    }
}
