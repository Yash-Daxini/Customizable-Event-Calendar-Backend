using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using NSubstitute;
using Core.Entities.RecurrecePattern;
using FluentAssertions;
using UnitTests.Builders;

namespace UnitTests.ApplicationCore.Services.EventServiceTests;

public class AddEvent
{
    private readonly IEventRepository _eventRepository;

    private readonly IEventCollaboratorService _eventCollaboratorService;
    private readonly IOverlappingEventService _overlappingEventService;
    private readonly ISharedCalendarService _sharedCalendarService;
    private readonly IEventService _eventService;
    private readonly List<Event> _events;

    public AddEvent()
    {
        _eventRepository = Substitute.For<IEventRepository>();
        _eventCollaboratorService = Substitute.For<IEventCollaboratorService>();
        _overlappingEventService = Substitute.For<IOverlappingEventService>();
        _sharedCalendarService = Substitute.For<ISharedCalendarService>();
        _eventService = new EventService(_eventRepository, _eventCollaboratorService, _overlappingEventService, _sharedCalendarService);

        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(0)
                                                     .WithOrganizer(new UserBuilder(49).Build(), new DateOnly(2024, 5, 31))
                                                     .Build();

        _events =
        [
            new EventBuilder()
            .WithEventCollaborators(eventCollaborators)
            .Build()
        ];
    }

    [Fact]
    public async Task Should_Return_AddedEventId_When_EventNotOverlaps()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(0)
                                                     .WithOrganizer(new UserBuilder(49).Build(), new DateOnly(2024, 5, 31))
                                                     .Build();

        Event eventObj = new EventBuilder()
                         .WithRecurrencePattern(new DailyRecurrencePattern())
                         .WithEventCollaborators(eventCollaborators)
                         .Build();

        _eventService.GetAllEventsByUserId(48).Returns(_events);

        _eventRepository.Add(eventObj).Returns(1);

        int id = await _eventService.AddEvent(eventObj, 48);

        id.Should().Be(1);

        await _eventRepository.Received().Add(eventObj);

        _overlappingEventService.ReceivedWithAnyArgs().CheckOverlap(eventObj, _events);
    }

    [Fact]
    public async Task Should_Throw_EventOverlapException_When_EventOverlaps()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(0)
                                                     .WithOrganizer(new UserBuilder(49).Build(), new DateOnly(2024, 5, 31))
                                                     .Build();

        Event eventObj = new EventBuilder()
                         .WithRecurrencePattern(new DailyRecurrencePattern())
                         .WithEventCollaborators(eventCollaborators)
                         .Build();

        _eventService.GetAllEventsByUserId(48).Returns(_events);

        _overlappingEventService.WhenForAnyArgs(e => e.CheckOverlap(eventObj, _events))
                                .Do(e => { throw new EventOverlapException("Overlaps"); });

        var action = async () => await _eventService.AddEvent(eventObj, 48);

        await action.Should().ThrowAsync<EventOverlapException>();
    }

    [Fact]
    public async Task Should_Throw_NullArgumentException_When_EventIsNull()
    {
        Event eventObj = null;

        _eventService.GetAllEventsByUserId(48).Returns(_events);

        var action = async () => await _eventService.AddEvent(eventObj, 48);

        await action.Should().ThrowAsync<NullArgumentException>();

        await _eventRepository.DidNotReceive().Add(eventObj);

        _overlappingEventService.DidNotReceive().CheckOverlap(eventObj, _events);
    }
}
