using Core.Entities;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using NSubstitute;

namespace UnitTests.ApplicationCore.Services.EventServiceTests;

public class GetAllEventsByUserId
{
    private readonly IEventRepository _eventRepository;

    private readonly IEventCollaboratorService _eventCollaboratorService;
    private readonly IOverlappingEventService _overlappingEventService;
    private readonly ISharedCalendarService _sharedCalendarService;
    private readonly IEventService _eventService;

    public GetAllEventsByUserId()
    {
        _eventRepository = Substitute.For<IEventRepository>();
        _eventCollaboratorService = Substitute.For<IEventCollaboratorService>();
        _overlappingEventService = Substitute.For<IOverlappingEventService>();
        _sharedCalendarService = Substitute.For<ISharedCalendarService>();
        _eventService = new EventService(_eventRepository, _eventCollaboratorService, _overlappingEventService, _sharedCalendarService);
    }

    [Fact]
    public async Task Should_Return_ListEventsByUserId_When_UserAvailableWithId()
    {
        List<Event> events = [];

        _eventRepository.GetAllEventsByUserId(48).Returns(events);

        await _eventService.GetAllEventsByUserId(48);

        await _eventRepository.Received().GetAllEventsByUserId(48);
    }
}
