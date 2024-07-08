using Core.Entities;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using FluentAssertions;
using Core.Entities.Enums;
using UnitTests.Builders.EntityBuilder;

namespace UnitTests.ApplicationCore.Services.EventServiceTests;

public class GetSharedEvents
{
    private readonly IEventRepository _eventRepository;

    private readonly IEventCollaboratorService _eventCollaboratorService;
    private readonly IOverlappingEventService _overlappingEventService;
    private readonly ISharedCalendarService _sharedCalendarService;
    private readonly IEventService _eventService;
    private readonly List<Event> _events;

    public GetSharedEvents()
    {
        _eventRepository = Substitute.For<IEventRepository>();
        _eventCollaboratorService = Substitute.For<IEventCollaboratorService>();
        _overlappingEventService = Substitute.For<IOverlappingEventService>();
        _sharedCalendarService = Substitute.For<ISharedCalendarService>();
        _eventService = new EventService(_eventRepository, _eventCollaboratorService, _overlappingEventService, _sharedCalendarService);

        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(0)
                                             .WithOrganizer(new UserBuilder(48).Build(), new DateOnly(2024, 5, 31))
                                             .WithParticipant(new UserBuilder(49).Build(),
                                                              ConfirmationStatus.Accept,
                                                              new DateOnly(2024, 5, 31),
                                                              null)
                                             .Build();

        Event eventObj = new EventBuilder()
                       .WithEventCollaborators(eventCollaborators)
                       .Build();

        _events = [eventObj];
    }

    [Fact]
    public async Task Should_Return_ListOfEvent_When_SharedCalendarAvailableWithId()
    {
        SharedCalendar sharedCalendar = new(1, new() { Id = 48, Name = "a", Email = "a@gmail.com", Password = "a" },
                                               new() { Id = 49, Name = "b", Email = "b@gmail.com", Password = "b" },
                                               new DateOnly(),
                                               new DateOnly());

        _sharedCalendarService.GetSharedCalendarById(48).Returns(sharedCalendar);

        _eventRepository.GetEventsWithinGivenDateByUserId(48, sharedCalendar.FromDate, sharedCalendar.ToDate).Returns(_events);

        List<Event> events = await _eventService.GetSharedEvents(48);

        events.Should().BeEquivalentTo(_events);

        await _eventRepository.Received().GetEventsWithinGivenDateByUserId(48, sharedCalendar.FromDate, sharedCalendar.ToDate);
    }

    [Fact]
    public async Task Should_Return_EmptyList_When_SharedCalendarNotAvailableWithId()
    {
        SharedCalendar sharedCalendar = new(1, new() { Id = 1, Name = "a", Email = "a@gmail.com", Password = "a" },
                                               new() { Id = 2, Name = "b", Email = "b@gmail.com", Password = "b" },
                                               new DateOnly(),
                                               new DateOnly());

        _sharedCalendarService.GetSharedCalendarById(48).ReturnsNull();

        _eventRepository.GetEventsWithinGivenDateByUserId(48, sharedCalendar.FromDate, sharedCalendar.ToDate).Returns(_events);

        List<Event> events = await _eventService.GetSharedEvents(48);

        events.Should().BeEmpty();

        await _eventRepository.DidNotReceive().GetEventsWithinGivenDateByUserId(48, sharedCalendar.FromDate, sharedCalendar.ToDate);
    }

    [Fact]
    public async Task Should_Throw_Exception_When_OrganizerOfEventIsNull()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(0)
                                             .WithParticipant(new UserBuilder(49).Build(),
                                                              ConfirmationStatus.Accept,
                                                              new DateOnly(2024, 5, 31),
                                                              null)
                                             .Build();

        Event eventObj = new EventBuilder()
                         .WithEventCollaborators(eventCollaborators)
                         .Build();

        List<Event> events = [eventObj];

        SharedCalendar sharedCalendar = new(1, new() { Id = 1, Name = "a", Email = "a@gmail.com", Password = "a" },
                                               new() { Id = 2, Name = "b", Email = "b@gmail.com", Password = "b" },
                                               new DateOnly(),
                                               new DateOnly());

        _sharedCalendarService.GetSharedCalendarById(48).Returns(sharedCalendar);

        _eventRepository.GetEventsWithinGivenDateByUserId(48, sharedCalendar.FromDate, sharedCalendar.ToDate).Returns(events);

        var action = async () => await _eventService.GetSharedEvents(48);

        await action.Should().ThrowAsync<Exception>();

        await _eventRepository.DidNotReceive().GetEventsWithinGivenDateByUserId(48, sharedCalendar.FromDate, sharedCalendar.ToDate);
    }

    [Fact]
    public async Task Should_Throw_NotFoundException_When_SharedCalendarWithInValidId()
    {
        SharedCalendar sharedCalendar = new(1, new() { Id = 1, Name = "a", Email = "a@gmail.com", Password = "a" },
                                               new() { Id = 2, Name = "b", Email = "b@gmail.com", Password = "b" },
                                               new DateOnly(),
                                               new DateOnly());

        _sharedCalendarService.GetSharedCalendarById(-1).ReturnsNull();

        _eventRepository.GetEventsWithinGivenDateByUserId(48, sharedCalendar.FromDate, sharedCalendar.ToDate).Returns([]);

        List<Event> events = await _eventService.GetSharedEvents(-1);

        events.Should().BeEmpty();
    }
}
