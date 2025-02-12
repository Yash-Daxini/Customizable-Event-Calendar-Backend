﻿using Core.Entities;
using Core.Extensions;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using NSubstitute;
using FluentAssertions;
using Core.Entities.Enums;
using UnitTests.Builders.EntityBuilder;

namespace UnitTests.ApplicationCore.Services.EventServiceTests;

public class GetEventsForWeeklyViewByUserId
{
    private readonly IEventRepository _eventRepository;

    private readonly IEventCollaboratorService _eventCollaboratorService;
    private readonly IOverlappingEventService _overlappingEventService;
    private readonly ISharedCalendarService _sharedCalendarService;
    private readonly IEventService _eventService;
    private readonly List<Event> _events;

    public GetEventsForWeeklyViewByUserId()
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

        _events = [event1, event2];
    }

    [Fact]
    public async Task Should_Return_ListOfEvent_When_UserAvailableWithId()
    {
        DateOnly startDateOfWeek = DateTimeUtills.GetStartDateOfWeek(DateTime.Now);
        DateOnly endDateOfWeek = DateTimeUtills.GetEndDateOfWeek(DateTime.Now);

        _eventRepository.GetEventsWithinGivenDateByUserId(48, startDateOfWeek, endDateOfWeek).ReturnsForAnyArgs(_events);

        List<Event> events = await _eventService.GetEventsForWeeklyViewByUserId(48);

        events.Should().BeEquivalentTo(_events);

        await _eventRepository.Received().GetEventsWithinGivenDateByUserId(48, startDateOfWeek, endDateOfWeek);
    }
}
