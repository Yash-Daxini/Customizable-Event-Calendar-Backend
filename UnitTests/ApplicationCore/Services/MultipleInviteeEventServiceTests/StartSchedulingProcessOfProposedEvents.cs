using Core.Entities;
using Core.Entities.Enums;
using Core.Entities.RecurrecePattern;
using Core.Extensions;
using Core.Interfaces.IServices;
using Core.Services;
using NSubstitute;
using UnitTests.Builders;

namespace UnitTests.ApplicationCore.Services.MultipleInviteeEventServiceTests;

public class StartSchedulingProcessOfProposedEvents
{

    private readonly IEventService _eventService;
    private readonly IEventCollaboratorService _eventCollaboratorService;
    private readonly IMultipleInviteesEventService _multipleInviteesEventService;
    private readonly List<Event> _events;

    public StartSchedulingProcessOfProposedEvents()
    {
        _eventService = Substitute.For<IEventService>();
        _eventCollaboratorService = Substitute.For<IEventCollaboratorService>();
        _multipleInviteesEventService = new MultipleInviteesEventService(_eventService, _eventCollaboratorService);

        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(1)
                                                      .WithOrganizer(new UserBuilder(1).Build(),
                                                                     new DateOnly(2024, 5, 31))
                                                      .WithParticipant(new UserBuilder(1).Build(),
                                                                       ConfirmationStatus.Accept,
                                                                       new DateOnly(2024, 5, 31),
                                                                       null)
                                                      .Build();

        _events =
        [
            new EventBuilder()
            .WithRecurrencePattern(new SingleInstanceRecurrencePatternBuilder()
                                   .WithStartDate(DateTime.Now.ConvertToDateOnly())
                                   .WithEndDate(DateTime.Now.ConvertToDateOnly())
                                   .Build())
            .WithDuration(new Duration(1,2))
            .WithEventCollaborators(eventCollaborators)
            .Build()

        ];
    }

    [Fact]
    public async Task Should_StartSchedulingProcess_When_UserAvailableWithIdAndUserWithPendingStatus()
    {
        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithEventId(1)
                                              .WithEventCollaboratorRole(EventCollaboratorRole.Participant)
                                              .WithConfirmationStatus(ConfirmationStatus.Accept)
                                              .WithEventDate(new DateOnly(2024, 5, 31))
                                              .Build();

        _eventService.GetProposedEventsByUserId(1).Returns(_events);

        await _multipleInviteesEventService.StartSchedulingProcessOfProposedEvent(1);

        await _eventService.Received().GetProposedEventsByUserId(1);

        await _eventCollaboratorService.ReceivedWithAnyArgs().UpdateEventCollaborator(eventCollaborator);
    }

    [Fact]
    public async Task Should_StartSchedulingProcess_When_UserAvailableWithIdAndUserWithProposedStatus()
    {
        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithEventId(1)
                                              .WithEventCollaboratorRole(EventCollaboratorRole.Participant)
                                              .WithConfirmationStatus(ConfirmationStatus.Proposed)
                                              .WithEventDate(new DateOnly(2024, 5, 31))
                                              .WithUser(new UserBuilder(1).Build())
                                              .Build();

        _events[0].EventCollaborators.Add(new EventCollaboratorBuilder()
                                          .WithEventId(1)
                                          .WithEventCollaboratorRole(EventCollaboratorRole.Participant)
                                          .WithConfirmationStatus(ConfirmationStatus.Proposed)
                                          .WithEventDate(new DateOnly(2024, 5, 31))
                                          .WithProposedDuration(new Duration(1, 2))
                                          .WithUser(new UserBuilder(1).Build())
                                          .Build());

        _eventService.GetProposedEventsByUserId(1).Returns(_events);

        await _multipleInviteesEventService.StartSchedulingProcessOfProposedEvent(1);

        await _eventService.Received().GetProposedEventsByUserId(1);

        await _eventCollaboratorService.ReceivedWithAnyArgs().UpdateEventCollaborator(eventCollaborator);
    }

    [Fact]
    public async Task Should_StartSchedulingProcess_When_UsersWithProposedStatusAndMutualTimeBlockRequired()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(1)
                                                      .WithOrganizer(new UserBuilder(48).Build(),
                                                                     new DateOnly(2024, 6, 5))
                                                      .WithParticipant(new UserBuilder(49).Build(),
                                                                       ConfirmationStatus.Proposed,
                                                                       new DateOnly(2024, 6, 5),
                                                                       new Duration(1, 4))
                                                      .WithParticipant(new UserBuilder(50).Build(),
                                                                       ConfirmationStatus.Proposed,
                                                                       new DateOnly(2024, 6, 5),
                                                                       new Duration(2, 5))
                                                      .WithParticipant(new UserBuilder(51).Build(),
                                                                       ConfirmationStatus.Proposed,
                                                                       new DateOnly(2024, 6, 5),
                                                                       new Duration(10, 15))
                                                      .Build();


        Event eventObj = new EventBuilder()
                        .WithDuration(new Duration(1, 3))
                        .WithRecurrencePattern(new SingleInstanceRecurrencePatternBuilder()
                                               .WithStartDate(DateTime.Now.ConvertToDateOnly())
                                               .WithEndDate(DateTime.Now.ConvertToDateOnly())
                                               .Build())
                        .WithEventCollaborators(eventCollaborators)
                        .Build();

        _events.Clear();

        _events.Add(eventObj);

        EventCollaborator eventCollaborator = _events[0].EventCollaborators[1];

        _eventService.GetProposedEventsByUserId(1).Returns(_events);

        await _multipleInviteesEventService.StartSchedulingProcessOfProposedEvent(1);

        await _eventService.Received().GetProposedEventsByUserId(1);

        await _eventCollaboratorService.Received().UpdateEventCollaborator(eventCollaborator);

        eventObj.Duration = new(1, 3);

        await _eventService.Received().UpdateEvent(eventObj, 1);
    }

    [Fact]
    public async Task Should_StartSchedulingProcess_When_UsersWithProposedStatusAndMutualTimeBlockRequiredAndEventNeedToScheduleImmediately()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(1)
                                                      .WithOrganizer(new UserBuilder(48).Build(),
                                                                     new DateOnly(2024, 6, 5))
                                                      .WithParticipant(new UserBuilder(49).Build(),
                                                                       ConfirmationStatus.Proposed,
                                                                       new DateOnly(2024, 6, 5),
                                                                       new Duration(1, 4))
                                                      .WithParticipant(new UserBuilder(50).Build(),
                                                                       ConfirmationStatus.Proposed,
                                                                       new DateOnly(2024, 6, 5),
                                                                       new Duration(2, 5))
                                                      .WithParticipant(new UserBuilder(51).Build(),
                                                                       ConfirmationStatus.Proposed,
                                                                       new DateOnly(2024, 6, 5),
                                                                       new Duration(8, 9))
                                                      .Build();


        Event eventObj = new EventBuilder()
                        .WithDuration(new Duration(10, 12))
                        .WithRecurrencePattern(new SingleInstanceRecurrencePattern())
                        .WithEventCollaborators(eventCollaborators)
                        .Build();

        _events.Clear();

        _events.Add(eventObj);

        EventCollaborator eventCollaborator = _events[0].EventCollaborators[1];

        _eventService.GetProposedEventsByUserId(1).Returns(_events);

        await _multipleInviteesEventService.StartSchedulingProcessOfProposedEvent(1);

        await _eventService.Received().GetProposedEventsByUserId(1);

        await _eventCollaboratorService.Received().UpdateEventCollaborator(eventCollaborator);

        eventObj.Duration = new(1, 3);

        await _eventService.Received().UpdateEvent(eventObj, 1);
    }


    [Fact]
    public async Task Should_StartSchedulingProcess_When_ProposedTimeBlockIsLarge()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(1)
                                                      .WithOrganizer(new UserBuilder(48).Build(),
                                                                     new DateOnly(2024, 6, 5))
                                                      .WithParticipant(new UserBuilder(49).Build(),
                                                                       ConfirmationStatus.Proposed,
                                                                       new DateOnly(2024, 6, 5),
                                                                       new Duration(1, 4))
                                                      .WithParticipant(new UserBuilder(50).Build(),
                                                                       ConfirmationStatus.Proposed,
                                                                       new DateOnly(2024, 6, 5),
                                                                       new Duration(2, 5))
                                                      .WithParticipant(new UserBuilder(51).Build(),
                                                                       ConfirmationStatus.Proposed,
                                                                       new DateOnly(2024, 6, 5),
                                                                       new Duration(8, 9))
                                                      .Build();


        Event eventObj = new EventBuilder()
                        .WithDuration(new Duration(10, 15))
                        .WithRecurrencePattern(new SingleInstanceRecurrencePatternBuilder()
                                               .WithStartDate(DateTime.Now.ConvertToDateOnly())
                                               .WithEndDate(DateTime.Now.ConvertToDateOnly())
                                               .Build()
                                   )
                        .WithEventCollaborators(eventCollaborators)
                        .Build();

        _events.Clear();

        _events.Add(eventObj);

        EventCollaborator eventCollaborator = _events[0].EventCollaborators[1];

        _eventService.GetProposedEventsByUserId(1).Returns(_events);

        await _multipleInviteesEventService.StartSchedulingProcessOfProposedEvent(1);

        await _eventService.Received().GetProposedEventsByUserId(1);

        await _eventCollaboratorService.Received().UpdateEventCollaborator(eventCollaborator);

        eventObj.Duration = new(1, 3);

        await _eventService.Received().UpdateEvent(eventObj, 1);
    }

    [Fact]
    public async Task Should_StartSchedulingProcess_When_ProposedStartHourIs0AndProposedEndHourIs23()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(1)
                                                      .WithOrganizer(new UserBuilder(48).Build(),
                                                                     new DateOnly(2024, 6, 5))
                                                      .WithParticipant(new UserBuilder(49).Build(),
                                                                       ConfirmationStatus.Proposed,
                                                                       new DateOnly(2024, 6, 5),
                                                                       new Duration(0, 23))
                                                      .WithParticipant(new UserBuilder(50).Build(),
                                                                       ConfirmationStatus.Proposed,
                                                                       new DateOnly(2024, 6, 5),
                                                                       new Duration(0, 23))
                                                      .WithParticipant(new UserBuilder(51).Build(),
                                                                       ConfirmationStatus.Maybe,
                                                                       new DateOnly(2024, 6, 5),
                                                                       null)
                                                      .Build();


        Event eventObj = new EventBuilder()
                        .WithDuration(new Duration(0, 23))
                        .WithRecurrencePattern(new SingleInstanceRecurrencePattern())
                        .WithEventCollaborators(eventCollaborators)
                        .Build();

        _events.Clear();

        _events.Add(eventObj);

        EventCollaborator eventCollaborator = _events[0].EventCollaborators[1];

        _eventService.GetProposedEventsByUserId(1).Returns(_events);

        await _multipleInviteesEventService.StartSchedulingProcessOfProposedEvent(1);

        await _eventService.Received().GetProposedEventsByUserId(1);

        await _eventCollaboratorService.Received().UpdateEventCollaborator(eventCollaborator);

        eventObj.Duration = new(0, 23);

        await _eventService.Received().UpdateEvent(eventObj, 1);
    }
}
