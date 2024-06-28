using Core.Entities;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using NSubstitute;
using Core.Entities.RecurrecePattern;
using Core.Entities.Enums;
using UnitTests.Builders.EntityBuilder;

namespace UnitTests.ApplicationCore.Services.EventServiceTests;

public class GetProposedEventsByUserId
{
    private readonly IEventRepository _eventRepository;

    private readonly IEventCollaboratorService _eventCollaboratorService;
    private readonly IOverlappingEventService _overlappingEventService;
    private readonly ISharedCalendarService _sharedCalendarService;
    private readonly IEventService _eventService;
    private readonly List<Event> _events;

    public GetProposedEventsByUserId()
    {
        _eventRepository = Substitute.For<IEventRepository>();
        _eventCollaboratorService = Substitute.For<IEventCollaboratorService>();
        _overlappingEventService = Substitute.For<IOverlappingEventService>();
        _sharedCalendarService = Substitute.For<ISharedCalendarService>();
        _eventService = new EventService(_eventRepository, _eventCollaboratorService, _overlappingEventService, _sharedCalendarService);

        List<EventCollaborator> eventCollaborators1 = new EventCollaboratorListBuilder(0)
                                             .WithOrganizer(new UserBuilder(49).Build(), new DateOnly(2024, 5, 31))
                                             .WithParticipant(new UserBuilder(48).Build(),
                                                              ConfirmationStatus.Accept,
                                                              new DateOnly(2024, 5, 31),
                                                              null)
                                             .Build();

        Event event1 = new EventBuilder()
                       .WithEventCollaborators(eventCollaborators1)
                       .Build();

        List<EventCollaborator> eventCollaborators2 = new EventCollaboratorListBuilder(0)
                                             .WithOrganizer(new UserBuilder(48).Build(), new DateOnly(2024, 5, 31))
                                             .WithParticipant(new UserBuilder(49).Build(),
                                                              ConfirmationStatus.Pending,
                                                              new DateOnly(2024, 5, 31),
                                                              null)
                                             .Build();

        Event event2 = new EventBuilder()
                       .WithEventCollaborators(eventCollaborators2)
                       .Build();

        _events = [event1, event2];
    }

    [Fact]
    public async Task Should_Return_ListOfEvent_When_UserAvailableWithId()
    {
        _eventRepository.GetAllEventsByUserId(48).Returns(_events);

        List<Event> events = await _eventService.GetProposedEventsByUserId(48);

        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(0)
                                             .WithOrganizer(new UserBuilder(48).Build(), new DateOnly(2024, 5, 31))
                                             .WithParticipant(new UserBuilder(49).Build(),
                                                              ConfirmationStatus.Pending,
                                                              new DateOnly(2024, 5, 31),
                                                              null)
                                             .Build();

        Event eventObj = new EventBuilder()
                       .WithEventCollaborators(eventCollaborators)
                       .Build();

        List<Event> expected = [eventObj];

        Assert.Equal(expected.Count, events.Count);

        Assert.Equivalent(expected, events);

        await _eventRepository.Received().GetAllEventsByUserId(48);
    }
}
