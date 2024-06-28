using Core.Entities;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using NSubstitute;
using FluentAssertions;
using UnitTests.Builders.EntityBuilder;
using Core.Entities.Enums;

namespace UnitTests.ApplicationCore.Services.EventServiceTests;

public class GetAllEventsCreatedByUser
{
    private readonly IEventRepository _eventRepository;

    private readonly IEventCollaboratorService _eventCollaboratorService;
    private readonly IOverlappingEventService _overlappingEventService;
    private readonly ISharedCalendarService _sharedCalendarService;
    private readonly IEventService _eventService;
    private readonly List<Event> _events;

    public GetAllEventsCreatedByUser()
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
                                                                      ConfirmationStatus.Accept,
                                                                      new DateOnly(2024, 5, 31),
                                                                      null)
                                                     .Build();

        Event event2 = new EventBuilder()
                       .WithEventCollaborators(eventCollaborators2)
                       .Build();

        _events =
        [
            event1,
            event2
        ];
    }

    [Theory]
    [InlineData(49)]
    public async Task Should_Return_ListOfEvents_When_UserAvailableWithId(int userId)
    {
        _eventRepository.GetAllEventsByUserId(userId).Returns(_events);

        List<Event> events = await _eventService.GetAllEventCreatedByUser(userId);

        List<EventCollaborator> eventCollaborators2 = new EventCollaboratorListBuilder(0)
                                                     .WithOrganizer(new UserBuilder(49).Build(), new DateOnly(2024, 5, 31))
                                                     .WithParticipant(new UserBuilder(48).Build(),
                                                                      ConfirmationStatus.Accept,
                                                                      new DateOnly(2024, 5, 31),
                                                                      null)
                                                     .Build();

        Event event2 = new EventBuilder()
                       .WithEventCollaborators(eventCollaborators2)
                       .Build();

        List<Event> expected = [event2];

        events.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [InlineData(50)]
    public async Task Should_Return_EmptyList_When_UserNotAvailableWithId(int userId)
    {
        _eventRepository.GetAllEventsByUserId(userId).Returns(_events);

        List<Event> events = await _eventService.GetAllEventCreatedByUser(userId);

        events.Should().BeEmpty();
    }

    [Theory]
    [InlineData(50)]
    public async Task Should_Return_EmptyList_When_OrganizerNotPresentInEvent(int userId)
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(0)
                                                     .WithParticipant(new UserBuilder(48).Build(),
                                                                      ConfirmationStatus.Accept,
                                                                      new DateOnly(2024, 5, 31),
                                                                      null)
                                                     .Build();

        Event @event = new EventBuilder()
                       .WithEventCollaborators(eventCollaborators)
                       .Build();

        List<Event> events = [@event];

        _eventRepository.GetAllEventsByUserId(userId).Returns(events);

        List<Event> actualResult = await _eventService.GetAllEventCreatedByUser(userId);

        actualResult.Should().BeEmpty();
    }
}
