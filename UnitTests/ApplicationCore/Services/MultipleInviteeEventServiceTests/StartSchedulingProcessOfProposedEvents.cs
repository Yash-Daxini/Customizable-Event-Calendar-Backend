using Core.Entities;
using Core.Extensions;
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
            Id = 2205,
            Title = "event",
            Location = "event",
            Description = "event",
            Duration = new Duration(1,2),
            RecurrencePattern = new WeeklyRecurrencePattern()
            {
                StartDate = new DateOnly(2024, 5, 31),
                EndDate = new DateOnly(2024, 8, 25),
                Frequency = Core.Entities.Enums.Frequency.Weekly,
                Interval = 2,
                ByWeekDay = [2, 6]
            },
            DateWiseEventCollaborators = [
                new EventCollaboratorsByDate
                {
                    EventDate = new DateOnly(2024, 5, 31),
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
                                Name = "b",
                                Email = "b@gmail.com",
                                Password = "b"
                            },
                            EventId = 47
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
                                Name = "a",
                                Email = "a@gmail.com",
                                Password = "a"
                            },
                            EventId = 47
                        }
                    ]
                }
            ]
        },
            new()
        {
            Id = 2205,
            Title = "event 1",
            Location = "event 1",
            Description = "event 1",
            Duration = new Duration(1,2),
            RecurrencePattern = new WeeklyRecurrencePattern()
            {
                StartDate = new DateOnly(2024, 5, 31),
                EndDate = new DateOnly(2024, 8, 25),
                Frequency = Core.Entities.Enums.Frequency.Weekly,
                Interval = 2,
                ByWeekDay = [2, 6]
            },
            DateWiseEventCollaborators = [
                new EventCollaboratorsByDate
                {
                    EventDate = new DateOnly(2024, 5, 31),
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
                                Name = "a",
                                Email = "a@gmail.com",
                                Password = "a"
                            },
                            EventId = 47
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
                                Name = "b",
                                Email = "b@gmail.com",
                                Password = "b"
                            },
                            EventId = 47
                        },
                    ]
                }
            ]
        },
            new()
        {
            Id = 2205,
            Title = "event 2",
            Location = "event 2",
            Description = "event 2",
            Duration = new Duration(1,2),
            RecurrencePattern = new WeeklyRecurrencePattern()
            {
                StartDate = new DateOnly(2024, 5, 31),
                EndDate = new DateOnly(2024, 8, 25),
                Frequency = Core.Entities.Enums.Frequency.Weekly,
                Interval = 2,
                ByWeekDay = [2, 6]
            },
            DateWiseEventCollaborators = [
                new EventCollaboratorsByDate
                {
                    EventDate = new DateOnly(2024, 5, 31),
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
                                Name = "a",
                                Email = "a@gmail.com",
                                Password = "a"
                            },
                            EventId = 47
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
                                Name = "b",
                                Email = "b@gmail.com",
                                Password = "b"
                            },
                            EventId = 47
                        },
                    ]
                }
            ]
        }
        ];
    }

    [Fact]
    public async Task Should_StartSchedulingProcess_When_UserWithIdAvailableAndUserWithPendingStatus()
    {
        EventCollaborator eventCollaborator = _events[1].DateWiseEventCollaborators[0].EventCollaborators[1];

        _eventService.GetProposedEventsByUserId(1).Returns(_events);

        await _multipleInviteesEventService.StartSchedulingProcessOfProposedEvent(1);

        await _eventService.Received().GetProposedEventsByUserId(1);

        await _eventCollaboratorService.Received().UpdateEventCollaborator(eventCollaborator);
    }

    [Fact]
    public async Task Should_StartSchedulingProcess_When_UserWithIdAvailableAndUserWithProposedStatus()
    {
        EventCollaborator eventCollaborator = _events[1].DateWiseEventCollaborators[0].EventCollaborators[1];

        _events[0].DateWiseEventCollaborators[0].EventCollaborators.Add(
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
            Id = 47,
            Title = "event 2",
            Location = "event 2",
            Description = "event 2",
            Duration = new Duration(10, 12),
            RecurrencePattern = new WeeklyRecurrencePattern()
            {
                StartDate = new DateOnly(2024, 6, 5),
                EndDate = new DateOnly(2024, 6, 5),
                Frequency = Core.Entities.Enums.Frequency.None,
                Interval = 1,
                ByWeekDay = null
            },
            DateWiseEventCollaborators = [
                new EventCollaboratorsByDate
                {
                    EventDate = new DateOnly(2024, 6, 5),
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
                                Name = "a",
                                Email = "a@gmail.com",
                                Password = "a"
                            },
                            EventId = 47
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
                                Name = "b",
                                Email = "b@gmail.com",
                                Password = "b"
                            },
                            EventId = 47
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
                                Name = "c",
                                Email = "c@gmail.com",
                                Password = "c"
                            },
                            EventId = 47
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
                                Name = "c",
                                Email = "c@gmail.com",
                                Password = "c"
                            },
                            EventId = 47
                        },
                    ]
                }
            ]
        };

        _events.Clear();

        _events.Add(eventObj);

        EventCollaborator eventCollaborator = _events[0].DateWiseEventCollaborators[0].EventCollaborators[1];

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
            Id = 47,
            Title = "event 2",
            Location = "event 2",
            Description = "event 2",
            Duration = new Duration(10, 12),
            RecurrencePattern = new WeeklyRecurrencePattern()
            {
                StartDate = DateTime.Now.ConvertToDateOnly(),
                EndDate = DateTime.Now.ConvertToDateOnly(),
                Frequency = Core.Entities.Enums.Frequency.None,
                Interval = 1,
                ByWeekDay = null
            },
            DateWiseEventCollaborators = [
                new EventCollaboratorsByDate
                {
                    EventDate = new DateOnly(2024, 6, 5),
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
                                Name = "a",
                                Email = "a@gmail.com",
                                Password = "a"
                            },
                            EventId = 47
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
                                Name = "b",
                                Email = "b@gmail.com",
                                Password = "b"
                            },
                            EventId = 47
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
                                Name = "c",
                                Email = "c@gmail.com",
                                Password = "c"
                            },
                            EventId = 47
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
                                Name = "ac",
                                Email = "ac@gmail.com",
                                Password = "ac"
                            },
                            EventId = 47
                        },
                    ]
                }
            ]
        };

        _events.Clear();

        _events.Add(eventObj);

        EventCollaborator eventCollaborator = _events[0].DateWiseEventCollaborators[0].EventCollaborators[1];

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
            Id = 47,
            Title = "event 2",
            Location = "event 2",
            Description = "event 2",
            Duration = new Duration(10, 15),
            RecurrencePattern = new WeeklyRecurrencePattern()
            {
                StartDate = DateTime.Now.ConvertToDateOnly(),
                EndDate = DateTime.Now.ConvertToDateOnly(),
                Frequency = Core.Entities.Enums.Frequency.None,
                Interval = 1,
                ByWeekDay = null
            },
            DateWiseEventCollaborators = [
                new EventCollaboratorsByDate
                {
                    EventDate = new DateOnly(2024, 6, 5),
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
                                Name = "a",
                                Email = "a@gmail.com",
                                Password = "a"
                            },
                            EventId = 47
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
                                Name = "b",
                                Email = "b@gmail.com",
                                Password = "b"
                            },
                            EventId = 47
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
                                Name = "c",
                                Email = "c@gmail.com",
                                Password = "c"
                            },
                            EventId = 47
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
                                Name = "ac",
                                Email = "ac@gmail.com",
                                Password = "ac"
                            },
                            EventId = 47
                        },
                    ]
                }
            ]
        };

        _events.Clear();

        _events.Add(eventObj);

        EventCollaborator eventCollaborator = _events[0].DateWiseEventCollaborators[0].EventCollaborators[1];

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
            Id = 47,
            Title = "event 2",
            Location = "event 2",
            Description = "event 2",
            Duration = new Duration(0, 23),
            RecurrencePattern = new WeeklyRecurrencePattern()
            {
                StartDate = DateTime.Now.ConvertToDateOnly(),
                EndDate = DateTime.Now.ConvertToDateOnly(),
                Frequency = Core.Entities.Enums.Frequency.None,
                Interval = 1,
                ByWeekDay = null
            },
            DateWiseEventCollaborators = [
                new EventCollaboratorsByDate
                {
                    EventDate = new DateOnly(2024, 6, 5),
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
                                Name = "a",
                                Email = "a@gmail.com",
                                Password = "a"
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
                                Id = 49,
                                Name = "b",
                                Email = "b@gmail.com",
                                Password = "b"
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
                                Name = "c",
                                Email = "c@gmail.com",
                                Password = "c"
                            },
                            EventId = 47
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
                                Name = "ac",
                                Email = "ac@gmail.com",
                                Password = "ac"
                            },
                            EventId = 47
                        },
                    ]
                }
            ]
        };

        _events.Clear();

        _events.Add(eventObj);

        EventCollaborator eventCollaborator = _events[0].DateWiseEventCollaborators[0].EventCollaborators[1];

        _eventService.GetProposedEventsByUserId(1).Returns(_events);

        await _multipleInviteesEventService.StartSchedulingProcessOfProposedEvent(1);

        await _eventService.Received().GetProposedEventsByUserId(1);

        await _eventCollaboratorService.Received().UpdateEventCollaborator(eventCollaborator);

        eventObj.Duration = new(0, 23);

        await _eventService.Received().UpdateEvent(eventObj, 1);
    }
}
