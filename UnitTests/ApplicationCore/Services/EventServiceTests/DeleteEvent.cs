using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using FluentAssertions;

namespace UnitTests.ApplicationCore.Services.EventServiceTests;

public class DeleteEvent
{
    private readonly IEventRepository _eventRepository;

    private readonly IEventCollaboratorService _eventCollaboratorService;
    private readonly IOverlappingEventService _overlappingEventService;
    private readonly ISharedCalendarService _sharedCalendarService;
    private readonly IEventService _eventService;

    public DeleteEvent()
    {
        _eventRepository = Substitute.For<IEventRepository>();
        _eventCollaboratorService = Substitute.For<IEventCollaboratorService>();
        _overlappingEventService = Substitute.For<IOverlappingEventService>();
        _sharedCalendarService = Substitute.For<ISharedCalendarService>();
        _eventService = new EventService(_eventRepository, _eventCollaboratorService, _overlappingEventService, _sharedCalendarService);
    }

    [Fact]
    public async Task Should_DeleteEvent_When_EventWithIdAvailable()
    {
        Event eventObj = new();

        _eventRepository.GetEventById(1).Returns(eventObj);

        await _eventService.DeleteEvent(1, 48);

        await _eventRepository.Received().Delete(eventObj);
    }

    [Fact]
    public async Task Should_Throw_NotFoundException_When_EventWithUnavailableId()
    {
        Event eventObj = new();

        _eventRepository.GetEventById(1).ReturnsNull();

        var action = async () => await _eventService.DeleteEvent(1, 48);

        await action.Should().ThrowAsync<NotFoundException>();

        await _eventRepository.DidNotReceive().Delete(eventObj);
    }

    [Fact]
    public async Task Should_Throw_NotFoundException_When_EventWithInValidId()
    {
        Event eventObj = new();

        _eventRepository.GetEventById(-1).ReturnsNull();

        var action = async () => await _eventService.DeleteEvent(-1, 48);

        await action.Should().ThrowAsync<NotFoundException>();

        await _eventRepository.DidNotReceive().Delete(eventObj);
    }
}
