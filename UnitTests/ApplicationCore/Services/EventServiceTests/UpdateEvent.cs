﻿using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using NSubstitute;
using FluentAssertions;
using Core.Entities.RecurrecePattern;
using Core.Entities.Enums;
using UnitTests.Builders.EntityBuilder;

namespace UnitTests.ApplicationCore.Services.EventServiceTests;

public class UpdateEvent
{
    private readonly IEventRepository _eventRepository;

    private readonly IEventCollaboratorService _eventCollaboratorService;
    private readonly IOverlappingEventService _overlappingEventService;
    private readonly ISharedCalendarService _sharedCalendarService;
    private readonly IEventService _eventService;
    private readonly List<Event> _events;

    public UpdateEvent()
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
    public async Task Should_UpdateEvent_When_EventNotOverlaps()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(0)
                                             .WithOrganizer(new UserBuilder(48).Build(), new DateOnly(2024, 5, 31))
                                             .WithParticipant(new UserBuilder(49).Build(),
                                                              ConfirmationStatus.Accept,
                                                              new DateOnly(2024, 5, 31),
                                                              null)
                                             .Build();

        Event eventObj = new EventBuilder()
                         .WithId(1)
                         .WithRecurrencePattern(new DailyRecurrencePattern())
                         .WithEventCollaborators(eventCollaborators)
                         .Build();

        _eventService.GetAllEventsByUserId(48).Returns(_events);

        _eventService.GetEventById(1, 48).Returns(eventObj);

        await _eventService.UpdateEvent(eventObj, 48);

        await _eventRepository.Received().Update(eventObj);

        _overlappingEventService.ReceivedWithAnyArgs().CheckOverlap(eventObj, _events);
    }

    [Fact]
    public async Task Should_Throw_EventOverlapException_When_EventOverlaps()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(0)
                                             .WithOrganizer(new UserBuilder(48).Build(), new DateOnly(2024, 5, 31))
                                             .WithParticipant(new UserBuilder(49).Build(),
                                                              ConfirmationStatus.Accept,
                                                              new DateOnly(2024, 5, 31),
                                                              null)
                                             .Build();

        Event eventObj = new EventBuilder()
                         .WithId(1)
                         .WithRecurrencePattern(new DailyRecurrencePattern())
                         .WithEventCollaborators(eventCollaborators)
                         .Build();

        _eventService.GetAllEventsByUserId(48).Returns(_events);

        _eventService.GetEventById(1, 48).Returns(eventObj);

        _overlappingEventService.WhenForAnyArgs(e => e.CheckOverlap(eventObj, _events))
                     .Do(e => { throw new EventOverlapException("Overlaps"); });

        var action = async () => await _eventService.UpdateEvent(eventObj, 48);

        await action.Should().ThrowAsync<EventOverlapException>();

        await _eventRepository.DidNotReceive().Update(eventObj);

        _overlappingEventService.ReceivedWithAnyArgs().CheckOverlap(eventObj, _events);
    }

    [Fact]
    public async Task Should_Throw_NullArgumentException_When_EventIsNull()
    {
        Event eventObj = null;

        _eventService.GetAllEventsByUserId(48).Returns(_events);

        _eventRepository.Add(eventObj).Returns(1);

        var action = async () => await _eventService.UpdateEvent(eventObj, 48);

        await action.Should().ThrowAsync<NullArgumentException>();

        await _eventRepository.DidNotReceive().Add(eventObj);

        _overlappingEventService.DidNotReceive().CheckOverlap(eventObj, _events);
    }
}
