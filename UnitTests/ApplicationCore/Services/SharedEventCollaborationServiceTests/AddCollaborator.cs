using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IServices;
using Core.Services;
using NSubstitute;
using NullArgumentException = Core.Exceptions.NullArgumentException;

namespace UnitTests.ApplicationCore.Services.SharedEventCollaborationServiceTests;

public class AddCollaborator
{

    private readonly IEventCollaboratorService _eventCollaboratorService;
    private readonly IEventService _eventService;
    private readonly ISharedEventCollaborationService _sharedEventCollaborationService;
    private readonly List<Event> _events;

    public AddCollaborator()
    {
        _eventCollaboratorService = Substitute.For<IEventCollaboratorService>();
        _eventService = Substitute.For<IEventService>();
        _sharedEventCollaborationService = new SharedEventCollaborationService(_eventCollaboratorService, _eventService);
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
                        }
                    ]
                }
            ]
        },
        ];
    }

    [Fact]
    public async Task Should_AddCollaborator_When_NotOverlapAndNotAlreadyCollaborated()
    {
        Event eventObj = new()
        {
            Id = 2205,
            Title = "event",
            Location = "event",
            Description = "event",
            Duration = new Duration(1, 2),
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
        };

        EventCollaborator eventCollaborator = new()
        {
            Id = 1,
            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Collaborator,
            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
            EventDate = new DateOnly(2024, 5, 31),
            EventId = 1,
            User = new()
            {
                Id = 50,
                Name = "c",
                Email = "c@gmail.com",
                Password = "c"
            },
            ProposedDuration = null
        };

        _eventService.GetEventById(1, 50).Returns(eventObj);

        _eventService.GetNonProposedEventsByUserId(50).Returns(_events);

        await _sharedEventCollaborationService.AddCollaborator(eventCollaborator);

        await _eventCollaboratorService.Received().AddEventCollaborator(eventCollaborator);
    }

    [Fact]
    public async Task Should_ThrowException_When_CollaborationOverlap()
    {
        Event eventObj = new()
        {
            Id = 1,
            Title = "event",
            Location = "event",
            Description = "event",
            Duration = new Duration(1, 2),
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
        };

        _events[1].DateWiseEventCollaborators[0].EventCollaborators.Add(
            new EventCollaborator
            {
                EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                ProposedDuration = null,
                EventDate = new DateOnly(2024, 5, 31),
                User = new User
                {
                    Id = 50,
                    Name = "c",
                    Email = "c@gmail.com",
                    Password = "c"
                },
                EventId = 47
            });

        EventCollaborator eventCollaborator = new()
        {
            Id = 1,
            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Collaborator,
            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
            EventDate = new DateOnly(2024, 5, 31),
            EventId = 1,
            User = new()
            {
                Id = 50,
                Name = "c",
                Email = "c@gmail.com",
                Password = "c"
            },
            ProposedDuration = null
        };

        _eventService.GetEventById(1, 50).Returns(eventObj);

        _eventService.GetNonProposedEventsByUserId(50).Returns(_events);

        await Assert.ThrowsAsync<CollaborationOverlapException>(async () => await _sharedEventCollaborationService.AddCollaborator(eventCollaborator));

        await _eventCollaboratorService.DidNotReceive().AddEventCollaborator(eventCollaborator);
    }

    [Fact]
    public async Task Should_ThrowException_When_AlreadyCollaborated()
    {
        Event eventObj = new()
        {
            Id = 1,
            Title = "event",
            Location = "event",
            Description = "event",
            Duration = new Duration(1, 2),
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
                        },
                        new EventCollaborator
            {
                EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                ProposedDuration = null,
                EventDate = new DateOnly(2024, 5, 31),
                User = new User
                {
                    Id = 50,
                    Name = "c",
                    Email = "c@gmail.com",
                    Password = "c"
                },
                EventId = 47
            }
                    ]
                }
            ]
        };

        _events.Add(eventObj);

        EventCollaborator eventCollaborator = new()
        {
            Id = 1,
            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Collaborator,
            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
            EventDate = new DateOnly(2024, 5, 31),
            EventId = 1,
            User = new()
            {
                Id = 50,
                Name = "c",
                Email = "c@gmail.com",
                Password = "c"
            },
            ProposedDuration = null
        };

        _eventService.GetEventById(1, 50).Returns(eventObj);

        _eventService.GetNonProposedEventsByUserId(50).Returns(_events);

        await Assert.ThrowsAsync<UserAlreadyCollaboratedException>(async () => await _sharedEventCollaborationService.AddCollaborator(eventCollaborator));

        await _eventCollaboratorService.DidNotReceive().AddEventCollaborator(eventCollaborator);
    }

    [Fact]
    public async Task Should_ThrowException_When_EventCollaboratorIsNull()
    {
        Event eventObj = new()
        {
            Id = 1,
            Title = "event",
            Location = "event",
            Description = "event",
            Duration = new Duration(1, 2),
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
                        },
                        new EventCollaborator
            {
                EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                ProposedDuration = null,
                EventDate = new DateOnly(2024, 5, 31),
                User = new User
                {
                    Id = 50,
                    Name = "c",
                    Email = "c@gmail.com",
                    Password = "c"
                },
                EventId = 47
            }
                    ]
                }
            ]
        };

        _events.Add(eventObj);

        EventCollaborator eventCollaborator = null;

        _eventService.GetEventById(1, 50).Returns(eventObj);

        _eventService.GetNonProposedEventsByUserId(50).Returns(_events);

        await Assert.ThrowsAsync<NullArgumentException>(async () => await _sharedEventCollaborationService.AddCollaborator(eventCollaborator));

        await _eventCollaboratorService.DidNotReceive().AddEventCollaborator(eventCollaborator);
    }
}
