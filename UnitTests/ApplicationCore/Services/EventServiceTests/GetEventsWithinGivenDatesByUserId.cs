using Core.Entities;
using Core.Extensions;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using NSubstitute;

namespace UnitTests.ApplicationCore.Services.EventServiceTests;

public class GetEventsWithinGivenDatesByUserId
{
    private readonly IEventRepository _eventRepository;

    private readonly IEventCollaboratorService _eventCollaboratorService;
    private readonly IOverlappingEventService _overlappingEventService;
    private readonly ISharedCalendarService _sharedCalendarService;
    private readonly IEventService _eventService;
    private readonly List<Event> _events;

    public GetEventsWithinGivenDatesByUserId()
    {
        _eventRepository = Substitute.For<IEventRepository>();
        _eventCollaboratorService = Substitute.For<IEventCollaboratorService>();
        _overlappingEventService = Substitute.For<IOverlappingEventService>();
        _sharedCalendarService = Substitute.For<ISharedCalendarService>();
        _eventService = new EventService(_eventRepository, _eventCollaboratorService, _overlappingEventService, _sharedCalendarService);
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


    [Theory]
    [InlineData(2024, 2024, 5, 6, 30, 1)]
    [InlineData(2024, 2024, 6, 6, 2, 10)]
    public async Task Should_ReturnListOfEvent_When_UserWithIdAvailable(int startYear, int endYear, int startMonth, int endMonth, int startDay, int endDay)
    {
        DateOnly startDate = new(startYear, startMonth, startDay);
        DateOnly endDate = new(endYear, endMonth, endDay);

        List<Event> expected = [startDay == 2 ? new()
        {
            Id = 2205,
            Title = "event 2",
            Location = "event 2",
            Description = "event 2",
            Duration = new Duration(1,2),
            RecurrencePattern = new WeeklyRecurrencePattern()
            {
                StartDate = new DateOnly(2024, 6, 2),
                EndDate = new DateOnly(2024, 6, 2),
                Frequency = Core.Entities.Enums.Frequency.Weekly,
                Interval = 2,
                ByWeekDay = [2, 6]
            },
            DateWiseEventCollaborators = [
                new EventCollaboratorsByDate
                {
                    EventDate = new DateOnly(2024, 6, 2),
                    EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(2024, 6, 2),
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
                            EventDate = new DateOnly(2024, 6, 2),
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
        } : new()
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
        },new()
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
        }];

        _eventRepository.GetEventsWithinGivenDateByUserId(48, startDate, endDate).Returns(expected);

        List<Event> events = await _eventService.GetEventsWithinGivenDatesByUserId(48, startDate, endDate);

        Assert.Equal(expected.Count, events.Count);

        Assert.Equal(expected, events);

        await _eventRepository.Received().GetEventsWithinGivenDateByUserId(48, startDate, endDate);
    }

    [Theory]
    [InlineData(2024, 2024, 6, 5, 30, 1)]
    [InlineData(2024, 2024, 6, 5, 2, 10)]
    public async Task Should_ThrowException_When_StartDateAndEndDateNotValid(int startYear, int endYear, int startMonth, int endMonth, int startDay, int endDay)
    {
        DateOnly startDate = new(startYear, startMonth, startDay);
        DateOnly endDate = new(endYear, endMonth, endDay);

        List<Event> expected = [startDay == 2 ? new()
        {
            Id = 2205,
            Title = "event 2",
            Location = "event 2",
            Description = "event 2",
            Duration = new Duration(1,2),
            RecurrencePattern = new WeeklyRecurrencePattern()
            {
                StartDate = new DateOnly(2024, 6, 2),
                EndDate = new DateOnly(2024, 6, 2),
                Frequency = Core.Entities.Enums.Frequency.Weekly,
                Interval = 2,
                ByWeekDay = [2, 6]
            },
            DateWiseEventCollaborators = [
                new EventCollaboratorsByDate
                {
                    EventDate = new DateOnly(2024, 6, 2),
                    EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(2024, 6, 2),
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
                            EventDate = new DateOnly(2024, 6, 2),
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
        } : new()
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
        },new()
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
        }];

        _eventRepository.GetEventsWithinGivenDateByUserId(48, startDate, endDate).Returns(expected);

        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await _eventService.GetEventsWithinGivenDatesByUserId(48, startDate, endDate);

        });

        await _eventRepository.DidNotReceive().GetEventsWithinGivenDateByUserId(48, startDate, endDate);
    }
}
