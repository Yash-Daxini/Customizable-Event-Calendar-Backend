using Core.Entities;
using Core.Entities.RecurrecePattern;
using Core.Interfaces.IServices;
using Core.Services;
using NSubstitute;

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
        _events =
        [
            new()
            {
                Duration = new Duration(1,2),
                RecurrencePattern = new DailyRecurrencePattern(),
                EventCollaborators = [
                     new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User
                            {
                                Id = 49,
                            },
                        },
                     new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User
                            {
                                Id = 48,
                            },
                        }
                    ]
            },
            new()
            {
                Duration = new Duration(1,2),
                RecurrencePattern = new DailyRecurrencePattern(),
                EventCollaborators = [
                            new EventCollaborator
                            {
                                EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                                ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                                ProposedDuration = null,
                                EventDate = new DateOnly(2024, 5, 31),
                                User = new User
                                {
                                    Id = 48,
                                },
                            },
                            new EventCollaborator
                            {
                                EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                                ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                                ProposedDuration = null,
                                EventDate = new DateOnly(2024, 5, 31),
                                User = new User
                                {
                                    Id = 49,
                                },
                            },
                        ]
            },
            new()
            {
                Duration = new Duration(1,2),
                RecurrencePattern = new DailyRecurrencePattern(),
                EventCollaborators = [
                            new EventCollaborator
                            {
                                EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                                ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                                ProposedDuration = null,
                                EventDate = new DateOnly(2024, 5, 31),
                                User = new User
                                {
                                    Id = 48,
                                },
                            },
                            new EventCollaborator
                            {
                                EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                                ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Pending,
                                ProposedDuration = null,
                                EventDate = new DateOnly(2024, 5, 31),
                                User = new User
                                {
                                    Id = 49,
                                },
                            },
                        ]
            }
        ];
    }

    [Fact]
    public async Task Should_StartSchedulingProcess_When_UserWithIdAvailableAndUserWithPendingStatus()
    {
        EventCollaborator eventCollaborator = _events[1].EventCollaborators[1];

        _eventService.GetProposedEventsByUserId(1).Returns(_events);

        await _multipleInviteesEventService.StartSchedulingProcessOfProposedEvent(1);

        await _eventService.Received().GetProposedEventsByUserId(1);

        await _eventCollaboratorService.Received().UpdateEventCollaborator(eventCollaborator);
    }

    [Fact]
    public async Task Should_StartSchedulingProcess_When_UserWithIdAvailableAndUserWithProposedStatus()
    {
        EventCollaborator eventCollaborator = _events[1].EventCollaborators[1];

        _events[0].EventCollaborators.Add(
            new EventCollaborator
            {
                EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Proposed,
                ProposedDuration = new(1, 2),
                EventDate = new DateOnly(2024, 5, 31),
                User = new User
                {
                    Id = 49,
                    Name = "b",
                    Email = "b@gmail.com",
                    Password = "b"
                },
                EventId = 47,
            });

        _eventService.GetProposedEventsByUserId(1).Returns(_events);

        await _multipleInviteesEventService.StartSchedulingProcessOfProposedEvent(1);

        await _eventService.Received().GetProposedEventsByUserId(1);

        await _eventCollaboratorService.Received().UpdateEventCollaborator(eventCollaborator);
    }

    [Fact]
    public async Task Should_StartSchedulingProcess_When_UsersWithProposedStatusAndMutualTimeBlockRequired()
    {
        Event eventObj = new()
        {
            Duration = new(1, 3),
            RecurrencePattern = new DailyRecurrencePattern(),
            EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(2024, 6, 5),
                            User = new User
                            {
                                Id = 48,
                            },
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Proposed,
                            ProposedDuration = new(1,4),
                            EventDate = new DateOnly(2024, 6, 5),
                            User = new User
                            {
                                Id = 49,
                            },
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Proposed,
                            ProposedDuration = new(2,5),
                            EventDate = new DateOnly(2024, 6, 5),
                            User = new User
                            {
                                Id = 50,
                            },
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Proposed,
                            ProposedDuration = new(10,15),
                            EventDate = new DateOnly(2024, 6, 5),
                            User = new User
                            {
                                Id = 51,
                            },
                        },
                    ]
        };

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
    public async Task Should_StartSchedulingProcess_When_UsersWithProposedStatusAndMutualTimeBlockRequiredAndEventTimeLessThanOneDay()
    {
        Event eventObj = new()
        {
            Duration = new Duration(10, 12),
            RecurrencePattern = new DailyRecurrencePattern(),
            EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            EventDate = new DateOnly(2024, 6, 5),
                            User = new User
                            {
                                Id = 48,
                            },
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Proposed,
                            ProposedDuration = new(1,4),
                            EventDate = new DateOnly(2024, 6, 5),
                            User = new User
                            {
                                Id = 49,
                            },
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Proposed,
                            ProposedDuration = new(2,5),
                            EventDate = new DateOnly(2024, 6, 5),
                            User = new User
                            {
                                Id = 50,
                            },
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Proposed,
                            ProposedDuration = new(8,9),
                            EventDate = new DateOnly(2024, 6, 5),
                            User = new User
                            {
                                Id = 51,
                            },
                        },
                    ]
        };

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
    public async Task Should_StartSchedulingProcess_When_UsersTimeBlockIsLarge()
    {
        Event eventObj = new()
        {
            Duration = new Duration(10, 15),
            RecurrencePattern = new DailyRecurrencePattern(),
            EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            EventDate = new DateOnly(2024, 6, 5),
                            User = new User
                            {
                                Id = 48,
                            },
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Proposed,
                            ProposedDuration = new(1,4),
                            EventDate = new DateOnly(2024, 6, 5),
                            User = new User
                            {
                                Id = 49,
                            },
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Proposed,
                            ProposedDuration = new(2,5),
                            EventDate = new DateOnly(2024, 6, 5),
                            User = new User
                            {
                                Id = 50,
                            },
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Proposed,
                            ProposedDuration = new(8,9),
                            EventDate = new DateOnly(2024, 6, 5),
                            User = new User
                            {
                                Id = 51,
                            },
                        },
                    ]
        };

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
    public async Task Should_StartSchedulingProcess_When_ProposedStartIs0AndProposedEndHourIs23()
    {
        Event eventObj = new()
        {
            Duration = new Duration(0, 23),
            RecurrencePattern = new DailyRecurrencePattern(),
            EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            EventDate = new DateOnly(2024, 6, 5),
                            User = new User
                            {
                                Id = 48,
                            },
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Proposed,
                            ProposedDuration = new(0,23),
                            EventDate = new DateOnly(2024, 6, 5),
                            User = new User
                            {
                                Id = 49,
                            },
                            EventId = 47
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Proposed,
                            ProposedDuration = new(0,23),
                            EventDate = new DateOnly(2024, 6, 5),
                            User = new User
                            {
                                Id = 50,
                            },
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Maybe,
                            ProposedDuration = null,
                            EventDate = new DateOnly(2024, 6, 5),
                            User = new User
                            {
                                Id = 51,
                            },
                        },
                    ]
        };

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
